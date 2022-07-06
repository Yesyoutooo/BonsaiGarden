using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//May become useless
public class Bonsai_Shop_Manager : MonoBehaviour

{
    public Bonsai_Data bonsaiDB;
    public TMP_Text bonsai_name;
    public TMP_Text bonsai_price;
    public Image bonsai_sprite;
    [SerializeField] public int bonsai_index;

    private void Start()
    {
        Display_Bonsai_Values();
    }

    private void Display_Bonsai_Values()
    {
        Bonsai_Attributes this_bonsai = bonsaiDB.Get_Bonsai(bonsai_index);
        bonsai_sprite.sprite = this_bonsai.bonsai_sprite;
        bonsai_name.text = this_bonsai.bonsai_name + " seeds";
        bonsai_price.text = Convert.ToString(this_bonsai.bonsai_shop_price);
    }
}
