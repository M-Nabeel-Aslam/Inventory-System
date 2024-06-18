using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Uses : MonoBehaviour
{


    public static Item_Uses _instance;

    public int _health = 0, _mana = 0;
    public Image _healthBar,_manaBar;
    public TextMeshProUGUI _healthBarText, _manaBarText;


    void Awake()
    {
        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Add_Health(20);
        Add_Mana(20);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void Add_Health(int add_Health) 
    {
        _health += add_Health;
        _healthBarText.text = _health.ToString();
        _healthBar.fillAmount = ((float)_health / 100f);
    }

    public void Add_Mana(int add_Mana)
    {
        _mana += add_Mana;
        _manaBarText.text = _mana.ToString();
        _manaBar.fillAmount = ((float)_mana / 100f);
    }


}

