using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI_Base
{
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

    private void Start()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(Gameobjects));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        #region Sign UI Contorl Fnc
        UI_Lobby_Controller.Contorl_SelectContainer -= Contorl_SelectContainer;
        UI_Lobby_Controller.Contorl_SelectContainer += Contorl_SelectContainer;
        #endregion

        #region Default Setting
        GetGameObject((int)Gameobjects.Select_Container).gameObject.SetActive(false);
        #endregion

        #region Event Setting
        GetButton((int)Buttons.L_GameStart).onClick.AddListener(() => UI_Lobby_Controller.Contorl_SelectContainer(true));
        GetButton((int)Buttons.L_Dictionary).onClick.AddListener(() => UI_Lobby_Controller.Contorl_SelectContainer(true));
        GetButton((int)Buttons.L_Setting).onClick.AddListener(() => UI_Lobby_Controller.Contorl_SelectContainer(true));

        GetButton((int)Buttons.back_Btn).onClick.AddListener(() => UI_Lobby_Controller.Contorl_SelectContainer(false));
        #endregion
    }

    #region UI Contorl

    void Contorl_SelectContainer(bool onOroff)
    {
        GetGameObject((int)Gameobjects.Select_Container).SetActive(onOroff);
    }

    #endregion
}
