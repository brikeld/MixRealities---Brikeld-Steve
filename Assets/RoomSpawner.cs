using UnityEngine;
using Meta.XR.MRUtilityKit;
using Unity.XR.CoreUtils;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    public float minRadius = 0.5f;
    public MRUKAnchor.SceneLabels Labels = ~(MRUKAnchor.SceneLabels)0;
    public MRUK.SurfaceType surfaceType = (MRUK.SurfaceType)~0;
    public int maxTryCount = 100;
    public int maxSpawnCount = 10;

    public LayerMask layerMask = 0;
    public Vector3 boxSize;

    public int objCount = 0;

    public float distanceFromSurfaceForBoundsCheck = 0.1f;

    public List<GameObject> prefabs;

    int prefabId = -1;

    private void Start()
    {
        if (MRUK.Instance)
        {
            MRUK.Instance.RegisterSceneLoadedCallback(() =>
            {
                StartSpawn(MRUK.Instance.GetCurrentRoom());
            });
        }
    }

    // This function starts the spawn process
    public void StartSpawn(MRUKRoom room)
    {
        int skipped = 0;
        int tried = 0;
        int foundPos = 0;

        // Loop through the maximum number of spawn attempts
        for (int i = 0; i < maxTryCount; i++)
        {
            tried++;

            // Generate a random position on the surface
            if (room.GenerateRandomPositionOnSurface(surfaceType, minRadius, new LabelFilter(Labels), out var pos, out var normal))
            {
                foundPos++;

                Vector3 halfExtents = boxSize / 2;
                Vector3 center = pos + normal * (halfExtents.y + distanceFromSurfaceForBoundsCheck);
                Quaternion rotation = Quaternion.LookRotation(normal);

                // Check if the position is occupied by another object
                if (Physics.CheckBox(center, halfExtents, rotation, layerMask, QueryTriggerInteraction.Collide))
                {
                    skipped++;
                    continue;
                }

                prefabId++;

                if (prefabId >= prefabs.Count)
                {
                    prefabId = 0;
                }
                GameObject prefab = prefabs[prefabId];

                // Spawn multiple prefabs with random counts
                Instantiate(prefab, pos, Quaternion.LookRotation(normal) * Quaternion.Euler(90, 0, 0));
                objCount++;

                // Stop spawning if we've reached the maximum spawn count
                if (objCount >= maxSpawnCount)
                    break;
            }
        }

        Debug.Log($"{tried} attempts, {foundPos} valid positions, {skipped} skipped, {objCount} objects spawned.");
    }
}
