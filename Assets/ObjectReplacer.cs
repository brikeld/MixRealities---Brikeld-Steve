using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    public GameObject prefab;
    GameObject instance;

    public Vector3 offsetPosition;

    void OnEnable()
    {
        Update();
    }

    void Update()
    {
        if (!instance)
            instance = Instantiate(prefab, transform.TransformPoint(offsetPosition), transform.rotation);
    }
}
