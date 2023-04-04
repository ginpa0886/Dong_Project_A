using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Select_Button_Prefabs : UI_Base
{
    #region ENUMS
    enum Texts
    {
        Sbp_Title_Text,
        Sbp_Des_Text,
    }

    enum GameObjects
    {

    }

    enum Images
    {
        Sbp_Img,
    }

    enum Buttons
    {
        Sbp_Btn,
    }
    #endregion

    public void Init(Sbp_Data_t data)
    {
        if (!IsInit())
        {
            Bind();
        }

        Set_SbpData(data);
    }

    void Bind()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
    }

    void Set_SbpData(Sbp_Data_t data)
    {
        GetText((int)Texts.Sbp_Title_Text).text = data.title;
        GetImage((int)Images.Sbp_Img).sprite = data.sprites;
        GetText((int)Texts.Sbp_Des_Text).text = data.desc;
        this.gameObject.GetComponent<Image>().color = data.color;

        // set event to open btn ui
        GetButton((int)Buttons.Sbp_Btn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.Sbp_Btn).onClick.AddListener(() => GameManager.Instance.Win.Open(data.windowId));
    }
}
