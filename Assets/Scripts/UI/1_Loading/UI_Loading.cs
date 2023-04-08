using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Loading : UI_Base
{
    #region ENUM
    enum Texts
    {

    }

    enum GameObjects
    {

    }

    enum Images
    {

    }

    enum Buttons
    {
        Next_Btn,
    }
    #endregion

    void Start()
    {
        if(IsInit() == false)
        {
            Bind();
        }
    }

    void Bind()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.Next_Btn).onClick.AddListener(() =>
        {
            GameManager.Instance.Scene.Set_SceneByTpye(_Enums.SCENE_TYPE.LOBBY);            
        });
    }
}
