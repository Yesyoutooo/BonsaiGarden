using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Place_Bonsai : MonoBehaviour
{
    [SerializeField] private Money_manager money_manager;
    [SerializeField] private Bonsai_Data bonsaiDB;
    [SerializeField] private GameObject bonsai_prefab;
    [SerializeField] Button disable_free_places_button;
    [SerializeField] private GameObject bonsai_inventory_UI;

    public GameObject[] placeable;
    private int active_bonsai_index;

    //Finds free places and opens them, if the acive bonsai is cheaper or equal to the current money
    public void Bonsai_Place_Requested()
    {
        Bonsai_Attributes this_bonsai = bonsaiDB.Get_Bonsai(active_bonsai_index);
        if (placeable.Length == 0)
        {
            placeable = FindInActiveObjectsByTag("Free_Place");

        }
        if (this_bonsai.bonsai_shop_price <= money_manager.Get_Money())
        {
            foreach (var place in placeable)
            {
                place.SetActive(true);
            }
            disable_free_places_button.gameObject.SetActive(true);
            bonsai_inventory_UI.SetActive(false);
        }
        else
        {
            return;
        }
    }
    //Disables free places after misclicking or placing a bonsai
    public void Disable_Free_Places()
    {
        foreach (var place in placeable)
        {
            place.SetActive(false);
            disable_free_places_button.gameObject.SetActive(false);
        }
    }

    //Empties the placeabl array to avoid null references
    public void Clear_Placeable()
    {
        placeable = new GameObject[0];
    }
    //Finds inactive gameobject with "Free_Place tags"
    GameObject[] FindInActiveObjectsByTag(string tag)
    {
        List<GameObject> validTransforms = new List<GameObject>();
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].gameObject.CompareTag(tag))
                {
                    validTransforms.Add(objs[i].gameObject);
                }
            }
        }
        return validTransforms.ToArray();
    }

    //Set the active bonsai index
    public void Set_Active_Bonsai(int index)
    {
        active_bonsai_index = index;
    }

    //Return the active bonsai index
    public int Get_Active_Bonsai()
    {
        return active_bonsai_index;
    }
}
