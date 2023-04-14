using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCardManager
{
    private InGameManager m_ingameManager;
    InGameManager InGame { get { return m_ingameManager; } }
    
    // ingame 내부에서 카드 관리 역할을 수행함
    List<Card_Data> m_curGameDeck;

    List<Card_Data> m_Draw;     // 드로우 카드 목록
    List<Card_Data> m_Hand;     // 핸드 카드 목록
    List<Card_Data> m_Abandon;  // 버려진 카드 목록
    List<Card_Data> m_Disapper; // 소멸된 카드 목록

    public InGameCardManager(InGameManager ingameManager)
    {
        m_curGameDeck = new List<Card_Data>();
        m_Draw        = new List<Card_Data>();
        m_Hand        = new List<Card_Data>();
        m_Abandon     = new List<Card_Data>();
        m_Disapper    = new List<Card_Data>();

        m_ingameManager = ingameManager;
    }

    public void Enter_Dungeon() // 한판이 시작될때의 덱을 세팅해준다.
    {
        // 유저 덱 클리어
        m_curGameDeck.Clear();
        List<Card_Data> c_List = InGame.Deck.Get_AllCardDataAtDeck();

        // 유저 덱으로 카드 생성(하나의 방을 들어가기 전에 덱으로 구성된 카드 덱을 만듬)
        foreach(Card_Data c_Data in c_List)
        {
            Card_Data newCard_Data = new Card_Data(c_Data);
            m_curGameDeck.Add(newCard_Data);
        }

        // 유저 드로우 카드 목록에 넣어줌
        foreach(Card_Data c_Data in m_curGameDeck)
        {
            m_Draw.Add(c_Data);
        }

        // 덱 섞기
        m_Draw.Shuffle();

        InGame.UIEventController.Set_DrawText(m_Draw.Count);
        InGame.UIEventController.Set_AbandonedText(m_Abandon.Count);
        InGame.UIEventController.Set_DisappearText(m_Disapper.Count);
        return;
    }

    public void Draw()
    {
        if(m_Draw.Count == 0)
        {
            Reset_DrawAtAbandon();
            m_Draw.Shuffle();

            if(m_Draw.Count == 0)
            {
                // 카드가 전부 없어진 상황이라면...
                return;
            }
        }

        Card_Data c_Data = m_Draw[m_Draw.Count - 1];
        m_Draw.RemoveAt(m_Draw.Count - 1);
        m_Hand.Add(c_Data);

        // UI
        InGame.UIEventController.Draw(c_Data);
        InGame.UIEventController.Set_DrawText(m_Draw.Count);
    }

    
    void Reset_DrawAtAbandon()
    {                
        foreach(Card_Data c_Data in m_Abandon)
        {
            m_Draw.Add(c_Data);
        }

        m_Abandon.Clear();

        InGame.UIEventController.Set_DrawText(m_Draw.Count);
        InGame.UIEventController.Set_AbandonedText(m_Abandon.Count);
    }

    public void Abandon_All()
    {
        for(int i = m_Hand.Count - 1; i >= 0; --i)
        {
            if(m_Hand[i] != null)
            {
                Abandon(m_Hand[i]);
            }
        }
    }

    public void Abandon(Card_Data c_Data)
    {        
        m_Abandon.Add(c_Data);
        int index = m_Hand.FindIndex(x => x == c_Data);
        m_Hand.RemoveAt(index);

        // UI
        InGame.UIEventController.Abandoned(c_Data);
        InGame.UIEventController.Set_AbandonedText(m_Abandon.Count);
    }

    public void Disappear(Card_Data c_Data)
    {
        m_Disapper.Add(c_Data);

        // UI
        InGame.UIEventController.Set_DisappearText(m_Disapper.Count);
    }
}
