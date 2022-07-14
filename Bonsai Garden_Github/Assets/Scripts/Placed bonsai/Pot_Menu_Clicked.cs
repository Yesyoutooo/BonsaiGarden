using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pot_Menu_Clicked : MonoBehaviour
{
    private GameObject parent_bonsai;
    private Placed_Bonsai_Handler handler;
    private Pot_Manager pot_manager;
    private SpriteRenderer this_spriterenderer;
    private UI_button_handler ui_handler;

    void Start()
    {
        parent_bonsai = transform.parent.gameObject;
        handler = parent_bonsai.GetComponent<Placed_Bonsai_Handler>();
        pot_manager = FindObjectOfType<Pot_Manager>();
        ui_handler = FindObjectOfType<UI_button_handler>();
        this_spriterenderer = parent_bonsai.transform.Find("Pot").GetComponent<SpriteRenderer>();
    }

    private void OnMouseUpAsButton()
    {
        handler.Hide_Bonsai_Menu();
        ui_handler.Open_Pot_Shop();
        pot_manager.Set_Active_Spriterenderer(this_spriterenderer);
    }
}
