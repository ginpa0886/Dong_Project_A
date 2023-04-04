using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI_Base
{
    #region ENUMS
    enum Texts
    {
        L_GameStart_Text,
        L_Dictionary_Text,
        L_Setting_Text,
        L_Exit_Text,
    }

    enum Gameobjects
    {
        Select_Container,
    }

    enum Buttons
    {
        L_GameStart,
        L_Dictionary,
        L_Setting,
        L_Exit,

        back_Btn,
    }

    enum Images
    {

    }

    #endregion

    UI_Select_Button_Prefabs[] m_Sbps;

    private void Start()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(Gameobjects));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        #region Sign UI Contorl Fnc
        UI_Lobby_Controller.Instance.Contorl_SelectContainer -= Contorl_SelectContainer;
        UI_Lobby_Controller.Instance.Contorl_SelectContainer += Contorl_SelectContainer;
        #endregion

        #region Default Setting
        {
            GameObject go = GetGameObject((int)Gameobjects.Select_Container);
            Util.FindAndSetComponent<UI_Select_Button_Prefabs>(go, ref m_Sbps);
        }

        GetGameObject((int)Gameobjects.Select_Container).gameObject.SetActive(false);
        #endregion

        #region Event Setting
        GetButton((int)Buttons.L_GameStart).onClick.AddListener(() => {
            UI_Lobby_Controller.Instance.Contorl_SelectContainer(true);
            Set_SelectButtonPrefabsData(GetButton((int)Buttons.L_GameStart).gameObject);
        });
        GetButton((int)Buttons.L_Dictionary).onClick.AddListener(() => {
            UI_Lobby_Controller.Instance.Contorl_SelectContainer(true);
            Set_SelectButtonPrefabsData(GetButton((int)Buttons.L_Dictionary).gameObject);
        });
        GetButton((int)Buttons.L_Setting).onClick.AddListener(() => {
            UI_Lobby_Controller.Instance.Contorl_SelectContainer(true);
            Set_SelectButtonPrefabsData(GetButton((int)Buttons.L_Setting).gameObject);
        });

        GetButton((int)Buttons.back_Btn).onClick.AddListener(() => UI_Lobby_Controller.Instance.Contorl_SelectContainer(false));
        #endregion
    }

    #region UI Contorl

    void Contorl_SelectContainer(bool onOroff)      // Set Select Button Prefab UI Open
    {
        GetGameObject((int)Gameobjects.Select_Container).SetActive(onOroff);
    }

    void Set_SelectButtonPrefabsData(GameObject go) // Set Select Button Prefab Datas
    {
        Sbp_Data s_Data = go.GetComponent<Sbp_Data>();

        for(int i = 0; i < 3; ++i)
        {
            m_Sbps[i].Init(s_Data.s_Data[i]);
        }
    }
    #endregion
}
