using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    public CombineLogic combineLogic;

    // Public fields for combination sound; assign via Inspector
    public AudioSource combineAudioSource;
    public AudioClip combineSound;
    public AudioClip correctCombineSound; // New variable for correct combination sound

    bool alreadyDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                // Play correct combination sound if available
                if (combineAudioSource)
                {
                    if (correctCombineSound)
                        combineAudioSource.PlayOneShot(correctCombineSound);
                    else if (combineSound)
                        combineAudioSource.PlayOneShot(combineSound);
                }
                Instantiate(prefab, transform.position, transform.rotation);
            }
            else
            {
                // Only trigger vibration on a wrong combination (when prefab is null)
                OVRInput.SetControllerVibration(.25f, .25f, OVRInput.Controller.RTouch);
            }

            Destroy(gameObject);
            Destroy(other.attachedRigidbody.gameObject);

            other.attachedRigidbody.GetComponent<CheckTrigger>().alreadyDone = true;
            alreadyDone = true;
        }
    }
}
