using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Placed_Bonsai_Handler : MonoBehaviour
{

    [SerializeField] private Bonsai_Data bonsaiDB;
    [SerializeField] private Place_Bonsai place_bonsai;
    Bonsai_Attributes this_bonsai;

    //Sprite and timer variables
    [SerializeField] private TMP_Text timer_text;
    private SpriteRenderer this_renderer;
    public float current_timer_time = 0;
    private string current_state;
    private bool seed_timer_started = false;

    private Money_manager money_manager;
    private int bonsai_index;

    private void Start()
    {
        this_bonsai = bonsaiDB.Get_Bonsai(bonsai_index);
        money_manager = FindObjectOfType<Money_manager>();
        this_renderer = transform.Find("Bonsai").GetComponent<SpriteRenderer>();
        timer_text.gameObject.SetActive(false);
    }

    private void Update()
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

    //Behavior on tapping bonsai
    private void OnMouseUpAsButton()
    {
        money_manager.Increase_Money(this_bonsai.money_on_tap);
        money_manager.Update_Displayed_Amount();
        Decrease_Timer(this_bonsai.decreased_time_on_tap);
    }

    //Sets the index on each placed bonsai
    public void Set_Bonsai_Index(int bonsai_index_receive)
    {
        bonsai_index = bonsai_index_receive;
    }

    public void Decrease_Timer(float decrease_by)
    {
        current_timer_time-=decrease_by;
    }

    private void Seed_State_Timer()
    {
        if (seed_timer_started==false)
        {
            current_timer_time = this_bonsai.seed_timer * 60;
            seed_timer_started = true;
        }
        current_timer_time -= Time.deltaTime;
        TimeSpan time_for_display = TimeSpan.FromSeconds(current_timer_time);
        timer_text.text = time_for_display.Hours.ToString() + ":" + time_for_display.Minutes.ToString() + ":" + time_for_display.Seconds.ToString();

        if (current_timer_time<=0)
        {
            current_timer_time = this_bonsai.sapling_timer*60;
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
            current_timer_time = this_bonsai.overgrown_timer*60;
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
        }
    }
}
