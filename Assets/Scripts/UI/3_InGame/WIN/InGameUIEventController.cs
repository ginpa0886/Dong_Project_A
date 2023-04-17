using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InGameUIEventController
{
    #region Player UI
    public Action<int>    Set_DrawText;
    public Action<string> Set_CostText;
    public Action<int>    Set_DisappearText;
    public Action<int>    Set_AbandonedText;

    public Action<Card_Data> Draw;
    public Action<Card_Data> Rebirth;
    public Action<Card_Data> Abandoned;
    #endregion

    #region MAP UI
    public Action Set_MapByData;
    #endregion
}
