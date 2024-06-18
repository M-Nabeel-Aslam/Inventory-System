using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Slot_UI : MonoBehaviour
{


    public int _id;
    public string _name;
    public int _quantity, _weight, _totalWeight;

    public Sprite _icon;
    public Image _icomImage;
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI _quantityText, _weightText;

    public enum ItemType { Consumable, Non_Consumable, Quest_Item }
    public ItemType _itemType;


    public GameObject _pickable_Itemprefab;

    //public UnityEngine.Events.UnityEvent _onBuy;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void Initialize()
    {
        _nameText.text = _name;
        _icomImage.sprite = _icon;
        Get_ItemQuantity();
        Update_Qunatity();
        Set_Weight();
    }


    int Get_ItemQuantity()
    {
        if (_itemType != ItemType.Quest_Item)
        {
            _quantity = PlayerPrefs.GetInt(_name + _id);
            return _quantity;
        }
        else
            _quantityText.gameObject.SetActive(false);
        return 0;
    }


    void Set_ItemQuantity(int quantity)
    {
        if (_itemType != ItemType.Quest_Item)
        {
            PlayerPrefs.SetInt(_name + _id, quantity);
            Update_Qunatity();
        }
        Set_Weight();
    }


    void Update_Qunatity()
    {
        if (_itemType != ItemType.Quest_Item)
        {
            _quantity = PlayerPrefs.GetInt(_name + _id);
            _quantityText.text = _quantity.ToString();
        }
    }



    public void Increase_Quatity()
    {
        Set_ItemQuantity(Get_ItemQuantity() + 1);
    }


    public void Decrease_Quatity()
    {
        if (Get_ItemQuantity() > 1)
        {
            Set_ItemQuantity(Get_ItemQuantity() - 1);
            Inventory_UI._instance.Total_Weight();
        }
        else if (Get_ItemQuantity() == 1)
        {
            Inventory_UI._instance.Remvoe_Slots(this);
        }

    }

    public void First_Time()
    {
        Set_ItemQuantity(1);
    }




    public void On_Click_to_Use()
    {
        if (_itemType != ItemType.Quest_Item)
        {
            if (Get_ItemQuantity() > 1)
            {
                //   his functionality call here
                Actions_Based_On_id(_id);
                //_onBuy.Invoke();
                Decrease_Quatity();
            }
            else if (Get_ItemQuantity() <= 1)
            {
                Actions_Based_On_id(_id);
                Inventory_UI._instance.Remvoe_Slots(this);
            }
        }
        else if (_itemType == ItemType.Quest_Item)
        {
            //   his functionality call here
            Actions_Based_On_id(_id);
            //_onBuy.Invoke();
            Inventory_UI._instance.Remvoe_Slots(this);
        }



    }

    void Actions_Based_On_id(int id)
    {
        if (id == 0) { Item_Uses._instance.Add_Health(10); }         //      Health
        else if (id == 1) { Item_Uses._instance.Add_Mana(10); }         //      Mana
        else if (id == 2) { }           //          Sword
        else if (id == 3) { Item_Uses._instance.Add_Health(50); }       //      Health
    }

    public void Throw_pickAble()
    {
        if (_itemType != ItemType.Quest_Item)
        {
            FirstPersonController._instance.Throw_PickAble(_pickable_Itemprefab);
            Decrease_Quatity();
        }
        else if (_itemType == ItemType.Quest_Item)
        {
            FirstPersonController._instance.Throw_PickAble(_pickable_Itemprefab);
            Inventory_UI._instance.Remvoe_Slots(this);
        }
    }



    public void Set_Weight()
    {
        if (_itemType != ItemType.Quest_Item)
            _totalWeight = Get_ItemQuantity() * _weight;
        else
            _totalWeight = _weight;

        _weightText.text = _totalWeight.ToString();
    }
}
