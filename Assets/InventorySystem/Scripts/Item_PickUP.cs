using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PickUP : MonoBehaviour
{

    public enum ItemType { Consumable, Non_Consumable, Quest_Item }
    public ItemType _itemType;
    public int _id,_weight;

}
