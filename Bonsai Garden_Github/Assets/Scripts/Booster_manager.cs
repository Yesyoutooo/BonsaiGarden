using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Booster_manager : MonoBehaviour
{
    //Extra money booster
    //===========================================================================
    //===========================================================================
    private bool extra_money_booster_active = false;
    private int extra_money_multipler = 2;
    private int extra_money_booster_duration = 10;
    private float extra_money_booster_duration_timer;

    //upgrades
    private float extra_money_duration_upgrade = 1000;
    private int extra_money_multiplier_upgrade = 2560;
    private bool extra_money_multiplier_max = false;
    private bool extra_money_duration_max = false;

    //display
    [SerializeField] TMP_Text extra_money_description;
    [SerializeField] TMP_Text extra_money_multiplier_upgrade_text;
    [SerializeField] TMP_Text extra_money_duration_upgrade_text;

    //timer
    private int extra_money_cooldown = 15;
    private float extra_money_cooldown_timer;
    private bool extra_money_is_useable = true;
    [SerializeField] Image extra_money_image;
    [SerializeField] Button extra_money_button;
    [SerializeField] TMP_Text extra_money_timer_text;

    //Decrease timer booster
    //===========================================================================
    //===========================================================================
    private int decrease_timer_by_minutes = 30;

    //upgrades
    private int timer_time_upgrade = 200;
    private bool timer_time_max = false;

    //display
    [SerializeField] TMP_Text decrease_timers_description;
    [SerializeField] TMP_Text timer_time_ugrade_text;

    //timer
    private int decrease_time_cooldown = 20;
    private float decrease_time_cooldown_timer;
    private bool decrease_time_is_useable = true;
    [SerializeField] Image decrease_time_image;
    [SerializeField] Button decrease_time_button;
    [SerializeField] TMP_Text decrease_time_timer_text;

    //Autotap money booster
    //===========================================================================
    //===========================================================================
    private bool autotap_money_booster_active = false;
    private int autotap_money_booster_duration = 10;
    private float autotap_money_booster_duration_timer;
    private float autotap_time = 1f;
    private float autotap_time_timer;

    //upgrades
    private float autotap_duration_upgrade = 1000;
    private int autotap_interval_upgrade = 10;
    private bool autotap_duration_upgrade_max = false;
    private bool autotap_interval_upgrade_max = false;

    //display
    [SerializeField] TMP_Text autotap_description;
    [SerializeField] TMP_Text autotap_duration_upgrade_text;
    [SerializeField] TMP_Text autotap_interval_upgrade_text;

    //timer
    private int autotap_cooldown = 15;
    private float autotap_cooldown_timer;
    private bool autootap_is_useable = true;
    [SerializeField] Image autotap_image;
    [SerializeField] Button autotap_button;
    [SerializeField] TMP_Text autotap_timer_text;


    private Money_manager money_manager;

    private void Start()
    {
        extra_money_booster_duration_timer = extra_money_booster_duration;
        autotap_money_booster_duration_timer = autotap_money_booster_duration;
        autotap_time_timer = autotap_time;
        Update_Booster_Description_Text();
        Update_Booster_Update_Description_Text();
        money_manager = FindObjectOfType<Money_manager>();

        extra_money_cooldown_timer = extra_money_cooldown * 60;
        decrease_time_cooldown_timer = decrease_time_cooldown;
        autotap_cooldown_timer = autotap_cooldown * 60;
    }

    private void Update()
    {
        Extra_Money_Booster_Update();
        Autotap_Money_Booster_Update();
        Check_If_Booster_Can_Be_Used();
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
        extra_money_is_useable = false;
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
            if (tree_state == "seed" || tree_state == "sapling")
            {
                placed_bonsai_handler.Decrease_Timer(decrease_timer_by_minutes * 60);
            }
        }
        decrease_time_is_useable = false;
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
        autootap_is_useable = false;
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

    //Booster upgrades and display
    //===========================================================================
    //===========================================================================

    private void Update_Booster_Description_Text()
    {
        extra_money_description.text = "Gain " + extra_money_multipler.ToString() + "X more gold for " + extra_money_booster_duration.ToString() + " seconds";
        decrease_timers_description.text = "Decrease timers on all trees by " + decrease_timer_by_minutes.ToString() + " minutes";
        autotap_description.text = "Each tree produces gold every " + Math.Round(autotap_time, 1).ToString() + "s for " + autotap_money_booster_duration.ToString() + " seconds";
    }

    private void Update_Booster_Update_Description_Text()
    {
        if (extra_money_duration_max == false)
        {
            extra_money_duration_upgrade_text.text = "Upgrade duration for: " + Format_Number_Display(Convert.ToInt32(extra_money_duration_upgrade));
        }
        else
        {
            extra_money_duration_upgrade_text.text = "Max level";
        }
        if (extra_money_multiplier_max == false)
        {
            extra_money_multiplier_upgrade_text.text = "Upgrade multiplier for: " + Format_Number_Display(extra_money_multiplier_upgrade);
        }
        else
        {
            extra_money_multiplier_upgrade_text.text = "Max level";
        }

        //===========================================================================

        if (timer_time_max == false)
        {
            timer_time_ugrade_text.text = "Upgrade decreased time for: " + Format_Number_Display(timer_time_upgrade);
        }
        else
        {
            timer_time_ugrade_text.text = "Max level";
        }

        //===========================================================================

        if (autotap_duration_upgrade_max == false)
        {
            autotap_duration_upgrade_text.text = "Upgrade duration for: " + Format_Number_Display(Convert.ToInt32(autotap_duration_upgrade));
        }
        else
        {
            autotap_duration_upgrade_text.text = "Max level";
        }
        if (autotap_interval_upgrade_max == false)
        {
            autotap_interval_upgrade_text.text = "Upgrade taps/s for: " + Format_Number_Display(autotap_interval_upgrade);
        }
        else
        {
            autotap_interval_upgrade_text.text = "Max level";
        }
    }

    public string Format_Number_Display(int int_to_format)
    {
        if (int_to_format < 1000)
        {
            return int_to_format.ToString();
        }
        else if (int_to_format >= 1000 && int_to_format < 10000)
        {
            return int_to_format.ToString().Substring(0, 1) + "," + int_to_format.ToString().Substring(1, 2) + "k";
        }
        else if (int_to_format >= 10000 && int_to_format < 100000)
        {
            return int_to_format.ToString().Substring(0, 2) + "," + int_to_format.ToString().Substring(2, 1) + "k";
        }
        else if (int_to_format >= 100000 && int_to_format < 1000000)
        {
            return int_to_format.ToString().Substring(0, 3) + "," + int_to_format.ToString().Substring(3, 1) + "k";
        }
        else if (int_to_format >= 1000000 && int_to_format < 10000000)
        {
            return int_to_format.ToString().Substring(0, 1) + "," + int_to_format.ToString().Substring(1, 2) + "m";
        }
        else if (int_to_format >= 10000000 && int_to_format < 100000000)
        {
            return int_to_format.ToString().Substring(0, 2) + "," + int_to_format.ToString().Substring(2, 1) + "m";
        }
        else if (int_to_format >= 100000000 && int_to_format < 1000000000)
        {
            return int_to_format.ToString().Substring(0, 3) + "," + int_to_format.ToString().Substring(3, 1) + "m";
        }
        else if (int_to_format >= 1000000000 && int_to_format <= 2147483647)
        {
            return int_to_format.ToString().Substring(0, 1) + "," + int_to_format.ToString().Substring(1, 2) + "b";
        }
        else
        {
            return int_to_format.ToString();
        }
    }

    //Extra money on tap
    //===========================================================================
    //===========================================================================
    //1b max
    public void Upgrade_Extra_Money_Multiplier_Button_Clicked()
    {
        if (money_manager.Get_Money() >= extra_money_multiplier_upgrade && extra_money_multiplier_max == false)
        {
            money_manager.Decrease_Money(extra_money_multiplier_upgrade);
            extra_money_multiplier_upgrade = extra_money_multiplier_upgrade * 5;
            extra_money_multipler++;
            if (extra_money_multipler == 10)
            {
                extra_money_multiplier_max = true;
            }
            Update_Booster_Description_Text();
            Update_Booster_Update_Description_Text();
        }
    }

    //70m max
    public void Upgrade_Extra_Money_Duration_Button_Clicked()
    {
        if (money_manager.Get_Money() >= extra_money_multiplier_upgrade && extra_money_duration_max == false)
        {
            money_manager.Decrease_Money(Mathf.RoundToInt(extra_money_duration_upgrade));
            extra_money_duration_upgrade = Mathf.RoundToInt(extra_money_duration_upgrade * 1.25f);
            extra_money_booster_duration++;
            if (extra_money_multipler == 60)
            {
                extra_money_duration_max = true;
            }
            Update_Booster_Description_Text();
            Update_Booster_Update_Description_Text();
        }
    }

    //Decrease timer
    //===========================================================================
    //===========================================================================
    //45m max
    public void Upgrade_Timer_Time_Button_Clicked()
    {
        if (money_manager.Get_Money() >= timer_time_upgrade && timer_time_max == false)
        {
            money_manager.Decrease_Money(Mathf.RoundToInt(timer_time_upgrade));
            timer_time_upgrade = Mathf.RoundToInt(timer_time_upgrade * 1.3f);
            decrease_timer_by_minutes += 30;
            if (decrease_timer_by_minutes == 1440)
            {
                timer_time_max = true;
            }
            Update_Booster_Description_Text();
            Update_Booster_Update_Description_Text();
        }
    }
    //Autotap
    //===========================================================================
    //===========================================================================
    //70m max
    public void Upgrade_Autotap_Duration_Button_Clicked()
    {
        if (money_manager.Get_Money() >= autotap_duration_upgrade && autotap_duration_upgrade_max == false)
        {
            money_manager.Decrease_Money(Mathf.RoundToInt(autotap_duration_upgrade));
            autotap_duration_upgrade = Mathf.RoundToInt(autotap_duration_upgrade * 1.25f);
            autotap_money_booster_duration++;
            if (autotap_money_booster_duration == 60)
            {
                autotap_duration_upgrade_max = true;
            }
            Update_Booster_Description_Text();
            Update_Booster_Update_Description_Text();
        }
    }

    public void Upgrade_Autotap_Interval_Button_Clicked()
    {
        if (money_manager.Get_Money() >= autotap_interval_upgrade && autotap_interval_upgrade_max == false)
        {
            money_manager.Decrease_Money(Mathf.RoundToInt(autotap_interval_upgrade));
            autotap_interval_upgrade = autotap_interval_upgrade * 10;
            autotap_time -= 0.1f;
            if (autotap_time == 0.1f)
            {
                autotap_interval_upgrade_max = true;
            }
            Update_Booster_Description_Text();
            Update_Booster_Update_Description_Text();
        }
    }

    //Can boosters be used
    //===========================================================================
    //===========================================================================
    private void Check_If_Booster_Can_Be_Used()
    {
        if (extra_money_is_useable == false)
        {
            extra_money_button.interactable = false;
            extra_money_image.color = Color.grey;
            extra_money_cooldown_timer -= Time.deltaTime;

            extra_money_timer_text.gameObject.SetActive(true);
            TimeSpan extra_money_time_display = TimeSpan.FromSeconds(extra_money_cooldown_timer);
            extra_money_timer_text.text = string.Format("{0:D2}:{1:D2}", extra_money_time_display.Minutes, extra_money_time_display.Seconds);

            if (extra_money_cooldown_timer <= 0)
            {
                extra_money_image.color = Color.white;
                extra_money_button.interactable = true;
                extra_money_is_useable = true;
                extra_money_timer_text.gameObject.SetActive(false);
                extra_money_cooldown_timer = extra_money_cooldown * 60;
            }
        }
        if (decrease_time_is_useable == false)
        {
            decrease_time_button.interactable = false;
            decrease_time_image.color = Color.grey;
            decrease_time_cooldown_timer -= Time.deltaTime;

            decrease_time_timer_text.gameObject.SetActive(true);
            TimeSpan decrease_time_time_display = TimeSpan.FromSeconds(decrease_time_cooldown_timer);
            decrease_time_timer_text.text = string.Format("{0:D2}:{1:D2}", decrease_time_time_display.Minutes, decrease_time_time_display.Seconds);

            if (decrease_time_cooldown_timer <= 0)
            {
                decrease_time_image.color = Color.white;
                decrease_time_button.interactable = true;
                decrease_time_is_useable = true;
                decrease_time_timer_text.gameObject.SetActive(false);
                decrease_time_cooldown_timer = decrease_time_cooldown * 60;
            }
        }
        if (autootap_is_useable == false)
        {
            autotap_button.interactable = false;
            autotap_image.color = Color.grey;
            autotap_cooldown_timer -= Time.deltaTime;

            autotap_timer_text.gameObject.SetActive(true);
            TimeSpan autotap_time_display = TimeSpan.FromSeconds(autotap_cooldown_timer);
            autotap_timer_text.text = string.Format("{0:D2}:{1:D2}", autotap_time_display.Minutes, autotap_time_display.Seconds);

            if (autotap_cooldown_timer <= 0)
            {
                autotap_image.color = Color.white;
                autotap_button.interactable = true;
                autootap_is_useable = true;
                autotap_timer_text.gameObject.SetActive(false);
                autotap_cooldown_timer = autotap_cooldown * 60;
            }
        }
    }

}
