using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    public GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody != null &&
            other.attachedRigidbody.GetComponent<ControllerTrigger>())
            {
                Debug.Log(other);
                Instantiate(prefab,transform.position,transform.rotation);
                Destroy(gameObject);
                Destroy(other.attachedRigidbody.gameObject);
            }
    }
}
