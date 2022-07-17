using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Placed_Bonsai_Handler : MonoBehaviour
{

    [SerializeField] private Bonsai_Data bonsaiDB;
    [SerializeField] private Place_Bonsai place_bonsai;
    Bonsai_Attributes this_bonsai;

    //Sprite and timer variables
    private SpriteRenderer this_renderer;
    private float current_timer_time = 0;
    private string current_state;
    private bool seed_timer_started = false;

    //Menu variables
    [SerializeField] private TMP_Text timer_text;
    [SerializeField] private GameObject select_bonsai_style;
    [SerializeField] private GameObject select_pot_style;
    [SerializeField] private GameObject relocate_bonsai;
    [SerializeField] private GameObject close_menus;
    private BoxCollider2D this_collider;
    bool raycast_hit_bonsai = false;

    private Booster_manager booster_manager;
    private Money_manager money_manager;
    private Level_Manager level_manager;

    private int bonsai_index;
    private int taps_to_remove_overgrow;
    private int xp_on_tap;

    //Time.deltaTime variables
    private float time_to_open_menus = 0.7f;
    private float time_to_generate_currency_not_overgrown = 3;
    private float time_to_generate_currency_not_overgrown_timer;
    private float time_to_generate_currency_overgrown = 10;
    private float time_to_generate_currency_overgrown_timer;

    private void Start()
    {
        this_bonsai = bonsaiDB.Get_Bonsai(bonsai_index);

        money_manager = FindObjectOfType<Money_manager>();
        booster_manager = FindObjectOfType<Booster_manager>();
        level_manager = FindObjectOfType<Level_Manager>();

        this_renderer = transform.Find("Bonsai").GetComponent<SpriteRenderer>();
        this_collider = GetComponent<BoxCollider2D>();

        taps_to_remove_overgrow = this_bonsai.taps_to_remove_overgrow;
        xp_on_tap = this_bonsai.xp_on_tap;

        time_to_generate_currency_not_overgrown_timer = time_to_generate_currency_not_overgrown;
        time_to_generate_currency_overgrown_timer = time_to_generate_currency_overgrown;


        Hide_Bonsai_Menu();
    }

    private void Update()
    {
        Handle_Timers();
        Handle_Menus();
        Autogenerate_Money();
    }

    //Behavior on tapping bonsai
    //===========================================================================
    //===========================================================================
    private void OnMouseUpAsButton()
    {
        switch (current_state)
        {
            case "overgrown":
                Overgrown_Bonsai_Tapped();
                break;
            default:
                if (booster_manager.Get_Extra_Money_Booster_Activity_State() == false)
                {
                    money_manager.Increase_Money(this_bonsai.money_on_tap);
                    level_manager.Increase_Current_XP(xp_on_tap);
                }
                else
                {
                    money_manager.Increase_Money(this_bonsai.money_on_tap * booster_manager.Get_Extra_Money_Booster_Multiplier());
                    level_manager.Increase_Current_XP(xp_on_tap);
                }
                break;
        }
    }
    private void Overgrown_Bonsai_Tapped()
    {
        taps_to_remove_overgrow--;
        if (booster_manager.Get_Extra_Money_Booster_Activity_State() == false)
        {
            money_manager.Increase_Money(this_bonsai.money_on_tap * 2);
        }
        else
        {
            money_manager.Increase_Money(this_bonsai.money_on_tap * booster_manager.Get_Extra_Money_Booster_Multiplier() * 2);
        }

        if (taps_to_remove_overgrow <= 0)
        {
            this_renderer.sprite = this_bonsai.bonsai_sprite;
            current_timer_time = this_bonsai.overgrown_timer * 60;
            current_state = "tree";
            taps_to_remove_overgrow = this_bonsai.taps_to_remove_overgrow;
        }
    }

    public void Tap_Behavior()
    {
        switch (current_state)
        {
            case "overgrown":
                Overgrown_Bonsai_Tapped();
                break;
            default:
                if (booster_manager.Get_Extra_Money_Booster_Activity_State() == false)
                {
                    money_manager.Increase_Money(this_bonsai.money_on_tap);
                }
                else
                {
                    money_manager.Increase_Money(this_bonsai.money_on_tap * booster_manager.Get_Extra_Money_Booster_Multiplier());
                }
                break;
        }
    }

    //Tree state timer functions
    //===========================================================================
    //===========================================================================
    private void Handle_Timers()
    {
        switch (current_state)
        {
            case "sapling":
                Sapling_State_Timer();
                break;
            case "tree":
                Tree_State_Timer();
                break;
            case "overgrown":
                return;
            default:
                Seed_State_Timer();
                break;
        }
    }
    public void Decrease_Timer(float decrease_by)
    {
        current_timer_time -= decrease_by;
    }
    private void Seed_State_Timer()
    {
        if (seed_timer_started == false)
        {
            current_timer_time = this_bonsai.seed_timer * 60;
            seed_timer_started = true;
        }
        current_timer_time -= Time.deltaTime;
        TimeSpan time_for_display = TimeSpan.FromSeconds(current_timer_time);
        timer_text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time_for_display.Hours, time_for_display.Minutes, time_for_display.Seconds);

        if (current_timer_time <= 0)
        {
            current_timer_time = this_bonsai.sapling_timer * 60;
            this_renderer.sprite = this_bonsai.sapling_sprite;
            current_state = "sapling";
        }
    }

    private void Sapling_State_Timer()
    {
        current_timer_time -= Time.deltaTime;
        TimeSpan time_for_display = TimeSpan.FromSeconds(current_timer_time);
        timer_text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time_for_display.Hours, time_for_display.Minutes, time_for_display.Seconds);

        if (current_timer_time <= 0)
        {
            current_timer_time = this_bonsai.overgrown_timer * 60;
            this_renderer.sprite = this_bonsai.bonsai_sprite;
            current_state = "tree";
            timer_text.gameObject.SetActive(false);
        }
    }

    private void Tree_State_Timer()
    {
        current_timer_time -= Time.deltaTime;

        if (current_timer_time <= 0)
        {
            this_renderer.sprite = this_bonsai.overgrown_sprite;
            current_state = "overgrown";
            timer_text.gameObject.SetActive(false);
        }
    }

    //Menu functions
    //===========================================================================
    //===========================================================================

    private void Handle_Menus()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.transform != null && hit.collider.CompareTag("Placed_Bonsai"))
            {
                raycast_hit_bonsai = true;
            }
            time_to_open_menus -= Time.deltaTime;
            if (raycast_hit_bonsai == true && hit.collider == this_collider)
            {
                if (time_to_open_menus <= 0 && current_state != "tree")
                {
                    Open_Bonsai_Menu_Not_Tree();
                }
                if (time_to_open_menus <= 0 && current_state == "tree")
                {
                    Open_Bonsai_Menu_Tree();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            time_to_open_menus = 0.7f;
            raycast_hit_bonsai = false;
        }
    }
    public void Hide_Bonsai_Menu()
    {
        timer_text.gameObject.SetActive(false);
        select_bonsai_style.SetActive(false);
        select_pot_style.SetActive(false);
        relocate_bonsai.SetActive(false);
        close_menus.SetActive(false);
    }

    private void Open_Bonsai_Menu_Not_Tree()
    {
        Hide_Menu_On_Other_Trees();
        timer_text.gameObject.SetActive(true);
        select_bonsai_style.SetActive(true);
        select_pot_style.SetActive(true);
        relocate_bonsai.SetActive(true);
        close_menus.SetActive(true);

    }
    private void Open_Bonsai_Menu_Tree()
    {
        Hide_Menu_On_Other_Trees();
        select_bonsai_style.gameObject.SetActive(true);
        select_pot_style.gameObject.SetActive(true);
        relocate_bonsai.gameObject.SetActive(true);
        close_menus.SetActive(true);
    }

    private void Hide_Menu_On_Other_Trees()
    {
        List<Placed_Bonsai_Handler> placed_bonsai = new List<Placed_Bonsai_Handler>();
        foreach (var bonsai in GameObject.FindGameObjectsWithTag("Placed_Bonsai"))
        {
            placed_bonsai.Add(bonsai.GetComponent<Placed_Bonsai_Handler>());
        }
        foreach (var bonsai in placed_bonsai)
        {
            bonsai.Hide_Bonsai_Menu();
        }
        placed_bonsai.Clear();
    }

    //===========================================================================
    //===========================================================================

    //Boosters
    //===========================================================================
    //===========================================================================
    private void Autogenerate_Money()
    {
        if (current_state != "overgrown")
        {
            time_to_generate_currency_not_overgrown_timer -= Time.deltaTime;
            if (time_to_generate_currency_not_overgrown_timer <= 0)
            {
                money_manager.Increase_Money(this_bonsai.money_on_tap);
                time_to_generate_currency_not_overgrown_timer = time_to_generate_currency_not_overgrown;
                level_manager.Increase_Current_XP(xp_on_tap);
            }
        }
        else
        {
            time_to_generate_currency_overgrown_timer -= Time.deltaTime;
            if (time_to_generate_currency_overgrown_timer <= 0)
            {
                money_manager.Increase_Money(this_bonsai.money_on_tap);
                time_to_generate_currency_overgrown_timer = time_to_generate_currency_overgrown;
                level_manager.Increase_Current_XP(xp_on_tap);
            }
        }
    }

    public void Set_Bonsai_Index(int bonsai_index_receive)
    {
        bonsai_index = bonsai_index_receive;
    }

    public string Get_Current_State()
    {
        return current_state;
    }

    public int Get_Index()
    {
        return this_bonsai.bonsai_index;
    }
}
