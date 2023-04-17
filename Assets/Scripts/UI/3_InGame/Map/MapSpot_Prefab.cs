using UnityEngine;
using UnityEngine.UI;
using static _Enums;

public class MapSpot_Prefab : MonoBehaviour
{
    public Button m_Spot_Btn;
    public Text m_Spot_Text;

    MAP_TYPE m_MapType;

    public int y;
    public int x;
    
    InGameMapManager m_mapManager;


    public void Init(int y, int x, InGameMapManager mapManager)
    {
        m_MapType = MAP_TYPE.NONE;
        this.y = y;
        this.x = x;        

        if(m_mapManager == null)
        {
            m_mapManager = mapManager;
        }
    }

    public void Get_MapType()
    {
        m_MapType = m_mapManager.Get_MapSpot(this.y, this.x);

        switch (m_MapType)
        {
            case MAP_TYPE.NONE:
                m_Spot_Text.text = "";
                m_Spot_Btn.image.color = Color.black;
                break;
            case MAP_TYPE.MONSTER:
                m_Spot_Text.text = "Monster";
                break;
            case MAP_TYPE.QUESTION_MARK:
                m_Spot_Text.text = "?";
                m_Spot_Btn.image.color = Color.red;
                break;
            case MAP_TYPE.RESET_SITE:
                m_Spot_Text.text = "Rest";
                m_Spot_Btn.image.color = Color.green;
                break;
            case MAP_TYPE.TREASURE:
                m_Spot_Text.text = "Treasure";
                m_Spot_Btn.image.color = Color.yellow;
                break;
            case MAP_TYPE.MERCHANT:
                m_Spot_Text.text = "Merchant";
                m_Spot_Btn.image.color = Color.blue;
                break;
            case MAP_TYPE.ELITE:
                m_Spot_Text.text = "Elite";
                m_Spot_Btn.image.color = Color.grey;
                break;
        }
    }
}
