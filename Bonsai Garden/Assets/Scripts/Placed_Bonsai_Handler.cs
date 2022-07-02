using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placed_Bonsai_Handler : MonoBehaviour
{

    [SerializeField] private Bonsai_Data bonsaiDB;
    [SerializeField] private Place_Bonsai place_bonsai;
    Bonsai_Attributes this_bonsai;

    private Money_manager money_manager;
    private int bonsai_index;

    private void Start()
    {
        this_bonsai = bonsaiDB.Get_Bonsai(bonsai_index);
        money_manager = FindObjectOfType<Money_manager>();
    }

    //Behavior on tapping bonsai
    private void OnMouseUpAsButton()
    {
        money_manager.Increase_Money(this_bonsai.money_on_tap);
        money_manager.Update_Displayed_Amount();
    }

    //Sets the index on each placed bonsai
    public void Set_Bonsai_Index(int bonsai_index_receive)
    {
        bonsai_index = bonsai_index_receive;
    }



}
