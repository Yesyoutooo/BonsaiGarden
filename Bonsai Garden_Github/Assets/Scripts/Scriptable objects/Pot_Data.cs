using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pot_Data : ScriptableObject
{
    public List<Pot_Attributes> pot = new List<Pot_Attributes>();

    public Pot_Attributes Get_Pot(int pot_index)
    {
        return pot[pot_index];
    }
}
