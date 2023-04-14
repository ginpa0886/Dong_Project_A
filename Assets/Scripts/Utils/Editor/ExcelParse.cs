using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class ExcelParse : Editor
{

    const string Default_Csv_Path = "/Resources/Csv/";
    const string Default_Resources_Path = "Table/";

    static string[] table_Names = { "Character", "Relic", "Card", "Monster" };

    [MenuItem("Table/CsvParser")]
    static void CsvParse_Default()
    {
        string OutputPath = string.Empty;
        for(int i = 0; i < table_Names.Length; ++i)
        {
            TextAsset data = Resources.Load("Table/" + table_Names[i]) as TextAsset;
            OutputPath = Application.dataPath + Default_Csv_Path + table_Names[i] + ".bytes";
            File.WriteAllBytes(OutputPath, data.bytes);
        }

        AssetDatabase.Refresh();
    }
}
