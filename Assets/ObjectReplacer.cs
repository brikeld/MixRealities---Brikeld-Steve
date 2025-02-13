using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    public GameObject prefab;
    GameObject instance;

    public Vector3 offsetPosition;

    public LayerMask layerMask = 0;

    public Vector3 boxSize;
    public Vector3 boxCenter;

    float timeSinceNoInstance = 10000;

    public float spawnDelay = 0.5f;

    void OnEnable()
    {
        FixedUpdate();
    }

    void FixedUpdate()
    {

        if (!instance)
        {
            timeSinceNoInstance += Time.deltaTime;
        }
        else
        {
            timeSinceNoInstance = 0;
        }


        if (timeSinceNoInstance > spawnDelay)
        {
            Vector3 halfExtents = boxSize / 2;

            if (!Physics.CheckBox(transform.TransformPoint(boxCenter), halfExtents, transform.rotation, layerMask))
            {

                instance = Instantiate(prefab, transform.TransformPoint(offsetPosition), transform.rotation);
            }
        }
    }
}
