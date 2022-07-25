using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Style_Menu : MonoBehaviour
{

    private GameObject parent_bonsai;
    private Placed_Bonsai_Handler handler;
    private SpriteRenderer this_spriterenderer;
    private string scirptable_name;
    private Style_Manager style_manager;

    void Start()
    {
        parent_bonsai = transform.parent.gameObject;
        handler = parent_bonsai.GetComponent<Placed_Bonsai_Handler>();
        style_manager = FindObjectOfType<Style_Manager>();
        this_spriterenderer = parent_bonsai.transform.Find("Bonsai").GetComponent<SpriteRenderer>();
        scirptable_name = "Style_" + handler.Get_Index();
    }

    private void OnMouseUpAsButton()
    {
        if (handler.Get_Current_State() == "tree" || handler.Get_Current_State() == "overgrown")
        {
            handler.Hide_Bonsai_Menu();
            style_manager.Set_Scriptable_Name(scirptable_name);
            style_manager.Open_Style_Shop();
            style_manager.Set_Active_Spriterenderer(this_spriterenderer);
        }
        else
        {
            return;
        }
    }
}
