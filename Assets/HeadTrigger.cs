using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null &&
            other.attachedRigidbody.GetComponent<CheckTrigger>())
        {
            GameObject otherObj = other.attachedRigidbody.gameObject;

            CombineObjectType otherType = otherObj.GetComponent<ObjectType>().type;

            if (otherType == CombineObjectType.Tiramisu)
            {
                Destroy(otherObj);
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
