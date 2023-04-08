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
        Portion_Grid,
        Portion_Container,
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

        #region UI EVENT
        GameManager.Instance.UI_EVENT.Set_FrameHp -= Set_FrameHp;
        GameManager.Instance.UI_EVENT.Set_FrameHp += Set_FrameHp;
        GameManager.Instance.UI_EVENT.Set_FrameGold -= Set_FrameGold;
        GameManager.Instance.UI_EVENT.Set_FrameGold += Set_FrameGold;
        GameManager.Instance.UI_EVENT.Set_FrameInformation -= Set_FrameInformation;
        GameManager.Instance.UI_EVENT.Set_FrameInformation += Set_FrameInformation;
        GameManager.Instance.UI_EVENT.Set_PortionBackground -= Set_PortionBackgrouond;
        GameManager.Instance.UI_EVENT.Set_PortionBackground += Set_PortionBackgrouond;
        #endregion
    }

    void Init()
    {
        Set_PortionBackgrouond(null);
    }

    public override void Close()
    {
        base.Close();
    }

    void Set_FrameHp(int hp, int maxHp)
    {
        GetText((int)Texts.Hp_Text).text = $"{hp} / {maxHp}";
    }

    void Set_FrameGold(int gold)
    {
        GetText((int)Texts.Gold_Text).text = $"{gold}";
    }

    void Set_FrameInformation(string information)
    {
        GetText((int)Texts.Information_Text).text = information;
    }

    void Set_PortionBackgrouond(RectTransform rect)
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

    }
}
