using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Relic_Prefabs : MonoBehaviour
{
    public Text u_RelicName_Text;
    public Image u_Relic_Image;
    public Text u_RelicDesc_Text;

    public void Set_Data(string name, Sprite sprtie, string desc)
    {
        u_RelicName_Text.text = name;
        u_Relic_Image.sprite = sprtie;
        u_RelicDesc_Text.text = desc;
    }
}
