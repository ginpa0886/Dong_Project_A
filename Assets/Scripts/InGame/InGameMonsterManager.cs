using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMonsterManager
{
    private InGameManager m_ingameManager;
    public InGameManager InGame { get { return m_ingameManager; } }

    public InGameMonsterManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
    }
}
