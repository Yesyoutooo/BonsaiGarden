using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//May become useless

public class Shop_button : MonoBehaviour
{
    [SerializeField] private GameObject bonsai_grid;
    [SerializeField] private GameObject decoration_grid;
    [SerializeField] private GameObject plant_growth_grid;
    [SerializeField] private GameObject tools_grid;
    [SerializeField] private GameObject extras_grid;

    public void Bonsai_Button_Clicked()
    {
        bonsai_grid.SetActive(true);
        decoration_grid.SetActive(false);
        plant_growth_grid.SetActive(false);
        tools_grid.SetActive(false);
        extras_grid.SetActive(false);
    }

    public void Decoration_Button_Clicked()
    {
        bonsai_grid.SetActive(false);
        decoration_grid.SetActive(true);
        plant_growth_grid.SetActive(false);
        tools_grid.SetActive(false);
        extras_grid.SetActive(false);
    }

    public void Plant_Growth_Button_Clicked()
    {
        bonsai_grid.SetActive(false);
        decoration_grid.SetActive(false);
        plant_growth_grid.SetActive(true);
        tools_grid.SetActive(false);
        extras_grid.SetActive(false);
    }

    public void Tools_Button_Clicked()
    {
        bonsai_grid.SetActive(false);
        decoration_grid.SetActive(false);
        plant_growth_grid.SetActive(false);
        tools_grid.SetActive(true);
        extras_grid.SetActive(false);
    }

    public void Extras_Button_Clicked()
    {
        bonsai_grid.SetActive(false);
        decoration_grid.SetActive(false);
        plant_growth_grid.SetActive(false);
        tools_grid.SetActive(false);
        extras_grid.SetActive(true);
    }
}
