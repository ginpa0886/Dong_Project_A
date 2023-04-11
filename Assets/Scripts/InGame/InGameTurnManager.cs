using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTurnManager
{
    private InGameManager m_ingameManager;
    private _Enums.TURN_TYPE m_TurnType;

    public InGameManager InGame { get { return m_ingameManager; } }
    _Enums.TURN_TYPE TurnType { get { return m_TurnType; } set => m_TurnType = value; }

    public InGameTurnManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
    }

    public void Set_TurnType(_Enums.TURN_TYPE turnType)
    {
        TurnType = turnType;

        switch (TurnType)
        {
            case _Enums.TURN_TYPE.PLAYER:
                Start_UserTurn();
                break;
            case _Enums.TURN_TYPE.MONSTER:
                Start_MonsterTurn();
                break;
        }
    }

    public void TurnOver()
    {
        Set_TurnType(TurnType == _Enums.TURN_TYPE.PLAYER ? _Enums.TURN_TYPE.MONSTER : _Enums.TURN_TYPE.PLAYER);
    }

    #region USER
    void Start_UserTurn()
    {
        InGame.Play.Set_StartBattle();  // 에너지와 드로우 카운트를 채운다.
    }

    public bool Request_UserPlaySomething()
    {
        if(TurnType == _Enums.TURN_TYPE.PLAYER)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region MONSTER
    void Start_MonsterTurn()
    {
        InGame.Cards.Abandon_All(); // 카드 전부 버리기
    }
    #endregion
}
