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

    //Opened menu variables
    [SerializeField] private TMP_Text timer_text;
    [SerializeField] private GameObject select_bonsai_style;
    [SerializeField] private GameObject select_pot_style;
    [SerializeField] private GameObject relocate_bonsai;

    [SerializeField] private Booster_manager booster_manager;

    private Money_manager money_manager;
    private int bonsai_index;
    private int taps_to_remove_overgrow;
    private float time_to_open_menus = 1;
    private float time_to_generate_currency_not_overgrown = 3;
    private float time_to_generate_currency_overgrown = 10;

    private void Start()
    {
        this_bonsai = bonsaiDB.Get_Bonsai(bonsai_index);
        money_manager = FindObjectOfType<Money_manager>();
        booster_manager = FindObjectOfType<Booster_manager>();
        this_renderer = transform.Find("Bonsai").GetComponent<SpriteRenderer>();
        taps_to_remove_overgrow = this_bonsai.taps_to_remove_overgrow;
        Hide_Bonsai_Menu();
    }

    private void Update()
    {
        Handle_Timers();
        Handle_Menus();
        Autogenerate_Money();
    }

    //Behavior on tapping bonsai
    private void OnMouseUpAsButton()
    {
        switch (current_state)
        {
            case "overgrown":
                Overgrown_Bonsai_Tapped();
                break;
            case "tree":
                if (booster_manager.Get_Extra_Money_Booster_Activity_State()==false)
                {
                    money_manager.Increase_Money(this_bonsai.money_on_tap);
                }
                else
                {
                    money_manager.Increase_Money(this_bonsai.money_on_tap * booster_manager.Get_Extra_Money_Booster_Multiplier());
                }
                break;
            default:
                if (booster_manager.Get_Extra_Money_Booster_Activity_State() == false)
                {
                    money_manager.Increase_Money(this_bonsai.money_on_tap);
                    Decrease_Timer(this_bonsai.decreased_time_on_tap);
                }
                else
                {
                    money_manager.Increase_Money(this_bonsai.money_on_tap * booster_manager.Get_Extra_Money_Booster_Multiplier());
                    Decrease_Timer(this_bonsai.decreased_time_on_tap);
                }
                break;
        }
    }

    //Sets the index on each placed bonsai
    public void Set_Bonsai_Index(int bonsai_index_receive)
    {
        bonsai_index = bonsai_index_receive;
    }

    public void Decrease_Timer(float decrease_by)
    {
        current_timer_time -= decrease_by;
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
    private void Seed_State_Timer()
    {
        if (seed_timer_started == false)
        {
            current_timer_time = this_bonsai.seed_timer * 60;
            seed_timer_started = true;
        }
        current_timer_time -= Time.deltaTime;
        TimeSpan time_for_display = TimeSpan.FromSeconds(current_timer_time);
        timer_text.text = time_for_display.Hours.ToString() + ":" + time_for_display.Minutes.ToString() + ":" + time_for_display.Seconds.ToString();

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
        timer_text.text = time_for_display.Hours.ToString() + ":" + time_for_display.Minutes.ToString() + ":" + time_for_display.Seconds.ToString();

        if (current_timer_time <= 0)
        {
            current_timer_time = this_bonsai.overgrown_timer * 60;
            this_renderer.sprite = this_bonsai.bonsai_sprite;
            current_state = "tree";
        }
    }

    private void Tree_State_Timer()
    {
        current_timer_time -= Time.deltaTime;
        TimeSpan time_for_display = TimeSpan.FromSeconds(current_timer_time);
        timer_text.text = time_for_display.Hours.ToString() + ":" + time_for_display.Minutes.ToString() + ":" + time_for_display.Seconds.ToString();

        if (current_timer_time <= 0)
        {
            this_renderer.sprite = this_bonsai.overgrown_sprite;
            current_state = "overgrown";
            timer_text.gameObject.SetActive(false);
        }
    }

    //===========================================================================
    //===========================================================================

    //Menu functions
    //===========================================================================
    //===========================================================================

    private void Handle_Menus()
    {
        if (Input.GetMouseButton(0))
        {
            time_to_open_menus -= Time.deltaTime;
            if (time_to_open_menus <= 0 && current_state != "overgrown")
            {
                Open_Bonsai_Menu_Not_Overgrown();
            }
            if (time_to_open_menus <= 0 && current_state == "overgrown")
            {
                Open_Bonsai_Menu_Overgrown();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            time_to_open_menus = 1;
        }
    }
    private void Hide_Bonsai_Menu()
    {
        timer_text.gameObject.SetActive(false);
        select_bonsai_style.SetActive(false);
        select_pot_style.SetActive(false);
        relocate_bonsai.SetActive(false);
    }

    private void Open_Bonsai_Menu_Not_Overgrown()
    {
        timer_text.gameObject.SetActive(true);
        select_bonsai_style.SetActive(true);
        select_pot_style.SetActive(true);
        relocate_bonsai.SetActive(true);
    }
    private void Open_Bonsai_Menu_Overgrown()
    {
        select_bonsai_style.SetActive(true);
        select_pot_style.SetActive(true);
        relocate_bonsai.SetActive(true);
    }
    //===========================================================================
    //===========================================================================

    private void Overgrown_Bonsai_Tapped()
    {
        taps_to_remove_overgrow--;
        if (booster_manager.Get_Extra_Money_Booster_Activity_State() == false)
        {
            money_manager.Increase_Money(this_bonsai.money_on_tap*2);
        }
        else
        {
            money_manager.Increase_Money(this_bonsai.money_on_tap * booster_manager.Get_Extra_Money_Booster_Multiplier()*2);
        }

        if (taps_to_remove_overgrow <= 0)
        {
            this_renderer.sprite = this_bonsai.bonsai_sprite;
            current_timer_time = this_bonsai.overgrown_timer * 60;
            current_state = "tree";
            taps_to_remove_overgrow = this_bonsai.taps_to_remove_overgrow;
        }
    }

    private void Autogenerate_Money()
    {
        if (current_state != "overgrown")
        {
            time_to_generate_currency_not_overgrown -= Time.deltaTime;
            if (time_to_generate_currency_not_overgrown <= 0)
            {
                money_manager.Increase_Money(this_bonsai.money_on_tap);
                time_to_generate_currency_not_overgrown = 3;
            }
        }
        else
        {
            time_to_generate_currency_overgrown -= Time.deltaTime;
            if (time_to_generate_currency_overgrown <= 0)
            {
                money_manager.Increase_Money(this_bonsai.money_on_tap);
                time_to_generate_currency_overgrown = 10;
            }
        }
    }
}
