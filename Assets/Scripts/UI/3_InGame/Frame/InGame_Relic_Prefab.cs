using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_Relic_Prefab : MonoBehaviour
{
    public Button m_Relic_Prefab_Btn;
    Relic_Data m_Relic_Data;

    public Relic_Data Get_RelicData { get { return m_Relic_Data; } }

    public void Set_RelicData(Relic_Data r_Data)
    {
        this.gameObject.SetActive(true);
        m_Relic_Data = r_Data;
        m_Relic_Prefab_Btn.image.sprite = null;
    }    

    public void Delete_RelicData()
    {
        m_Relic_Data = null;
        this.gameObject.SetActive(false);
    }
}
