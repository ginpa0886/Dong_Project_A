using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDeckManager
{
    // ���� ���۽� �������־���� ������
    private InGameManager m_ingameManager;
    InGameManager InGame { get { return m_ingameManager;} }

 
    Dictionary<uint, List<Card_Data>> m_userDeckDictionary;
    List<Card_Data> m_UserDeckList;

    public InGameDeckManager(InGameManager ingameManager)
    {
        m_userDeckDictionary = new Dictionary<uint, List<Card_Data>>();
        m_UserDeckList       = new List<Card_Data>();

        m_ingameManager = ingameManager;
    }

    #region CARD
    public void Add_CardAtDeckByID(uint c_ID, bool isEnforced = false)
    {
        Card_Data c_Data = GameManager.Instance.Table.Get_TryCardDataByType(c_ID);
        Card_Data newCard_Data = new Card_Data(c_Data, isEnforced);

        m_UserDeckList.Add(newCard_Data);
        return;  
    }

    public void Remove_CardAtDeck(Card_Data c_Data) // ������ ī�� �����ϱ�
    {
        m_UserDeckList.Remove(c_Data);
        return;
    }

    public List<Card_Data> Get_AllCardDataAtDeck()  // ������ ���� �ִ� �� ī�� ������ �ޱ� - ������ ������ ��
    {
        return m_UserDeckList;
    }

    public void Enforce_CardData(Card_Data c_Data)  // ī�� ��ȭ�ϱ�
    {
        Card_Data card = m_UserDeckList.Find(x => x == c_Data);
        card.Set_IsEnforce = true;
        return;
    }
    #endregion
}
