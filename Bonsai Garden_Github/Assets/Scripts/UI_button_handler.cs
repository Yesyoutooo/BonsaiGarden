using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_button_handler : MonoBehaviour
{
    [SerializeField] private GameObject pause_button;
    [SerializeField] private GameObject pause_menu;
    [SerializeField] private GameObject pause_menu_resume_button;
    [SerializeField] private GameObject pause_menu_quit_button;

    [SerializeField] private GameObject bonsai_inventory_button;
    [SerializeField] private GameObject bonsai_inventory_grid;
    [SerializeField] private GameObject bonsai_inventory_close_button_1;
    [SerializeField] private GameObject bonsai_inventory_close_button_2;
    [SerializeField] private GameObject bonsai_inventory_open_decoration_button;

    [SerializeField] private GameObject decoration_inventory_grid;
    [SerializeField] private GameObject decoration_inventory_close_button_1;
    [SerializeField] private GameObject decoration_inventory_close_button_2;
    [SerializeField] private GameObject decoration_inventory_open_bonsai_button;

    [SerializeField] private GameObject disable_free_places_button;
    [SerializeField] private Place_Bonsai place_Bonsai;

    private void Enable_Menu_Buttons()
    {
        pause_button.SetActive(true);
        bonsai_inventory_button.SetActive(true);
    }

    private void Disable_Menu_Buttons()
    {
        pause_button.SetActive(false);
        bonsai_inventory_button.SetActive(false);
    }

    public void Pause_Button_Clicked()
    {
        Disable_Menu_Buttons();
        pause_menu.SetActive(true);
    }

    public void Pause_Menu_Resume_Button_Clicked()
    {
        Enable_Menu_Buttons();
        pause_menu.SetActive(false);
    }

    public void Pause_Menu_Quit_Button_Clicked()
    {
        Application.Quit();
    }

    public void Bonsai_Inventory_Button_Clicked()
    {
        bonsai_inventory_grid.SetActive(true);
    }

    public void Bonsai_Inventory_Open_Decoration_Button_Clicked()
    {
        bonsai_inventory_grid.SetActive(false);
        decoration_inventory_grid.SetActive(true);
    }

    public void Bonsai_Inventory_Close_Button_Clicked()
    {
        bonsai_inventory_grid.SetActive(false);
    }

    public void Decoration_Inventory_Button_Clicked()
    {
        bonsai_inventory_button.SetActive(false);
        decoration_inventory_grid.SetActive(true);
    }

    public void Decoration_Inventory_Open_Bonsai_Button_Clicked()
    {
        bonsai_inventory_grid.SetActive(true);
        decoration_inventory_grid.SetActive(false);
    }

    public void Decoration_Inventory_Close_Button_Clicked()
    {
        decoration_inventory_grid.SetActive(false);
    }

    public void Disable_Free_Places_Button_Clicked()
    {
        disable_free_places_button.SetActive(false);
        place_Bonsai.Disable_Free_Places();
    }
}
