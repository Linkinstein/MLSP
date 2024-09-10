using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] public Sprite itemImage;

    [SerializeField] public string itemName;

    [TextArea(15, 20)]
    [SerializeField] public string itemDescription;

}
