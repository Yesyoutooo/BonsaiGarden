using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_manager : MonoBehaviour
{
    private bool extra_money_booster_active = false;
    private int extra_money_multipler = 3;
    private int extra_money_booster_duration = 5;
    private float booster_duration;

    private void Start()
    {
        booster_duration = extra_money_booster_duration;
    }

    private void Update()
    {
        if (extra_money_booster_active == false)
        {
            return;
        }
        else
        {
            booster_duration -= Time.deltaTime;
            if (booster_duration<=0)
            {
                extra_money_booster_active = false;
                booster_duration = extra_money_booster_duration;
            }
        }
    }

    public void Set_Extra_Money_Activate_Booster()
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
}
