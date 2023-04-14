using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCharacterManager
{
    private InGameManager m_ingameManager;
    private Character_Player m_PlayerInGameCharacter;  // �ΰ��ӿ����� ĳ����

    public InGameManager InGame { get { return m_ingameManager; } }    
    public Character_Player InGame_Character { get { return m_PlayerInGameCharacter; } }

    public InGameCharacterManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
    }

    public void Init_CharacterSetting()
    {
        // ĳ���� ����â���� ������ ĳ���ͷ� ������ ����
        Character_Data c_Data = InGame.Play.Get_CharacterData;
        int init_armor    = 0;
        int init_strength = 0;
        int init_agility  = 0;
        _Enums.CHARACTER_STATE init_type = _Enums.CHARACTER_STATE.ALIVE;
        Character_State newState = new Character_State(c_Data.Get_HP, init_armor, init_strength, init_agility, init_type);

        // ������ �����ͷ� ���ο� ĳ���͸� ���� �Ҵ�
        Character_Player newCharacter_InGame = new Character_Player(newState);        
        m_PlayerInGameCharacter = newCharacter_InGame;
    }

    public void Enter_Dungeon()
    {
        InGame_Character.Reset_AddAbility();    // �Ƹ�, ��, ��ø �� ��� �ʱ�ȭ
    }
}
