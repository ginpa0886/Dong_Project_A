using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCardManager
{
    private InGameManager m_ingameManager;
    InGameManager InGame { get { return m_ingameManager; } }
    
    // ingame ���ο��� ī�� ���� ������ ������
    List<Card_Data> m_curGameDeck;

    List<Card_Data> m_Draw;     // ��ο� ī�� ���
    List<Card_Data> m_Hand;     // �ڵ� ī�� ���
    List<Card_Data> m_Abandon;  // ������ ī�� ���
    List<Card_Data> m_Disapper; // �Ҹ�� ī�� ���

    public InGameCardManager(InGameManager ingameManager)
    {
        m_curGameDeck = new List<Card_Data>();
        m_Draw        = new List<Card_Data>();
        m_Hand        = new List<Card_Data>();
        m_Abandon     = new List<Card_Data>();
        m_Disapper    = new List<Card_Data>();

        m_ingameManager = ingameManager;
    }

    public void Enter_Dungeon() // ������ ���۵ɶ��� ���� �������ش�.
    {
        // ���� �� Ŭ����
        m_curGameDeck.Clear();
        List<Card_Data> c_List = InGame.Deck.Get_AllCardDataAtDeck();

        // ���� ������ ī�� ����(�ϳ��� ���� ���� ���� ������ ������ ī�� ���� ����)
        foreach(Card_Data c_Data in c_List)
        {
            Card_Data newCard_Data = new Card_Data(c_Data);
            m_curGameDeck.Add(newCard_Data);
        }

        // ���� ��ο� ī�� ��Ͽ� �־���
        foreach(Card_Data c_Data in m_curGameDeck)
        {
            m_Draw.Add(c_Data);
        }

        // �� ����
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
                // ī�尡 ���� ������ ��Ȳ�̶��...
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
