using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCharacterManager
{
    private InGameManager m_ingameManager;
    private Character_Player m_PlayerInGameCharacter;  // 인게임에서의 캐릭터

    public InGameManager InGame { get { return m_ingameManager; } }    
    public Character_Player InGame_Character { get { return m_PlayerInGameCharacter; } }

    public InGameCharacterManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
    }

    public void Init_CharacterSetting()
    {
        // 캐릭터 선택창에서 선택한 캐릭터로 데이터 생성
        Character_Data c_Data = InGame.Play.Get_CharacterData;
        int init_armor    = 0;
        int init_strength = 0;
        int init_agility  = 0;
        _Enums.CHARACTER_STATE init_type = _Enums.CHARACTER_STATE.ALIVE;
        Character_State newState = new Character_State(c_Data.Get_HP, init_armor, init_strength, init_agility, init_type);

        // 생성한 데이터로 새로운 캐릭터를 만들어서 할당
        Character_Player newCharacter_InGame = new Character_Player(newState);        
        m_PlayerInGameCharacter = newCharacter_InGame;
    }

    public void Enter_Dungeon()
    {
        InGame_Character.Reset_AddAbility();    // 아머, 힘, 민첩 등 요소 초기화
    }
}
