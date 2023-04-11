using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IG_PlayerUI : BaseWindow
{
    #region ENUM
    enum Texts
    {
        Draw_Text,
        Cost_Text,
        Disappear_Text,
        Abandoned_Text,

        Talk_Text,
    }

    enum GameObjects
    {
        Card_Container,
    }

    enum Buttons
    {
        TurnOver_Btn,

        // EDITOR
        Draw_Btn,
        Monster_TurnOver_Btn,
    }

    enum Images
    {
        Talk_Img,
    }
    #endregion

    Card_Prefab[] m_CardPrefabs;

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
            GameObject go = GetGameObject((int)GameObjects.Card_Container);
            Util.FindAndSetComponent(go, ref m_CardPrefabs);
        }

        #region Default Event
        GetButton((int)Buttons.TurnOver_Btn).onClick.AddListener(() =>
        {
            GameManager.Instance.InGame.Turn.TurnOver();
        });
        #endregion

        #region UI EVENT
        InGameUIEventController UIEvent = GameManager.Instance.InGame.UIEventController;
        UIEvent.Set_DrawText -= Set_DrawText;
        UIEvent.Set_DrawText += Set_DrawText;
        UIEvent.Set_CostText -= Set_CostText;
        UIEvent.Set_CostText += Set_CostText;
        UIEvent.Set_DisappearText -= Set_DisappearText;
        UIEvent.Set_DisappearText += Set_DisappearText;
        UIEvent.Set_DisappearText -= Set_AbandonedText;
        UIEvent.Set_AbandonedText += Set_AbandonedText;

        UIEvent.Draw -= Draw;
        UIEvent.Draw += Draw;
        UIEvent.Rebirth -= Rebirth;
        UIEvent.Rebirth += Rebirth;
        UIEvent.Abandoned -= Abandon;
        UIEvent.Abandoned += Abandon;
        #endregion

        #region EDITOR
        GetButton((int)Buttons.Draw_Btn).onClick.AddListener(Editor_Draw);
        GetButton((int)Buttons.Monster_TurnOver_Btn).onClick.AddListener(Editor_MonsterTurnoverBtn);
        #endregion
    }

    void Init()
    {
        Reset_CardPrefabs();
        Set_TalkImg(false);
    }

    #region CARD
    void Reset_CardPrefabs()
    {
        foreach (Card_Prefab card in m_CardPrefabs)
        {
            if(card != null)
            {
                card.Delete_CardData();
            }            
        }
    }

    void Draw(Card_Data c_Data)
    {
        Card_Prefab card = Get_NoDataCardPrefabs();
        if(card == null)
        {
            Set_TalkText("카드가 너무 많아...");
            return;
        }
        card.Set_CardData(c_Data);
    }

    void Rebirth(Card_Data c_Data)
    {
        Card_Prefab card = Get_NoDataCardPrefabs();
        if(card == null)
        {
            Set_TalkText("카드가 너무 많아...");
            return;
        }
        card.Set_CardData(c_Data);
    }

    void Abandon(Card_Data c_Data)
    {
        foreach(Card_Prefab card in m_CardPrefabs)
        {
            if(card.Get_CardData == c_Data)
            {
                card.Delete_CardData();
            }
        }
    }

    Card_Prefab Get_NoDataCardPrefabs()
    {
        for (int i = 0; i < m_CardPrefabs.Length; ++i)
        {
            if (m_CardPrefabs[i].gameObject.activeSelf == false)
            {
                m_CardPrefabs[i].gameObject.SetActive(true);
                return m_CardPrefabs[i];
            }
        }

        return null;
    }
    #endregion

    #region UI
    void Set_DrawText(int amount)
    {
        GetText((int)Texts.Draw_Text).text = $"{amount}";
    }

    void Set_CostText(string str)
    {
        GetText((int)Texts.Cost_Text).text = str;
    }

    void Set_DisappearText(int amount)
    {
        GetText((int)Texts.Disappear_Text).text = $"{amount}";
    }

    void Set_AbandonedText(int amount)
    {
        GetText((int)Texts.Abandoned_Text).text = $"{amount}";
    }

    void Set_TalkImg(bool b)
    {
        GetImage((int)Images.Talk_Img).gameObject.SetActive(b);
    }

    void Set_TalkText(string str)
    {
        GetText((int)Texts.Talk_Text).text = str;
    }
    #endregion

    #region EDITOR
    void Editor_Draw()
    {
        GameManager.Instance.InGame.Cards.Draw();
    }

    void Editor_MonsterTurnoverBtn()
    {
        GameManager.Instance.InGame.Turn.Set_TurnType(_Enums.TURN_TYPE.PLAYER);
    }
    #endregion

    public override void Close()
    {
        base.Close();
    }
}
