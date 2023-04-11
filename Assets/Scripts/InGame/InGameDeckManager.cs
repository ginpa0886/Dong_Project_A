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

        {
            if (m_userDeckDictionary.TryGetValue(c_ID, out List<Card_Data> c_List) == true)
            {
                c_List.Add(newCard_Data);
                return;
            }

            // ���ο� ī�� �����Ͷ�� ���ο� ����Ʈ ����
            List<Card_Data> newCard_List = new List<Card_Data>();
            newCard_List.Add(newCard_Data);

            // New Card UID�� New List�� Dictionary�� �߰�
            m_userDeckDictionary.Add(newCard_Data.Get_UID, newCard_List);
            return;
        }        
    }

    public void Remove_CardAtDeck(Card_Data c_Data) // ������ ī�� �����ϱ�
    {
        m_UserDeckList.Remove(c_Data);
        return;

        {
            uint c_UID = c_Data.Get_UID;

            if (m_userDeckDictionary.TryGetValue(c_UID, out List<Card_Data> c_List) == false)
            {
                Debug.Log($"there is no Card in your deck! card ID is : {c_UID} check it");
                return;
            }

            // List�ȿ����� Remove�̹Ƿ� �˻� �ð� ���⵵�� ���� �����Ƿ� ������
            c_List.Remove(c_Data);
            return;
        }        
    }

    public List<Card_Data> Get_AllCardDataAtDeck()  // ������ ���� �ִ� �� ī�� ������ �ޱ�
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
