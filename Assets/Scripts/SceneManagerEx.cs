using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : MonoBehaviour
{
    public _Enums.SCENE_TYPE m_sceneType;
    static GameManager gm;

    [SerializeField] GameObject windowManager_Prefab;

    void Start()
    {
        if(gm == null)
        {            
            gm = GameManager.Instance;

            if(gm.Win == null)
            {
                GameObject go = Instantiate(windowManager_Prefab).gameObject;
                go.name = "WindowManager";
                go.transform.SetParent(gm.gameObject.transform);
                gm.Win = go.GetComponent<WindowController>();
            }
        }

        gm.Scene = this;
        Open_SceneByType(m_sceneType);
    }

    void Open_SceneByType(_Enums.SCENE_TYPE type)
    {
        switch (type)
        {
            case _Enums.SCENE_TYPE.INGAME:
                GameManager.Instance.Win.Open(WIN_ID.INGAME_FRAME_WIN, true);
                GameManager.Instance.Win.Open(WIN_ID.INGAME_SELECT1_WIN);
                GameManager.Instance.Win.Open(WIN_ID.INGAME_PLAYERUI_WIN);
                break;
        }
    }


    void Close_ScneneByType(_Enums.SCENE_TYPE type)
    {

    }

    public void Set_SceneByTpye(_Enums.SCENE_TYPE type)
    {
        Close_ScneneByType(m_sceneType);

        GameManager.Instance.Win.Close_All();
        SceneManager.LoadScene((int)type);
    }
}
