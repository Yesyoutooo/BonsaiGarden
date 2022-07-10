using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_button_handler : MonoBehaviour
{
    [SerializeField] private GameObject pause_button;
    [SerializeField] private GameObject pause_menu;
    [SerializeField] private GameObject pause_menu_resume_button;
    [SerializeField] private GameObject pause_menu_quit_button;

    [SerializeField] private GameObject bonsai_shop_button;

    [SerializeField] private GameObject bonsai_shop_grid;
    [SerializeField] private GameObject bonsai_shop_close_button_1;
    [SerializeField] private GameObject bonsai_shop_close_button_2;
    [SerializeField] private GameObject bonsai_shop_open_decoration_button;
    [SerializeField] private GameObject bonsai_shop_open_booster_button;
    [SerializeField] private GameObject bonsai_shop_open_MTX_button;

    [SerializeField] private GameObject decoration_shop_grid;
    [SerializeField] private GameObject decoration_shop_close_button_1;
    [SerializeField] private GameObject decoration_shop_close_button_2;
    [SerializeField] private GameObject decoration_shop_open_bonsai_button;
    [SerializeField] private GameObject decoration_shop_open_booster_button;
    [SerializeField] private GameObject decoration_shop_open_MTX_button;

    [SerializeField] private GameObject booster_shop_grid;
    [SerializeField] private GameObject booster_shop_close_button_1;
    [SerializeField] private GameObject booster_shop_close_button_2;
    [SerializeField] private GameObject booster_shop_open_bonsai_button;
    [SerializeField] private GameObject booster_shop_open_decoration_button;
    [SerializeField] private GameObject booster_shop_open_MTX_button;

    [SerializeField] private GameObject MTX_shop_grid;
    [SerializeField] private GameObject MTX_shop_close_button_1;
    [SerializeField] private GameObject MTX_shop_close_button_2;
    [SerializeField] private GameObject MTX_shop_open_bonsai_button;
    [SerializeField] private GameObject MTX_shop_open_decoration_button;
    [SerializeField] private GameObject MTX_shop_open_booster_button;


    [SerializeField] private GameObject disable_free_places_button;
    [SerializeField] private Place_Bonsai place_Bonsai;

    private void Enable_Menu_Buttons()
    {
        pause_button.SetActive(true);
        bonsai_shop_button.SetActive(true);
    }

    private void Disable_Menu_Buttons()
    {
        pause_button.SetActive(false);
        bonsai_shop_button.SetActive(false);
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

    public void Open_Bonsai_Shop()
    {
        bonsai_shop_grid.SetActive(true);
        decoration_shop_grid.SetActive(false);
        booster_shop_grid.SetActive(false);
        MTX_shop_grid.SetActive(false);
    }
    public void Open_Decoration_Shop()
    {
        bonsai_shop_grid.SetActive(false);
        decoration_shop_grid.SetActive(true);
        booster_shop_grid.SetActive(false);
        MTX_shop_grid.SetActive(false);
    }
    public void Open_Booster_Shop()
    {
        bonsai_shop_grid.SetActive(false);
        decoration_shop_grid.SetActive(false);
        booster_shop_grid.SetActive(true);
        MTX_shop_grid.SetActive(false);
    }
    public void Open_MTX_Shop()
    {
        bonsai_shop_grid.SetActive(false);
        decoration_shop_grid.SetActive(false);
        booster_shop_grid.SetActive(false);
        MTX_shop_grid.SetActive(true);
    }

    public void Close_Shop()
    {
        bonsai_shop_grid.SetActive(false);
        decoration_shop_grid.SetActive(false);
        booster_shop_grid.SetActive(false);
        MTX_shop_grid.SetActive(false);
    }

    public void Disable_Free_Places_Button_Clicked()
    {
        disable_free_places_button.SetActive(false);
        place_Bonsai.Disable_Free_Places();
    }
}
