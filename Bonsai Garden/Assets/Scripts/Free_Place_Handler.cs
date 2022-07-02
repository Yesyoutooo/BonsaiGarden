using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free_Place_Handler : MonoBehaviour
{
    [SerializeField] private Bonsai_Data bonsaiDB;
    [SerializeField] private GameObject bonsai_prefab;
    [SerializeField] private Place_Bonsai place_bonsai;
    [SerializeField] private Money_manager money_manager;
    [SerializeField] private GameObject bonsai_inventory_UI;
    private SpriteRenderer tree_sprite;
    private Vector3 spawn_transform;

    //On clicking the free place, this handles everything
    private void OnMouseUpAsButton()
    {
        Bonsai_Attributes this_bonsai = bonsaiDB.Get_Bonsai(place_bonsai.Get_Active_Bonsai());

        if (this_bonsai.bonsai_shop_price <= money_manager.Get_Money())
        {
            Spawn_Bonsai();
            Destroy(gameObject);
            place_bonsai.Disable_Free_Places();
            place_bonsai.Clear_Placeable();
            money_manager.Update_Displayed_Amount();
            bonsai_inventory_UI.SetActive(true);
        }
    }

    //Spawns the bonsai, with the correct sprite, decreases the bonsai amount in inventory
    public void Spawn_Bonsai()
    {
        Bonsai_Attributes this_bonsai = bonsaiDB.Get_Bonsai(place_bonsai.Get_Active_Bonsai());
        GameObject new_bonsai = Instantiate(bonsai_prefab, Calculate_Spawn_Transform(transform.position), Quaternion.identity);
        tree_sprite = new_bonsai.transform.Find("Bonsai").GetComponent<SpriteRenderer>();
        tree_sprite.sprite = this_bonsai.bonsai_sprite;
        money_manager.Decrease_Money(this_bonsai.bonsai_shop_price);
        new_bonsai.GetComponent<Placed_Bonsai_Handler>().Set_Bonsai_Index(place_bonsai.Get_Active_Bonsai());
    }

    //Calculates the position of the to be placed bonsai
    public Vector3 Calculate_Spawn_Transform(Vector3 place_transform)
    {
        switch (place_transform.y)
        {
            case 0.5f:
                spawn_transform.y = 1;
                spawn_transform.x = place_transform.x + 5;
                break;
            case -1.8f:
                spawn_transform.y = -2;
                spawn_transform.x = place_transform.x + 5;
                break;
            default:
                break;
        }
        return spawn_transform;
    }


}


