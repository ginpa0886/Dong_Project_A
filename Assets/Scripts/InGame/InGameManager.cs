using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager
{
    private InGameDeckManager ingameDeckManager;
    private InGameCardManager ingameCardManager;
    private InGamePlayManager ingamePlayManager;
    private InGameMonsterManager ingameMonsterMonster;
    private InGameTurnManager ingameTurnManager;

    private InGameUIEventController ingameUIEventController;

    public InGameDeckManager Deck { get { return ingameDeckManager; } }
    public InGameCardManager Cards { get { return ingameCardManager; } }
    public InGamePlayManager Play { get { return ingamePlayManager; } }
    public InGameMonsterManager Monster { get { return ingameMonsterMonster; } }
    public InGameTurnManager Turn { get { return ingameTurnManager; } }

    public InGameUIEventController UIEventController { get { return ingameUIEventController; } }

    public InGameManager()
    {
        ingameDeckManager = new InGameDeckManager(this);
        ingameCardManager = new InGameCardManager(this);
        ingamePlayManager = new InGamePlayManager(this);
        ingameMonsterMonster = new InGameMonsterManager(this);
        ingameTurnManager = new InGameTurnManager(this);

        ingameUIEventController = new InGameUIEventController();
    }

    public void Start_Game()
    {
        Play.Init_UserCharacterData();   // �⺻ ���� ���� �� �⺻ ī�� ����

        Start_Battle();
    }

    public void Start_Battle()
    {
        Cards.Init_GameDeck();      // �� ����(ī��)
        Turn.Set_TurnType(_Enums.TURN_TYPE.PLAYER);
    }
}
