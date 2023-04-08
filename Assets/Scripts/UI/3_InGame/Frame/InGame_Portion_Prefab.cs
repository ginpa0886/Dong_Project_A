using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_Portion_Prefab : MonoBehaviour
{
    [SerializeField] Image Portion_Img;

    Portion_Data m_PortionData;

    public void Init()
    {
        Delete_Portion();
    }

    public void Set_PortionData(Portion_Data p_Data)
    {
        this.gameObject.SetActive(true);
        m_PortionData = p_Data;
        Set_PortionImg(null);
    }

    void Set_PortionImg(Sprite sprite)
    {
        Portion_Img.sprite = sprite;
    }

    public void Use_Portion()
    {

    }

    public void Delete_Portion()
    {
        Set_PortionImg(null);
        this.gameObject.SetActive(false);
    }
}
