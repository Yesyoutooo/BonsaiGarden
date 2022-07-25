using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] private Slider level_progress;
    [SerializeField] private TMP_Text level_text;
    [SerializeField] private float next_level_xp_multiplier;

    //Claim rewards
    [SerializeField] TMP_Text show_money_reward;
    [SerializeField] TMP_Text show_premium_reward;
    [SerializeField] Button claim_reward;
    [SerializeField] Button claim_5x_reward;

    private Money_manager money_manager;
    private UI_button_handler ui_handler;

    [SerializeField] private float XP_to_next_level = 1000;
    [SerializeField] private float current_XP = 0;
    private int current_level = 1;

    private int money_on_level_up;
    private int premium_on_level_up;

    void Start()
    {
        money_manager = FindObjectOfType<Money_manager>();
        ui_handler = FindObjectOfType<UI_button_handler>();
        Update_Slider();
        Display_Level();
    }

    private void Update_Slider()
    {
        level_progress.value = current_XP / XP_to_next_level;
        if (current_XP >= XP_to_next_level)
        {
            On_Level_Up();
        }
    }

    public void Increase_Current_XP(int increase_value)
    {
        current_XP += increase_value;
        Update_Slider();
    }

    private void On_Level_Up()
    {
        current_level++;
        current_XP = 0;
        XP_to_next_level = XP_to_next_level * next_level_xp_multiplier;
        level_progress.value = current_XP;
        Calculate_Level_Up_Reward();
        Display_Level();
        Display_Level_Up_Reward();
    }
    private void Display_Level()
    {
        level_text.text = "Level " + current_level.ToString();
    }

    private void Display_Level_Up_Reward()
    {
        ui_handler.Enable_Level_Up_Reward_Panel();
        show_money_reward.text = money_on_level_up.ToString();
        show_premium_reward.text = premium_on_level_up.ToString();
    }

    public void Claim_Reward_Button_Clicked()
    {
        ui_handler.Disable_Level_Up_Reward_Panel();
        money_manager.Increase_Money(money_on_level_up);
        money_manager.Increase_Diamond(premium_on_level_up);
    }
    public void Claim_5x_Reward_Button_Clicked()
    {
        ui_handler.Disable_Level_Up_Reward_Panel();
        money_manager.Increase_Money(money_on_level_up * 5);
        money_manager.Increase_Diamond(premium_on_level_up * 5);
    }

    private void Calculate_Level_Up_Reward()
    {
        switch (current_level)
        {
            case 2:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            case 3:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            case 4:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            case 5:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            case 6:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            case 7:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            case 8:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            case 9:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            case 10:
                money_on_level_up = 1000;
                premium_on_level_up = 5;
                break;
            default:
                break;
        }
    }
}
