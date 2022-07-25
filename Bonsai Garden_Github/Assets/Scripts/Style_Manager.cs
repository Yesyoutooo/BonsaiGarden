using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Style_Manager : MonoBehaviour
{
    [SerializeField] Style_Data styleDB_0;
    [SerializeField] Style_Data styleDB_1;
    [SerializeField] Style_Data styleDB_2;

    [SerializeField] GameObject style_shop_0;
    [SerializeField] GameObject style_shop_1;
    [SerializeField] GameObject style_shop_2;

    private string scriptable_name;
    private Style_Data this_styleDB;
    private SpriteRenderer active_spriterenderer;

    public void Open_Style_Shop()
    {
        switch (scriptable_name)
        {
            case "Style_0":
                style_shop_0.gameObject.SetActive(true);
                this_styleDB = styleDB_0;
                break;
            case "Style_1":
                style_shop_1.gameObject.SetActive(true);
                this_styleDB = styleDB_1;
                break;
            case "Style_2":
                style_shop_2.gameObject.SetActive(true);
                this_styleDB = styleDB_2;
                break;
            default:
                break;
        }
    }

    public void Set_Scriptable_Name(string set_name)
    {
        scriptable_name = set_name;
    }

    public void Set_Active_Spriterenderer(SpriteRenderer spriterenderer)
    {
        active_spriterenderer = spriterenderer;
    }

    public void Style_Button_Clicked(int style_index)
    {
        active_spriterenderer.sprite = this_styleDB.Get_Style(style_index).style_sprite;
    }
}
