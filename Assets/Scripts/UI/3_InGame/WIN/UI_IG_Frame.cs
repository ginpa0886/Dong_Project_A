using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IG_Frame : BaseWindow
{
    #region ENUM
    enum Texts
    {
        User_Name_Text,
        Char_Name_Text,
        
        Hp_Text,
        Gold_Text,

        Information_Text,
    }

    enum GameObjects
    {
        Relic_Container,

        Portion_Grid,
        PortionUse_Container,
    }

    enum Buttons
    {
        Map_Btn,
        Card_Btn,
        Setting_Btn,
    }

    enum Images
    {

    }
    #endregion

    InGame_Portion_Prefab[] m_IngmaePortionPrefabs;
    GameObject[] m_IngamePortionContainers;

    InGame_Relic_Prefab[] m_IngameRelicPrefabs;

    public override void Open()
    {
        base.Open();
        if (IsInit() == false)
        {
            Bind();
        }

        Init();
    }

    void Bind()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        {
            GameObject go = GetGameObject((int)GameObjects.Relic_Container).gameObject;
            Util.FindAndSetComponent<InGame_Relic_Prefab>(go, ref m_IngameRelicPrefabs);
        }

        {
            GameObject go = GetGameObject((int)GameObjects.Portion_Grid).gameObject;
            Util.FindAndSetComponent<InGame_Portion_Prefab>(go, ref m_IngmaePortionPrefabs);

            m_IngamePortionContainers = new GameObject[go.transform.childCount];
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                m_IngamePortionContainers[i] = go.transform.GetChild(i).gameObject;
            }
        }

        #region DEFAULT SETTING
        Reset_All();
        #endregion

        #region UI EVENT
        GameManager.Instance.UI_EVENT.Set_FrameHp           -= Set_FrameHp;
        GameManager.Instance.UI_EVENT.Set_FrameHp           += Set_FrameHp;
        GameManager.Instance.UI_EVENT.Set_FrameGold         -= Set_FrameGold;
        GameManager.Instance.UI_EVENT.Set_FrameGold         += Set_FrameGold;
        GameManager.Instance.UI_EVENT.Set_FrameInformation  -= Set_FrameInformation;
        GameManager.Instance.UI_EVENT.Set_FrameInformation  += Set_FrameInformation;
        GameManager.Instance.UI_EVENT.Set_PortionCount      -= Set_PortionCount;
        GameManager.Instance.UI_EVENT.Set_PortionCount      += Set_PortionCount;
        GameManager.Instance.UI_EVENT.Set_PortionBackground -= Set_PortionBackgrouond;
        GameManager.Instance.UI_EVENT.Set_PortionBackground += Set_PortionBackgrouond;
        #endregion
    }

    void Init()
    {
        Set_PortionBackgrouond(null);
    }

    public void Reset_All()
    {
        int DEFAULT_PORTION_COUNT = 3;

        Set_PortionCount(DEFAULT_PORTION_COUNT);
        Reset_FrameRelictData();
    }

    public override void Close()
    {
        base.Close();
    }

    void Set_FrameHp(int hp, int maxHp)             // 캐릭터 체력 세팅 함수
    {
        GetText((int)Texts.Hp_Text).text = $"{hp} / {maxHp}";
    }

    void Set_FrameGold(int gold)                    // 캐릭터 골드 세팅 함수
    {
        GetText((int)Texts.Gold_Text).text = $"{gold}";
    }

    void Set_FrameInformation(string information)   // 던전 정보 함수
    {
        GetText((int)Texts.Information_Text).text = information;
    }

    void Set_PortionCount(int cnt)                  // 포션 총 활성화 개수 함수
    {
        for(int i = 0; i < m_IngamePortionContainers.Length; ++i)
        {
            if(cnt - 1 < i)
            {
                m_IngamePortionContainers[i].SetActive(false);
            }
            else
            {
                m_IngamePortionContainers[i].SetActive(true);
            }            
        }
    }
   
    void Set_PortionData()
    {

    }

    void RemoveAt_PortionData()
    {

    }

    void Set_PortionBackgrouond(RectTransform rect) // 포션 클릭시 나타나는 UI
    {
        if(rect == null)
        {
            GetGameObject((int)GameObjects.PortionUse_Container).SetActive(false);
            return;
        }

        int height = -30;
        GameObject go = GetGameObject((int)GameObjects.PortionUse_Container);
        go.SetActive(true);       
        go.GetComponent<RectTransform>().anchoredPosition = rect.anchoredPosition + Vector2.down * height;
    }

    void Set_FrameRelic(Relic_Data r_Data)
    {
        for(int i = 0; i < m_IngameRelicPrefabs.Length; ++i)
        {
            if(m_IngameRelicPrefabs[i].Get_RelicData == null)
            {
                m_IngameRelicPrefabs[i].Set_RelicData(r_Data);
                return;
            }
        }

        Debug.Log("there is no rest relic data array");
    }       // 유물 데이터 세팅

    void RemoveAt_FrameRelicData(Relic_Data r_Data) // 유물 데이터 삭제
    {
        for(int i = 0; i < m_IngameRelicPrefabs.Length; ++i)
        {
            if(m_IngameRelicPrefabs[i].Get_RelicData == r_Data)
            {
                m_IngameRelicPrefabs[i].Delete_RelicData();
                return;
            }
        }
        Debug.Log("there is no relic data in frame relic array");
    }

    void Reset_FrameRelictData()                    // 유물 데이터 전부 삭제
    {
        for(int i = 0; i < m_IngameRelicPrefabs.Length; ++i)
        {
            m_IngameRelicPrefabs[i].Delete_RelicData();
        }
    }
}
