using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterSelect : BaseWindow
{
    #region ENUMS
    enum Texts
    {
        Character_Name_Text,
        HP_Text,
        Gold_Text,
        Char_Des_Text,

        Diff_Text,
        Diff_desc_Text,
    }

    enum GameObjects
    {
        Relic_Container,
    }

    enum Buttons
    {
        Back_Btn,

        Iron_Btn,
        Silent_Btn,

        Diff_Left_Btn,
        Diff_Right_Btn,

        Start_Btn,
    }

    enum Images
    {
        HP_Image,
        Gold_Image,
    }
    #endregion

    public override void Open()
    {
        if(IsInit() == false)
        {
            Bind();
        }

        Init();
    }

    void Bind()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.Back_Btn).onClick.AddListener(Close);
        GetButton((int)Buttons.Iron_Btn).onClick.AddListener(() => 
        {
            Character_Data c_Data = GameManager.Instance.Table.Get_TryCharacterDataByType(_Enums.CHARACTER_TYPE.IRON);
            Set_CharacterData(c_Data);
        });
        GetButton((int)Buttons.Silent_Btn).onClick.AddListener(() =>
        {
            Character_Data c_Data = GameManager.Instance.Table.Get_TryCharacterDataByType(_Enums.CHARACTER_TYPE.SILENT);
            Set_CharacterData(c_Data);
        });

        GetButton((int)Buttons.Diff_Left_Btn).onClick.AddListener(() =>
        {
            Set_LevelBtnEvent(-1);
            Set_DiffcultDescText();
        });
        GetButton((int)Buttons.Diff_Right_Btn).onClick.AddListener(() =>
        {
            Set_LevelBtnEvent(1);
            Set_DiffcultDescText();
        });

        GetButton((int)Buttons.Start_Btn).onClick.AddListener(() =>
        {
            GameManager.Instance.Scene.Set_SceneByTpye(_Enums.SCENE_TYPE.INGAME);
        });
    }

    #region Default
    _Enums.CHARACTER_TYPE m_CurCharacter;   // default Character Type
    void Init()
    {
        m_CurCharacter = _Enums.CHARACTER_TYPE.IRON;
        Diff_Level = MIN_DIFF_LEVEL;
    }
    #endregion

    #region Character Data 

    void Set_CharacterData(Character_Data c_Data)
    {
        // Character Data Set
        GetText((int)Texts.Character_Name_Text).text = c_Data.Get_Name;
        GetText((int)Texts.HP_Text).text = $"체력 : {c_Data.Get_HP} / {c_Data.Get_HP}";
        GetText((int)Texts.Gold_Text).text = $"골드 : {c_Data.Get_Gold}";
        GetText((int)Texts.Char_Des_Text).text = c_Data.Get_Desc;

        // Relic Data Set
        Character_Relic_Prefabs c_RelicPrefab = GetGameObject((int)GameObjects.Relic_Container).transform.GetChild(0).gameObject.GetComponent<Character_Relic_Prefabs>();
        c_RelicPrefab.Set_Data(c_Data.Get_RelicData.Get_Name, null, c_Data.Get_RelicData.Get_Desc);
    }

    #endregion

    #region Level Data
    const int MIN_DIFF_LEVEL = 1;
    const int MAX_DIFF_LEVEL = 10;

    int Diff_Level { get; set; }
    void Set_LevelBtnEvent(int v)
    {
        Diff_Level += v;

        if(Diff_Level <= MIN_DIFF_LEVEL)
        {
            Diff_Level = MIN_DIFF_LEVEL;
        }

        if(Diff_Level >= MAX_DIFF_LEVEL)
        {
            Diff_Level = MAX_DIFF_LEVEL;
        }

        Set_DiffcultLevelText(Diff_Level);
    }

    void Set_DiffcultLevelText(int level)
    {
        GetText((int)Texts.Diff_Text).text = $"{level}";
    }

    void Set_DiffcultDescText()
    {
        // level에 따른 데이터를 불러오는 로직 필요
        string diff_data = "여기는 난이도에 따른 데이터가 들어올 텍스트입니다.";
        GetText((int)Texts.Diff_desc_Text).text = $"{diff_data}";
    }

    
    #endregion

    public override void Close()
    {
        base.Close();   
    }
}
