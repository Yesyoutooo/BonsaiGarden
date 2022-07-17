using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Style_Data : ScriptableObject
{
    public List<Style_Attributes> style = new List<Style_Attributes>();

    public Style_Attributes Get_Style(int style_index)
    {
        return style[style_index];
    }

}
