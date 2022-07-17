using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] Slider level_progress;
    private float XP_to_next_level = 1000;
    private float current_XP=0;
    [SerializeField] private float next_level_xp;
    private float current_level=1;

    void Start()
    {
        Update_Slider();
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
        XP_to_next_level = XP_to_next_level * next_level_xp;
        level_progress.value = 0;
    }
}
