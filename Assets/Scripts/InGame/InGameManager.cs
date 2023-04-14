using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager
{
    private InGameDeckManager ingameDeckManager;            // Player�� ������ �ִ� ���� �� ���� ���� ����
    private InGameCardManager ingameCardManager;            // Player�� �ΰ��ӿ����� �� ���� ���� ����
    private InGamePlayManager ingamePlayManager;            // Player�� ���� Enery, Draw�� �ΰ��ӿ����� Enery, Draw ���� �˰� ����.
    private InGameCharacterManager ingameCharacterManager;
    private InGameMonsterManager ingameMonsterManager;
    private InGameMapManager ingameMapManager;
    private InGameTurnManager ingameTurnManager;

    private InGameUIEventController ingameUIEventController;

    public InGameDeckManager Deck { get { return ingameDeckManager; } }
    public InGameCardManager Cards { get { return ingameCardManager; } }
    public InGamePlayManager Play { get { return ingamePlayManager; } }
    public InGameCharacterManager Characters { get { return ingameCharacterManager; } }
    public InGameMonsterManager Monster { get { return ingameMonsterManager; } }
    public InGameMapManager Map { get { return ingameMapManager; } }
    public InGameTurnManager Turn { get { return ingameTurnManager; } }

    public InGameUIEventController UIEventController { get { return ingameUIEventController; } }

    public InGameManager()
    {
        ingameDeckManager      = new InGameDeckManager(this);
        ingameCardManager      = new InGameCardManager(this);
        ingamePlayManager      = new InGamePlayManager(this);
        ingameCharacterManager = new InGameCharacterManager(this);
        ingameMonsterManager   = new InGameMonsterManager(this);
        ingameMapManager       = new InGameMapManager(this);
        ingameTurnManager      = new InGameTurnManager(this);        

        ingameUIEventController = new InGameUIEventController();
    }

    public void Start_Game()
    {
        // �⺻ ���� ���� �� �⺻ ī�� ����
        Play.Init_UserCharacterData();
        Characters.Init_CharacterSetting();

        Enter_Dungeon();
    }

    public void Enter_Dungeon()
    {
        Cards.Enter_Dungeon();                      // �� ����(ī��) : �� ����, �����ϰ� ����
        Characters.Enter_Dungeon();                 // �÷��̾� ĳ���� ����.
        Monster.Enter_Dungeon();                    // ���� ����.





        Turn.Set_TurnType(_Enums.TURN_TYPE.PLAYER); // ĳ���� �� ����
    }
}
