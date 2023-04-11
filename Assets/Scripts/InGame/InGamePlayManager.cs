using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePlayManager
{
    private InGameManager m_ingameManager;

    public InGameManager InGame { get { return m_ingameManager; }}

    public InGamePlayManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
    }

    _Enums.CHARACTER_TYPE m_CharacterType;

    Character_Data d_UserCharacterData;

    public _Enums.CHARACTER_TYPE CharacterType { get { return m_CharacterType; } set => m_CharacterType = value; } 
    public int Energy { get { return d_UserCharacterData.Get_Energy; } set => d_UserCharacterData.Set_Energy = value; }
    public int DrawCnt { get { return d_UserCharacterData.Get_DrawCnt; } set => d_UserCharacterData.Set_DrawCnt = value; }

    int m_current_energy;
    int m_total_energy;

    int m_current_drawcnt;
    int m_total_drawcnt;

    int CURRENT_ENERGY {
        get { return m_current_energy; }
        set 
        {
            m_current_energy = value;
            InGame.UIEventController.Set_CostText($"{m_current_energy} / {TOTAL_ENERGY}");
        }
    }
    int TOTAL_ENERGY {
        get { return m_total_energy; }
        set 
        {
            m_total_energy = value;
            InGame.UIEventController.Set_CostText($"{CURRENT_ENERGY} / {m_total_energy}");
        } 
    }

    int CURRENT_DRAW
    {
        get { return m_current_drawcnt; }
        set
        {
            m_current_drawcnt = value;

            for(int i = 0; i < value; ++i)
            {
                InGame.Cards.Draw();
            }
            m_current_drawcnt = 0;
        }
    }
    int TOTAL_DRAW
    {
        get { return m_total_drawcnt; }
        set
        {
            m_total_drawcnt = value;
        }
    }

    public void Init_UserCharacterData()
    {
        Character_Data u_Data = GameManager.Instance.Table.Get_TryCharacterDataByType(_Enums.CHARACTER_TYPE.IRON);
        d_UserCharacterData = new Character_Data(u_Data);

        foreach (uint c_UID in u_Data.Get_Cards)
        {
            InGame.Deck.Add_CardAtDeckByID(c_UID);
        }
    }

    public void Set_StartBattle()
    {        
        TOTAL_ENERGY = Energy;
        FillMax_CurrentEnergy();
        
        TOTAL_DRAW = DrawCnt;
        FillMax_CurrentDrawCnt();
    }
    
    public void FillMax_CurrentEnergy()
    {
        CURRENT_ENERGY = TOTAL_ENERGY;
    }

    public void FillMax_CurrentDrawCnt()
    {
        CURRENT_DRAW = TOTAL_DRAW;
    }   

    public bool Request_UseEnergy(int cost)
    {
        if(CURRENT_ENERGY >= cost)
        {
            CURRENT_ENERGY += (-cost);
            return true;
        }
        return false;
    }

    public void Fill_CurrentEnergy(int add)
    {
        CURRENT_ENERGY = add;
    }
}
