using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using static _Enums;

#region DATA
public class Character_Data
{
    string c_name;
    CHARACTER_TYPE c_type;
    int c_hp;
    int c_gold;
    string c_desc;
    Relic_Data c_default_Relic;

    public Character_Data(string name, CHARACTER_TYPE type, int hp, int gold, string desc, Relic_Data default_relic)
    {
        c_name = name;
        c_type = type;
        c_hp   = hp;
        c_gold = gold;
        c_desc = desc;
        c_default_Relic = default_relic;
    }

    public string Get_Name { get { return c_name; } }
    public CHARACTER_TYPE Get_Type { get { return c_type; } }
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
    public string Get_COmmonDesc2 { get { return r_common_desc2; } }
}

public class Portion_Data
{
    string p_name;
    string p_img;
    PORTION_DICTIONARY_TYPE p_dictionary_type;
    PORTION_INGAME_TYPE p_ingame_type;
    string p_desc;
    string p_common_desc1;
    string p_common_desc2;

    public Portion_Data(string name, string img, PORTION_DICTIONARY_TYPE dictionary_type, PORTION_INGAME_TYPE ingame_type,
                        string desc, string common_desc1, string common_desc2)
    {
        p_name = name;
        p_img = img;
        p_dictionary_type = dictionary_type;
        p_ingame_type = ingame_type;
        p_desc = desc;
        p_common_desc1 = common_desc1;
        p_common_desc2 = common_desc2;
    }

    public string Get_Name { get { return p_name; } }
    public string Get_Img { get { return p_img; } }
    public PORTION_DICTIONARY_TYPE Get_DictionaryType { get { return p_dictionary_type; } }
    public PORTION_INGAME_TYPE Get_IngameType { get { return p_ingame_type; } }
    public string Get_Desc { get { return p_desc; } }
    public string Get_Common_Desc1 { get { return p_common_desc1; } }
    public string Get_Common_Desc2 { get { return p_common_desc2; } }
}
#endregion

public class TableDataManager
{
    const string relic_Table_Path = "Csv/Relic";
    const string character_Table_Path = "Csv/Character";

    Dictionary<uint, Relic_Data> t_Relic = new Dictionary<uint, Relic_Data>();
    Dictionary<CHARACTER_TYPE, Character_Data> t_Character = new Dictionary<CHARACTER_TYPE, Character_Data>();

    public TableDataManager()
    {
        Parse_RelicTable(relic_Table_Path);
        Parse_CharacterTable(character_Table_Path);
    }

    string Get_CsvParseStringByFilePath(string f_Path)
    {
        TextAsset asset = Resources.Load(f_Path) as TextAsset;

        if(asset == null)
        {
            Debug.Log($"Try Csv Parse But filePath : '{f_Path}' is wrong or null check it!");
            return null;
        }

        return Encoding.UTF8.GetString(asset.bytes);
    }

    void Parse_RelicTable(string f_Path)
    {
        string t_Data = Get_CsvParseStringByFilePath(f_Path);

        if(t_Data == null)
        {
            return;
        }


        string[] rows = t_Data.Split('\n');

        for(int row_index = 1; row_index < rows.Length; ++row_index)
        {
            string[] column = rows[row_index].Split(',');
            if(column.Length < 2)
            {
                continue;
            }

            int column_index = 0;

            try
            {
                uint r_UID = uint.Parse(column[column_index++]);
                RELIC_DICTIONARY_TYPE r_Dictionary_Type = (RELIC_DICTIONARY_TYPE)Enum.Parse(typeof(RELIC_DICTIONARY_TYPE), column[column_index++]);
                string r_name = column[column_index++];
                string r_Image = column[column_index++];
                RELIC_INGAME_TYPE r_Ingame_Type = (RELIC_INGAME_TYPE)Enum.Parse(typeof(RELIC_INGAME_TYPE), column[column_index++]);

                string r_Ptext = column[column_index++];
                string r_Desc = column[column_index++];
                string r_Common_Desc1 = column[column_index++];
                string r_Common_Desc2 = column[column_index++];

                Relic_Data r_Data = new Relic_Data(r_UID, r_Dictionary_Type, r_name, r_Image, r_Ingame_Type,
                                                   r_Ptext, r_Desc, r_Common_Desc1, r_Common_Desc2);

                t_Relic.Add(r_Data.Get_UID, r_Data);
            }
            catch (Exception e)
            {
                Debug.LogError($"巩力 惯积! : {e}");
            }
            
        }
    }

    void Parse_CharacterTable(string f_Path)
    {
        string t_Data = Get_CsvParseStringByFilePath(f_Path);

        if (t_Data == null)
        {
            return;
        }


        string[] rows = t_Data.Split('\n');

        for (int row_index = 1; row_index < rows.Length; ++row_index)
        {
            string[] column = rows[row_index].Split(',');
            if (column.Length < 2)
            {
                continue;
            }

            int column_index = 0;

            try
            {
                string c_Name = column[column_index++];
                CHARACTER_TYPE c_Type = (CHARACTER_TYPE)Enum.Parse(typeof(CHARACTER_TYPE), column[column_index++]);
                int h_Hp = int.Parse(column[column_index++]);
                int c_Gold = int.Parse(column[column_index++]);
                string c_Desc = column[column_index++];
                int c_Relic_UID = int.Parse(column[column_index++]);

                Relic_Data c_Relic_Data = Get_TryRelicData((uint)c_Relic_UID);
                Character_Data c_Data = new Character_Data(c_Name, c_Type, h_Hp, c_Gold, c_Desc, c_Relic_Data);

                t_Character.Add(c_Data.Get_Type, c_Data);

            }
            catch(Exception e)
            {
                Debug.LogError($"巩力 惯积!! : {e}");
            }            
        }
    }



    public Relic_Data Get_TryRelicData(uint UID)
    {
        if (t_Relic.TryGetValue(UID, out Relic_Data data) == true)
        {
            return data;
        }
        Debug.Log($"Relic Data is Missing : Relic UID is {UID}");
        return null;
    }

    public Character_Data Get_TryCharacterDataByType(_Enums.CHARACTER_TYPE characterType)
    {
        if (t_Character.TryGetValue(characterType, out Character_Data data) == true)
        {
            return data;
        }

        Debug.Log($"Character data is msissing! : Character type is {characterType}");
        return null;
    }
}
