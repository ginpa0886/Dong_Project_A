using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WIN_ID
{
    CHARACTER_SELECT_WIN,   // 캐릭터 선택
    DAILY_CHALLENGE_WIN,    // 일일 도전
    CUSTOM_MODE_WIN,        // 커스텀 모드
     
    CARD_COLLECTION_WIN,    // 카드 수집
    RELIC_COLLECTION_WIN,   // 유물 수집
    PORTION_COLLECTION_WIN, // 포션 수집
   
    GAME_SETTING_WIN,       // 게임 설정
    INPUT_SETTING_WIN,      // 입력 설정
    CREDIT_WIN,             // 크레딧

    INGAME_FRAME_WIN,       // 인게임 프레임 
    INGAME_REWARD_WIN,      // 인게임 리워드
    INGAME_SELECT1_WIN,     // 인게임 선택창1 - 짧은 대화 선택지
    INGAME_SELECT2_WIN,     // 인게임 선택창2 - 긴대화 포함된 UI
    INGAME_SHOP_WIN,        // 인게임 상점
    INGAME_PLAYERUI_WIN,    // 인게임 플레이어창
    INGAME_MAP_WIN,         // 인게임 맵창
    
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

    [SerializeField] Win_Struct_Data[] m_winobj;    // 이 부분에 대해서는 리소스를 읽어와서 만드는 형식으로 진행해도 좋았을 것 같음 + 나중에 어셋 번들 형식으로 연결해보도록 하자
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
