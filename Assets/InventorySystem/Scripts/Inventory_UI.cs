using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{


    public static Inventory_UI _instance;

    public int _inventory_items_Count;
    public int _totalWeight;

    public List<Slot_UI> _inventory_items;

    public Slot_UI[] _prefabs;

    public Transform _grid;


    void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        First_Time_Coming();
    }

    public void PickUP(Item_PickUP item)
    {
        if (item._itemType == Item_PickUP.ItemType.Quest_Item)
        {
            for (int i = 0; i < _prefabs.Length; i++)
            {
                if (item._id == _prefabs[i]._id)
                {
                    Slot_UI slot = Instantiate(_prefabs[i], _grid);
                    _inventory_items.Add(slot);
                    Set_Inventory_Count();
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < _inventory_items.Count; i++)
            {
                if (item._id == _inventory_items[i]._id)
                {
                    _inventory_items[i].Increase_Quatity();
                    Set_Inventory_Count();
                    return;
                }
            }

            for (int i = 0; i < _prefabs.Length; i++)
            {
                if (item._id == _prefabs[i]._id)
                {
                    Slot_UI slot = Instantiate(_prefabs[i], _grid);
                    _inventory_items.Add(slot);
                    Set_Inventory_Count();
                    return;
                }
            }
        }


    }




    void Set_Inventory_Count()
    {
        _inventory_items_Count = _inventory_items.Count;
        PlayerPrefs.SetInt("Inventory_items_Count", _inventory_items_Count);

        for (int i = 0; i < _inventory_items_Count; i++)
        {
            PlayerPrefs.SetInt("Inventory_Items" + i, _inventory_items[i]._id);
        }
        Total_Weight();
    }

    public void Get_Inventory_Count()
    {
        _inventory_items_Count = PlayerPrefs.GetInt("Inventory_items_Count");

        for (int i = 0; i < _inventory_items_Count; i++)
        {
            int _id = PlayerPrefs.GetInt("Inventory_Items" + i);
            Slot_UI toSpawn = null;

            for (int j = 0; j < _prefabs.Length; j++)
            {
                if (_id == _prefabs[j]._id)
                {
                    toSpawn = _prefabs[j];
                    break;
                }

            }
            //Debug.LogError("execution after break");
            Slot_UI toAdd = Instantiate(toSpawn, _grid);
            _inventory_items.Add(toAdd);
            
            Total_Weight();
        }
    }




    void First_Time_Coming()
    {
        if (PlayerPrefs.GetInt("First_Time") != 1)
        {
            for (int i = 0; i < _prefabs.Length; i++)
            {
                _prefabs[i].First_Time();
            }

            //  Call Here

            PlayerPrefs.SetInt("First_Time", 1);

        }
        else
        {
            Get_Inventory_Count();
        }
    }




    public void Remvoe_Slots(Slot_UI slot_UI)
    {
        _inventory_items.Remove(slot_UI);
        Destroy(slot_UI.gameObject);
        Set_Inventory_Count();
    }


    public void Total_Weight()
    {
        _totalWeight = 0;
        for (int i = 0; i < _inventory_items.Count; i++)
        {
            _inventory_items[i].Set_Weight();
            _totalWeight += _inventory_items[i]._totalWeight;
        }

        FirstPersonController._instance.Set_Bag(_totalWeight);
    }




}



