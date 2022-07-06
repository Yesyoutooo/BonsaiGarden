using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//This became more of an inventory manager
public class Currency_Handler_And_Display : MonoBehaviour
{
    //Currency display
    [SerializeField] private TMP_Text main_money_text;
    public Money_manager money_manager;


    //Bonsai inventory attributes
    [SerializeField] Bonsai_Data bonsaiDB;
    [SerializeField] TMP_Text bonsai_name;
    [SerializeField] TMP_Text bonsai_price_text;
    [SerializeField] Image bonsai_sprite;
    [SerializeField] int bonsai_index;

    //Does inventory check and updates UI money amount
    private void Start()
    {
        Display_Bonsai_In_Inventory();
    }

    public void Display_Bonsai_In_Inventory()
    {
        Bonsai_Attributes this_bonsai = bonsaiDB.Get_Bonsai(bonsai_index);
        bonsai_name.text = this_bonsai.bonsai_name + " seeds";
        bonsai_sprite.sprite = this_bonsai.bonsai_sprite;
        bonsai_price_text.text = Convert.ToString(this_bonsai.bonsai_shop_price);
    }
}
