using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Bonsai_Data : ScriptableObject
{
    public List<Bonsai_Attributes> bonsai = new List<Bonsai_Attributes>();

    public int bonsaiCount
    {
        get { return bonsai.Count; }
    }

    public Bonsai_Attributes Get_Bonsai(int bonsai_index)
    {
        return bonsai[bonsai_index];
    }
}
