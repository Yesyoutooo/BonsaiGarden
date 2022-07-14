using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Menus : MonoBehaviour
{
    private GameObject parent_bonsai;
    private Placed_Bonsai_Handler handler;

    void Start()
    {
        parent_bonsai = transform.parent.gameObject;
        handler = parent_bonsai.GetComponent<Placed_Bonsai_Handler>();
    }

    private void OnMouseUpAsButton()
    {
        handler.Hide_Bonsai_Menu();
    }
}
