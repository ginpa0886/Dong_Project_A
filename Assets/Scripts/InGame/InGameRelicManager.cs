using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRelicManager
{
    private InGameManager m_ingameManager;
    public InGameManager InGame { get { return m_ingameManager; } }

    public InGameRelicManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
    }
}
