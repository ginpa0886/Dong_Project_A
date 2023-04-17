using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _Enums
{
    #region SCENE
    public enum SCENE_TYPE
    {
        LOADING,
        LOBBY,
        INGAME,

        MAX,
    }

    #endregion

    public enum CHARACTER_TYPE
    {
        IRON,
        SILENT,

        MAX,
    }

    #region CARD
    public enum CARD_INGAME_TYPE
    {
        ATTACK,
        SKILL,
        POWRE,

        MAX,
    }

    public enum CARD_CLASS_TYPE
    {
        IRON,       // ���̾�Ŭ����
        SILENT,     // ���Ϸ���
        COMMON,     // ����(����)

        CURSE,      // ����
        ABNORMAL,   // �����̻�

        MAX,
    }

    public enum CARD_GRADE_TYPE
    {
        NORMAL, // �Ϲ�(ȸ��)
        EXPERT, // Ư��(�ϴû�)
        SPECIAL,// ���(���)
    }

    public enum CARD_USECONDITION_TYPE
    {
        NONE,               // �ܼ� �ڽ�Ʈ�� ����
        HAVE_ONLY_ATTACK,   // �տ� �ִ� ī�尡 ���� ����ī���϶���
    }
    #endregion

    #region RELIC

    public enum RELIC_DICTIONARY_TYPE
    {
        START,
        NORMAL,
        ADVANCE,
        EXPERT,
        BOSS,
        EVENT,
        SHOP,

        MAX,
    }

    public enum RELIC_INGAME_TYPE
    {


        MAX,
    }

    #endregion

    #region PORTION
    public enum PORTION_DICTIONARY_TYPE
    {
        NORMAL,
        ADVANCE,
        EXPERT,

        MAX,
    }

    public enum PORTION_INGAME_TYPE
    {

        MAX,
    }
    #endregion

    #region ABILITY
    public enum ABILITY_TYPE
    {
        NONE,               // ����
        ATTACK,             // �Ϲ� ����
        ARMOR,              // �Ϲ� ��
        ATTACK_ALL,         // ��ü ����
        ARMOR_ATTACK,       // �������� ����

        WEAK,               // ���
        COPY_AT_ABANDON,    // ���� ī�� ���̿� ����
        ATTACK_BY_ARMOR,    // ����ŭ ����        
        POWER_MULTIPLY,     // ��ȿ�� �� ����
        PICK_AT_ABANDON,    // ���� ī�� ���̿��� �� ������

        // MONSTER ABILITY
        BODY_ROLL,
        WEAK_BY_DEAD,
        SLEEPING,
        ARTIFACT,
        METALIZATION,
        CHANGE_BY_DAMAGE,

        MAX,
    }
    #endregion

    #region TURN
    public enum TURN_TYPE
    {
        NONE,
        PLAYER,
        MONSTER,

        MAX,
    }
    #endregion

    #region CHARACTER STATE
    public enum CHARACTER_STATE
    {
        NONE,
        ALIVE,
        DEAD,

        MAX,
    }
    #endregion

    #region MONSTER
    public enum MONSTER_TYPE
    {
        NORMAL,
        ELITE,
        ELITE_BOSS,
        BOSS,

        MAX,
    }

    public enum MONSTER_PATTERN_TYPE
    {
        ATTACK,
        DEFENCE,
        ATTACK_DEFFENCE,
        BUFF,
        DEBUFF,

        MAX,
    }
    #endregion

    #region MAP
    public enum MAP_TYPE
    {
        NONE,
        EMPTY,          // ���
        MONSTER,        // ����
        QUESTION_MARK,  // �̺�Ʈ
        ELITE,          // ����Ʈ
        RESET_SITE,     // �޽�
        MERCHANT,       // ����
        TREASURE,       // ����
        BOSS,
        
        MAX,
    }
    #endregion
}
