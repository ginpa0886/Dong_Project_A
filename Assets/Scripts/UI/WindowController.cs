using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WIN_ID
{
    CHARACTER_SELECT_WIN,   // ĳ���� ����
    DAILY_CHALLENGE_WIN,    // ���� ����
    CUSTOM_MODE_WIN,        // Ŀ���� ���
     
    CARD_COLLECTION_WIN,    // ī�� ����
    RELIC_COLLECTION_WIN,   // ���� ����
    PORTION_COLLECTION_WIN, // ���� ����
   
    GAME_SETTING_WIN,       // ���� ����
    INPUT_SETTING_WIN,      // �Է� ����
    CREDIT_WIN,             // ũ����

    INGAME_FRAME_WIN,       // �ΰ��� ������ 
    INGAME_REWARD_WIN,      // �ΰ��� ������
    INGAME_SELECT1_WIN,     // �ΰ��� ����â1 - ª�� ��ȭ ������
    INGAME_SELECT2_WIN,     // �ΰ��� ����â2 - ���ȭ ���Ե� UI
    INGAME_SHOP_WIN,        // �ΰ��� ����
    INGAME_PLAYERUI_WIN,    // �ΰ��� �÷��̾�â
    INGAME_MAP_WIN,         // �ΰ��� ��â
    
    MAX_WIN,
}

[System.Serializable]
public struct Win_Struct_Data
{
    public WIN_ID m_WindowId;
    public GameObject m_WindowObj;
}

public class WindowController : MonoBehaviour
{
    const int START_SORTORDER = 2;
    const int FIX_SORTORDER = 100;

    [SerializeField] Win_Struct_Data[] m_winobj;    // �� �κп� ���ؼ��� ���ҽ��� �о�ͼ� ����� �������� �����ص� ������ �� ���� + ���߿� ��� ���� �������� �����غ����� ����
    Dictionary<WIN_ID, UnityEngine.GameObject> m_windowDic = new Dictionary<WIN_ID, UnityEngine.GameObject>();
    BaseWindow[] m_baseWindow = new BaseWindow[(int)WIN_ID.MAX_WIN]; 
    
    int w_sortOrder;

    void Awake()
    {
        w_sortOrder = START_SORTORDER;
        // init
        {
            for(int i = 0; i <  m_winobj.Length; ++i)
            {
                // if don' t have value add win obj
                if(m_windowDic.TryGetValue(m_winobj[i].m_WindowId, out GameObject value) == false)
                {
                    m_windowDic.Add(m_winobj[i].m_WindowId, m_winobj[i].m_WindowObj);
                }                
            }
        }
    }

    public void Open(WIN_ID window_id, bool fixUI = false)
    {
        BaseWindow basewin = null;

        // child check                
        if(!Get_BaseWinInstance(window_id, out basewin))
        {
            if (!m_windowDic.TryGetValue(window_id, out GameObject go))
            {
                Debug.Log("No UI Value Set UI Id And UI Prefabs");
                return;
            }

            BaseWindow b = go.GetComponent<BaseWindow>();
            if (b == null)
            {
                Debug.Log("Please Attach BaseWindow Scripts");
                return;
            }

            GameObject obj = Instantiate(go).gameObject;
            obj.name = b.name;
            obj.transform.SetParent(this.gameObject.transform);

            BaseWindow win = obj.GetComponent<BaseWindow>();
            Set_BaseWinInstance(win);
            win.WindowID = window_id;
            basewin = win;
        }

        Set_BaseWindowSortLayer(basewin, fixUI);   // Canvas SortLayer Control
        basewin.gameObject.SetActive(true);
        basewin.Open();
    }    

    bool Get_BaseWinInstance(WIN_ID window_id, out BaseWindow win)
    {
        for(int i = 0; i < m_baseWindow.Length; ++i){
            if(m_baseWindow[i] != null && m_baseWindow[i].WindowID == window_id)
            {
                win = m_baseWindow[i];
                return true;
            }
        }
        win = null;
        return false;
    }

    void Set_BaseWinInstance(BaseWindow win)
    {
        for(int i = 0; i < m_baseWindow.Length; ++i)
        {
            if(m_baseWindow[i] == null)
            {
                m_baseWindow[i] = win;
                return;
            }
        }
        Debug.Log("base win array need more count");
        return;
    }

    void Set_BaseWindowSortLayer(BaseWindow win, bool fixUI = false)
    {
        if(fixUI == true)
        {
            win.gameObject.GetComponent<Canvas>().sortingOrder = FIX_SORTORDER;
            ++w_sortOrder;
            return;
        }

        win.gameObject.GetComponent<Canvas>().sortingOrder = w_sortOrder++;
    }

    public void Close(WIN_ID window_id)
    {
        for(int i = 0; i < m_baseWindow.Length; ++i)
        {
            if (m_baseWindow[i] != null && m_baseWindow[i].WindowID == window_id)
            {
                m_baseWindow[i].gameObject.SetActive(false);
                --w_sortOrder;
                return;
            }
        }        
    }

    public void Close_All()
    {
        for(int i = 0; i < m_baseWindow.Length; ++i)
        {
            if(m_baseWindow[i] != null)
            {
                m_baseWindow[i].gameObject.SetActive(false);                
            }
        }

        w_sortOrder = START_SORTORDER;
    }
}
