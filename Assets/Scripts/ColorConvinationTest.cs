using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorConvinationTest : MonoBehaviour {

    public Color colorMix;

    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;
        return result;
    }  
}
