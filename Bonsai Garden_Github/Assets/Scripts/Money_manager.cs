using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money_manager : MonoBehaviour
{
    private int current_money = 100;
    private int current_diamond = 100;
    [SerializeField] TMP_Text currency_text;
    [SerializeField] TMP_Text diamond_text;

    private void Start()
    {
        currency_text.text = Convert.ToString(current_money);
        diamond_text.text = Convert.ToString(current_diamond);
    }

    //Non-premium money mmethods
    //===========================================================
    //===========================================================

    //Return current money
    public int Get_Money()
    {
        return current_money;
    }

    //Decreases money
    public void Decrease_Money(int amount)
    {
        current_money -= amount;
        Update_Displayed_Amount();
    }
    //Increases money
    public void Increase_Money(int amount)
    {
        current_money += amount;
        Update_Displayed_Amount();
    }

    public void Update_Displayed_Amount()
    {
        currency_text.text = Convert.ToString(current_money);
        diamond_text.text = Convert.ToString(current_diamond);
    }

    //Premium money methods
    //===========================================================
    //===========================================================

    public int Get_Diamond()
    {
        return current_diamond;
    }

    public void Decrease_Diamond(int amount)
    {
        current_diamond -= amount;
        Update_Displayed_Amount();  
    }
    public void Increase_Diamond(int amount)
    {
        current_diamond+=amount;
        Update_Displayed_Amount();    
    }
}
