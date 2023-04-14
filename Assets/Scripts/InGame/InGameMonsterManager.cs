using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMonsterManager
{
    const int MAX_MONSTER_CNT = 4;

    private InGameManager m_ingameManager;
    private Character_Monster[] m_Monsters;
    public InGameManager InGame { get { return m_ingameManager; } }

    public InGameMonsterManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
        m_Monsters = new Character_Monster[MAX_MONSTER_CNT];
    }

    public void Enter_Dungeon()
    {
        Character_Monster[] c_Monster = InGame.Map.Get_MonsterDatas();

        if(c_Monster == null)
        {
            return;
        }

        // reset monster array
        for (int i = 0; i < MAX_MONSTER_CNT; ++i)
        {
            m_Monsters[i] = null;
        }

        // Set Monster Array
        for(int i = 0; i < c_Monster.Length; ++i)
        {
            m_Monsters[i] = c_Monster[i];
        }
    }
}
