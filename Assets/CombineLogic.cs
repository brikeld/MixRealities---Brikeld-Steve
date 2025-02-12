using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombineLogic", menuName = "Scriptable Objects/CombineLogic")]
public class CombineLogic : ScriptableObject
{
    [System.Serializable]
    public struct CombineData{
        public CombineObjectType objectType1;
        public CombineObjectType objectType2;
        public GameObject resultPrefab;
    }
    public List<CombineData> combineDataList = new List<CombineData>();
}
