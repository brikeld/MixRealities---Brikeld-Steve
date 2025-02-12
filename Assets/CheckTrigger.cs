using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    public GameObject prefab;
    public CombineLogic combineLogic;

    bool alreadyDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(alreadyDone)
        {
            return;
        }   
        if(other.attachedRigidbody != null &&
            other.attachedRigidbody.GetComponent<CheckTrigger>())
            {
                Debug.Log(other);
                Instantiate(prefab,transform.position,transform.rotation);

                Destroy(gameObject);
                Destroy(other.attachedRigidbody.gameObject);

                other.attachedRigidbody.GetComponent<CheckTrigger>().alreadyDone = true;
            }
    }
}
