using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RelicCollection : BaseWindow
{
    #region ENUM
    enum Texts
    {

    }

    enum GameObjects
    {

    }

    enum Buttons
    {
        Back_Btn,
    }

    enum Images
    {

    }
    #endregion

    public override void Open()
    {
        base.Open();
        if (IsInit() == false)
        {
            Bind();
        }
    }

    void Bind()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.Back_Btn).onClick.AddListener(Close);
    }

    public override void Close()
    {
        base.Close();
    }
}
