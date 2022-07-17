using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Style_Manager : MonoBehaviour
{
    [SerializeField] Style_Data Style_0;
    [SerializeField] Style_Data Style_1;
    [SerializeField] Style_Data Style_2;

    [SerializeField] GameObject Style_Shop_0;
    [SerializeField] GameObject Style_Shop_1;
    [SerializeField] GameObject Style_Shop_2;

    string scriptable_name;

    public void Open_Style_Shop()
    {
        switch (scriptable_name)
        {
            case "Style_0":
                Style_Shop_0.gameObject.SetActive(true);
                break;
            case "Style_1":
                Style_Shop_1.gameObject.SetActive(true);
                break;
            case "Style_2":
                Style_Shop_2.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Set_Scriptable_Name(string set_name)
    {
        scriptable_name = set_name;
    }
}
