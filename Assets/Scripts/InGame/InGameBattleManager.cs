using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBattleManager
{
    // ĳ���͵��� ������ ���ؼ� �����ϴ� ��

    private InGameManager m_ingameManager;
    public InGameManager InGame { get { return m_ingameManager; } }

    public InGameBattleManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
    }

    
}
