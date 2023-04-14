using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager
{
    private InGameDeckManager ingameDeckManager;            // Player가 가지고 있는 원본 덱 값을 갖고 있음
    private InGameCardManager ingameCardManager;            // Player의 인게임에서의 덱 값을 갖고 있음
    private InGamePlayManager ingamePlayManager;            // Player의 원본 Enery, Draw와 인게임에서의 Enery, Draw 값을 알고 있음.
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
        // 기본 유저 세팅 및 기본 카드 세팅
        Play.Init_UserCharacterData();
        Characters.Init_CharacterSetting();

        Enter_Dungeon();
    }

    public void Enter_Dungeon()
    {
        Cards.Enter_Dungeon();                      // 덱 세팅(카드) : 덱 구성, 랜덤하게 섞기
        Characters.Enter_Dungeon();                 // 플레이어 캐릭터 세팅.
        Monster.Enter_Dungeon();                    // 몬스터 세팅.





        Turn.Set_TurnType(_Enums.TURN_TYPE.PLAYER); // 캐릭터 턴 세팅
    }
}
