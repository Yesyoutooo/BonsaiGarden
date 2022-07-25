using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pot_Manager : MonoBehaviour
{
    [SerializeField] Pot_Data potDB;
    private SpriteRenderer active_spriterenderer;
    private Sprite current_sprite;
    private Sprite new_sprite;
    private Pot_Attributes this_pot;
    private Money_manager money_manager;
    private UI_button_handler ui_button_handler;

    void Start()
    {
        money_manager = FindObjectOfType<Money_manager>();
        ui_button_handler = FindObjectOfType<UI_button_handler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Preview_Pot_Style(int pot_index)
    {
        this_pot = potDB.Get_Pot(pot_index);

        if (money_manager.Get_Diamond() >= this_pot.pot_price)
        {
            Set_New_Sprite();
            active_spriterenderer.sprite = new_sprite;
            ui_button_handler.Enable_Pot_Buttons();
        }
        else
        {
            return;
        }
    }

    public void Set_Active_Spriterenderer(SpriteRenderer pot_spriterenderer)
    {
        active_spriterenderer = pot_spriterenderer;
        current_sprite = pot_spriterenderer.sprite;
    }

    public void Set_New_Sprite()
    {
        new_sprite = this_pot.pot_sprite;
    }

    public void Confirm_Button_Pressed()
    {
        money_manager.Decrease_Diamond(this_pot.pot_price);
        active_spriterenderer.sprite = new_sprite;
        ui_button_handler.Disable_Pot_Buttons();
        ui_button_handler.Close_Pot_Shop();
    }
    public void Cancel_Button_Pressed()
    {
        active_spriterenderer.sprite = current_sprite;
        ui_button_handler.Disable_Pot_Buttons();
        ui_button_handler.Close_Pot_Shop();
    }

}
