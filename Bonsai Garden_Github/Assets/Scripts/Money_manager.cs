using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money_manager : MonoBehaviour
{
    public int current_money = 100;
    [SerializeField] TMP_Text currency_text;

    private void Start()
    {
        currency_text.text = Convert.ToString(current_money);
    }
    //Return current money
    public int Get_Money()
    {
        return current_money;
    }

    //Decreases money
    public void Decrease_Money(int amount)
    {
        current_money -= amount;
    }
    //Increases money
    public void Increase_Money(int amount)
    {
        current_money += amount;
    }

    public void Update_Displayed_Amount()
    {
        currency_text.text = Convert.ToString(current_money);
    }
}
