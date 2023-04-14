using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_State
{
    int c_hp;
    int c_maxhp;
    int c_armor;
    int c_strength;
    int c_agility;
    // 추가적인 능력들은 넣어주어야함!

    _Enums.CHARACTER_STATE c_type;

    public Character_State(int hp, int armor, int strength, int agility, _Enums.CHARACTER_STATE type)
    {
        c_hp = hp;
        c_maxhp = hp;
        c_armor = armor;
        c_strength = strength;
        c_agility = agility;
        c_type = type;
    }

    public Character_State(Character_State c_State)
    {
        c_hp = c_State.c_hp;
        c_armor = c_State.Get_Armor;
        c_strength = c_State.Get_Strength;
        c_agility = c_State.Get_Agility;

        c_type = c_State.Get_StateType;
    }

    public int Get_Hp { get { return c_hp; } }
    public int Get_MaxHP { get { return c_maxhp; } }
    public int Get_Armor { get { return c_armor; } }
    public int Get_Strength { get { return c_strength; } }
    public int Get_Agility { get { return c_agility; } }

    public _Enums.CHARACTER_STATE Get_StateType { get { return c_type; } }

    public void Set_CharacterData(Character_State c_State)
    {
        c_hp = c_State.Get_Hp;
        c_maxhp = c_State.Get_MaxHP;
        c_armor = c_State.Get_Armor;
        c_strength = c_State.Get_Strength;
        c_agility = c_State.Get_Agility;

        c_type = c_State.Get_StateType;
    }

    public void Reset_AddAbility()
    {
        c_armor = 0;
        c_strength = 0;
        c_agility = 0;
    }
}

public abstract class Character
{
    protected Character_State m_CharacterState;

    public virtual void Attack(Character fromA, Character toB, Card_Data c_Data)
    {

    }

    public virtual void Attack(Character fromA, Character toB, int damage)
    {

    }

    public virtual void Damaged(Character toA, Character fromB, int damage)
    {

    }

    public int Get_Hp { get { return m_CharacterState.Get_Hp; } }
    public int Get_MaxHp { get { return m_CharacterState.Get_MaxHP; } }
    public int Get_Armor { get { return m_CharacterState.Get_Armor; } }
    public int Get_Strength { get { return m_CharacterState.Get_Strength; } }
    public int Get_Agility { get { return m_CharacterState.Get_Agility; } }

    public _Enums.CHARACTER_STATE Get_StateType { get { return m_CharacterState.Get_StateType; } }

    public Character_State Get_CharacterState()
    {
        return m_CharacterState;
    }

    public void Set_CharacterData(Character_State c_State)
    {
        m_CharacterState.Set_CharacterData(c_State);
    }

    public void Reset_AddAbility()
    {
        m_CharacterState.Reset_AddAbility();
    }
}
