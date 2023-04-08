using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEventController
{
    #region UI_IG_FRAME
    public Action<int, int> Set_FrameHp;            // ������ Hp ����
    public Action<int> Set_FrameGold;          // ������ Gold ���� 
    public Action<string> Set_FrameInformation;   // ������ Information ����
    public Action<RectTransform> Set_PortionBackground;
    #endregion
}
