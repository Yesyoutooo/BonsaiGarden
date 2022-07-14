using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_manager : MonoBehaviour
{
    //Extra money booster
    private bool extra_money_booster_active = false;
    private int extra_money_multipler = 2;
    private int extra_money_booster_duration = 10;
    private float extra_money_booster_duration_timer;

    //Decrease timer booster
    private int decrease_timer_by_minutes = 20;

    //Autogenerate money booster
    private bool autotap_money_booster_active = false;
    private int autotap_money_booster_duration = 9;
    private float autotap_money_booster_duration_timer;
    private float autotap_time = 0.2f;
    private float autotap_time_timer;


    private void Start()
    {
        extra_money_booster_duration_timer = extra_money_booster_duration;
        autotap_money_booster_duration_timer = autotap_money_booster_duration;
        autotap_time_timer = autotap_time;
    }

    private void Update()
    {
        Extra_Money_Booster_Update();
        Autotap_Money_Booster_Update();
    }
    //Extra money booster
    //===========================================================================
    //===========================================================================
    private void Extra_Money_Booster_Update()
    {
        if (extra_money_booster_active == false)
        {
            return;
        }
        else
        {
            extra_money_booster_duration_timer -= Time.deltaTime;
            if (extra_money_booster_duration_timer <= 0)
            {
                extra_money_booster_active = false;
                extra_money_booster_duration_timer = extra_money_booster_duration;
            }
        }
    }
    public void Extra_Money_Activate_Booster_Button_Clicked()
    {
        extra_money_booster_active = true;
    }

    public bool Get_Extra_Money_Booster_Activity_State()
    {
        return extra_money_booster_active;
    }

    public int Get_Extra_Money_Booster_Multiplier()
    {
        return extra_money_multipler;
    }

    //Decrease timer booster
    //===========================================================================
    //===========================================================================

    public void Decrease_Timer_Booster_Button_Clicked()
    {
        foreach (var bonsai in Get_All_Placed_Bonsai())
        {
            Placed_Bonsai_Handler placed_bonsai_handler = bonsai.GetComponent<Placed_Bonsai_Handler>();
            string tree_state = placed_bonsai_handler.Get_Current_State();
            if (tree_state == "" || tree_state == "sapling")
            {
                placed_bonsai_handler.Decrease_Timer(decrease_timer_by_minutes * 60);
            }
        }
    }

    //Autogenerate money booster
    //===========================================================================
    //===========================================================================
    private void Autotap_Money_Booster_Update()
    {
        if (autotap_money_booster_active == false)
        {
            return;
        }
        else
        {
            autotap_money_booster_duration_timer -= Time.deltaTime;
            autotap_time_timer -= Time.deltaTime;
            if (autotap_time_timer <= 0)
            {
                foreach (var bonsai in Get_All_Placed_Bonsai())
                {
                    Placed_Bonsai_Handler placed_bonsai_handler = bonsai.GetComponent<Placed_Bonsai_Handler>();
                    placed_bonsai_handler.Tap_Behavior();
                }
                autotap_time_timer = autotap_time;
            }
            if (autotap_money_booster_duration_timer <= 0)
            {
                autotap_money_booster_active = false;
                autotap_money_booster_duration_timer = autotap_money_booster_duration;
            }
        }
    }


    public void Autotap_Money_Booster_Button_Clicked()
    {
        autotap_money_booster_active = true;
    }

    public bool Autotap_Money_Booster_Activity_State()
    {
        return autotap_money_booster_active;
    }

    //===========================================================================
    //===========================================================================
    public List<GameObject> Get_All_Placed_Bonsai()
    {
        List<GameObject> placed_bonsai_handlers = new List<GameObject>();
        foreach (var bonsai in GameObject.FindGameObjectsWithTag("Placed_Bonsai"))
        {
            placed_bonsai_handlers.Add(bonsai);
        }

        return placed_bonsai_handlers;
    }

}
