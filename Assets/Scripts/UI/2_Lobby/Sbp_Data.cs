using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sbp_Data_t
{
    public string title;
    public Sprite sprites;
    public string desc;
    public Color color;
    public WIN_ID windowId;
}

public class Sbp_Data : MonoBehaviour
{
    public Sbp_Data_t[] s_Data = new Sbp_Data_t[3];
}
