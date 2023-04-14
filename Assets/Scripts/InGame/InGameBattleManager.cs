using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBattleManager
{
    // 캐릭터들의 전투에 대해서 관리하는 곳

    private InGameManager m_ingameManager;
    public InGameManager InGame { get { return m_ingameManager; } }

    public InGameBattleManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
    }

    
}
