using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    public CombineLogic combineLogic;

    // Public fields for combination sound; assign via Inspector
    public AudioSource combineAudioSource;
    public AudioClip combineSound;

    bool alreadyDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (combineAudioSource != null)
        {
            combineAudioSource.PlayOneShot(combineSound);
        }

        // CombineSound must be assigned via the Inspector.
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (alreadyDone)
        {
            return;
        }
        if (other.attachedRigidbody != null &&
            other.attachedRigidbody.GetComponent<CheckTrigger>())
        {
            GameObject otherObj = other.attachedRigidbody.gameObject;
            GameObject thisObj = gameObject;

            CombineObjectType otherType = otherObj.GetComponent<ObjectType>().type;
            CombineObjectType thisType = thisObj.GetComponent<ObjectType>().type;

            Debug.Log("Collision between " + thisType + " and " + otherType);

            GameObject prefab = null;
            for (int i = 0; i < combineLogic.combineDataList.Count; i++)
            {
                CombineLogic.CombineData data = combineLogic.combineDataList[i];
                if ((data.objectType1 == thisType && data.objectType2 == otherType) ||
                    (data.objectType1 == otherType && data.objectType2 == thisType))
                {
                    prefab = data.resultPrefab;
                }
            }

            if (prefab)
            {
                // Play combination sound if available
                if (combineAudioSource && combineSound)
                {
                    combineAudioSource.PlayOneShot(combineSound);
                }
                Instantiate(prefab, transform.position, transform.rotation);
            }

            OVRInput.SetControllerVibration(.25f, .25f, OVRInput.Controller.RTouch);
            Destroy(gameObject);
            Destroy(other.attachedRigidbody.gameObject);

            other.attachedRigidbody.GetComponent<CheckTrigger>().alreadyDone = true;
            alreadyDone = true;
        }
    }
}
