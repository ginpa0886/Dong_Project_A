using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region DATA
public class Character_Data
{
    string c_name;
    int c_hp;
    int c_gold;
    string c_desc;
    Relic_Data c_default_Relic;

    public Character_Data(string name, int hp, int gold, string desc, Relic_Data default_relic)
    {
        c_name = name;
        c_hp = hp;
        c_gold = gold;
        c_desc = desc;
        c_default_Relic = default_relic;
    }

    public string Get_Name { get { return c_name; } }
    public int Get_HP { get { return c_hp; } }
    public int Get_Gold { get { return c_gold; } }
    public string Get_Desc { get { return c_desc; } }
    public Relic_Data Get_RelicData { get { return c_default_Relic; } }
}

public class Relic_Data
{
    uint r_UID;

    _Enums.RELIC_DICTIONARY_TYPE r_Dictionary_Type;
    string r_name;
    string r_image;
    _Enums.RELIC_INGAME_TYPE r_Ingame_Type;

    string r_ptext;
    string r_desc;
    string r_common_desc1;
    string r_common_desc2;

    public Relic_Data(uint UID, _Enums.RELIC_DICTIONARY_TYPE dictionary_type, 
                      string name, string image, _Enums.RELIC_INGAME_TYPE ingame_type, 
                      string ptext, string desc, string common_desc1, string common_desc2)
    {
        r_UID = UID;

        r_Dictionary_Type = dictionary_type;
        r_name = name;
        r_image = image;
        r_Ingame_Type = ingame_type;

        r_ptext = ptext;
        r_desc = desc;
        r_common_desc1 = common_desc1;
        r_common_desc2 = common_desc2;
    }

    public uint Get_UID { get { return r_UID; } }
    public _Enums.RELIC_DICTIONARY_TYPE Get_DictionaryType { get { return r_Dictionary_Type; } }
    public string Get_Name { get { return r_name; } }
    public string Get_Image { get { return r_image; } }
    public _Enums.RELIC_INGAME_TYPE Get_IngameType { get { return r_Ingame_Type; } }
    public string Get_PText { get { return r_ptext; } }
    public string Get_Desc { get { return r_desc; } }
    public string Get_CommonDesc1 { get { return r_common_desc1; } }
    public string Get_COmmonDesc2 { get { return r_common_desc2;} }
}

#endregion

public class DataManager
{
    Dictionary<uint, Relic_Data> d_Relic = new Dictionary<uint, Relic_Data>();
    Dictionary<_Enums.CHARACTER_TYPE, Character_Data> d_Character = new Dictionary<_Enums.CHARACTER_TYPE, Character_Data>();

    public DataManager()
    {
        Init_RelicData();
        Init_CharacterData();
    }
    void Init_RelicData()
    {
        // �ӽ� ������ ���� ---------------------------------------------------------------------------------
        uint r_UID1 = 10001;
        _Enums.RELIC_DICTIONARY_TYPE r_Dictionary_type1 = _Enums.RELIC_DICTIONARY_TYPE.START;
        string r_name1 = "���� ����";
        string r_image1 = "snake";
        _Enums.RELIC_INGAME_TYPE r_Ingame_type1 = _Enums.RELIC_INGAME_TYPE.MAX;

        string r_ptext1 = "ȭ���� �� ������ ��������ϴ�.\n�پ ����� ���� ����ɲ��� ��Ÿ���ϴ�";
        string r_desc1 = "ù �Ͽ��� <color=blue>2</color>���� ī�带 �߰��� �̽��ϴ�.";
        string r_common_desc1 = "";
        string r_common_desc2 = "";
        Relic_Data relic1 = new Relic_Data(r_UID1, r_Dictionary_type1, r_name1, r_image1, r_Ingame_type1,
                                            r_ptext1, r_desc1, r_common_desc1, r_common_desc2);

        uint r_UID2 = 10003;
        _Enums.RELIC_DICTIONARY_TYPE r_Dictionary_type2 = _Enums.RELIC_DICTIONARY_TYPE.START;
        string r_name2 = "��Ÿ�� ����";
        string r_image2 = "fire_blood";
        _Enums.RELIC_INGAME_TYPE r_Ingame_type2 = _Enums.RELIC_INGAME_TYPE.MAX;

        string r_ptext2 = "����� ���� ����� �Ǵ� ������\n�ʴ� �г�� Ÿ������ �ֽ��ϴ�.";
        string r_desc2 = "���� ����� ü���� <color=skyblue>6</color> ȸ���մϴ�.";
        string r_common_desc3 = "";
        string r_common_desc4 = "";
        Relic_Data relic2 = new Relic_Data(r_UID2, r_Dictionary_type2, r_name2, r_image2, r_Ingame_type2,
                                            r_ptext2, r_desc2, r_common_desc3, r_common_desc4);
        // -------------------------------------------------------------------------------------------------
        
        d_Relic.Add(relic1.Get_UID, relic1);
        d_Relic.Add(relic2.Get_UID, relic2);
    }

    void Init_CharacterData()
    {
        // �ӽ� ������ ���� ---------------------------------------------------------------------------------
        string char1_desc = "���̾�Ŭ������ ��Ƴ��� �����Դϴ�.\n�Ǹ������� ����ϱ� ���� ��ȥ�� �Ⱦҽ��ϴ�.";
        Relic_Data relic1 = Get_TryRelicData(10003);
        Character_Data char1 = new Character_Data("���̾� Ŭ����", 80, 99, char1_desc, relic1);

        string char2_desc = "�Ȱ� ���뿡�� �� ġ������ ��ɲ��Դϴ�.\n�ܰ˰� ������ ������ �ڸ��մϴ�.";
        Relic_Data relic2 = Get_TryRelicData(10001);
        Character_Data char2 = new Character_Data("���Ϸ�Ʈ", 70, 99, char2_desc, relic2);
        // ------------------------------------------------------------------------------------------------

        d_Character.Add(_Enums.CHARACTER_TYPE.IRON, char1);
        d_Character.Add(_Enums.CHARACTER_TYPE.SILENT, char2);
    }

    public Relic_Data Get_TryRelicData(uint UID)
    {
        if(d_Relic.TryGetValue(UID, out Relic_Data data) == true)
        {
            return data;
        }
        Debug.Log($"Relic Data is Missing : Relic UID is {UID}");
        return null;
    }

    public Character_Data Get_TryCharacterDataByType(_Enums.CHARACTER_TYPE characterType)
    {
        if(d_Character.TryGetValue(characterType, out Character_Data data) == true)
        {
            return data;
        }

        Debug.Log($"Character data is msissing! : Character type is {characterType}");
        return null;
    }
}
