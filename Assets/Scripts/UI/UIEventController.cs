using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEventController
{
    #region UI_IG_FRAME
    public Action<int, int>      Set_FrameHp;            // 프레임 Hp 설정
    public Action<int>           Set_FrameGold;          // 프레임 Gold 설정 
    public Action<string>        Set_FrameInformation;   // 프레임 Information 설정
    public Action<int>           Set_PortionCount;       // 프레임 Portion 총 활성화 개수 컨트롤    
    public Action<RectTransform> Set_PortionBackground;  // 프레인 Portion 클릭 UI 활성화
    #endregion
}
