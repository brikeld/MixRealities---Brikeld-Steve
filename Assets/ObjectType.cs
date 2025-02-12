using UnityEngine;

public class ObjectType : MonoBehaviour
{

    public CombineObjectType type;
}

public enum CombineObjectType{
    None,
    ChickenWhite,

    Coq,
    Milk, // always add new items at the end

    Mascarpone,

    Sugar,

    Farine,

    Coffee,

    Boudoir,
}