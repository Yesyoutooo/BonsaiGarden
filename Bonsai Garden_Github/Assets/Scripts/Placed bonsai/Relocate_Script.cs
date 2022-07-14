using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocate_Script : MonoBehaviour
{
    [SerializeField] private GameObject free_place;
    private Place_Bonsai place_bonsai;
    private Placed_Bonsai_Handler placed_bonsai_handler;
    private GameObject[] free_places;
    private bool replace_bonsai = false;
    private GameObject parent_bonsai;
    private Vector3 spawn_transform;

    private void Start()
    {
        parent_bonsai = transform.parent.gameObject;
        free_places = new GameObject[0];
        place_bonsai = FindObjectOfType<Place_Bonsai>();
        placed_bonsai_handler = parent_bonsai.GetComponent<Placed_Bonsai_Handler>();
    }
    private void Update()
    {
        if (replace_bonsai == true)
        {
            Handle_Click();
        }
    }

    public void Handle_Click()
    {
        if (replace_bonsai == true)
        {
            Raycast_On_tap();
        }
    }

    private void Raycast_On_tap()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.transform != null && hit.collider.CompareTag("Free_Place") && place_bonsai.Get_Active_Bonsai() == 200211)
            {
                Instantiate(free_place, Calculate_Free_Place_Spawn_Transform(parent_bonsai.transform.position), Quaternion.identity);
                parent_bonsai.transform.position = Calculate_Bonsai_Spawn_Transform(hit.transform.position);
                Destroy(hit.transform.gameObject);
                Clear_Free_Places();
                free_places = FindInActiveObjectsByTag("Free_Place");
                Disable_Free_Places();
                Clear_Free_Places();
                placed_bonsai_handler.Hide_Bonsai_Menu();

            }
            replace_bonsai = false;
        }

    }
    public void Clear_Free_Places()
    {
        free_places = new GameObject[0];
        replace_bonsai = false;
    }
    public void Disable_Free_Places()
    {
        foreach (var place in free_places)
        {
            place.SetActive(false);
        }
    }

    private void OnMouseUpAsButton()
    {
        if (free_places.Length == 0)
        {
            free_places = FindInActiveObjectsByTag("Free_Place");
            if (free_places.Length > 0)
            {
                foreach (var place in free_places)
                {
                    place.SetActive(true);
                }
                replace_bonsai = true;
            }
            place_bonsai.Set_Active_Bonsai(200211);
        }
    }

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

    public Vector3 Calculate_Bonsai_Spawn_Transform(Vector3 place_transform)
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

    public Vector3 Calculate_Free_Place_Spawn_Transform(Vector3 bonsai_transform)
    {
        switch (bonsai_transform.y)
        {
            case 1:
                spawn_transform.y = 0.5f;
                spawn_transform.x = bonsai_transform.x - 5;
                break;
            case -2:
                spawn_transform.y = -1.8f;
                spawn_transform.x = bonsai_transform.x - 5;
                break;
            default:
                break;
        }
        return spawn_transform;
    }
}
