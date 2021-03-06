using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bonsai_Attributes
{
    public string bonsai_name;
    public int bonsai_index;

    public Sprite seed_sprite;
    public Sprite sapling_sprite;
    public Sprite bonsai_sprite;
    public Sprite overgrown_sprite;

    public int bonsai_shop_price;
    public int money_on_tap;
    public int decreased_time_on_tap;

    public int seed_timer;
    public int sapling_timer;
    public int overgrown_timer;
    public int style_timer;
    public int style_overgrown_timer;

    public int taps_to_remove_overgrow;
    public int xp_on_tap;
}
