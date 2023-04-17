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
        IRON,       // 아이언클래드
        SILENT,     // 사일런스
        COMMON,     // 무색(공통)

        CURSE,      // 저주
        ABNORMAL,   // 상태이상

        MAX,
    }

    public enum CARD_GRADE_TYPE
    {
        NORMAL, // 일반(회색)
        EXPERT, // 특별(하늘색)
        SPECIAL,// 희귀(노랑)
    }

    public enum CARD_USECONDITION_TYPE
    {
        NONE,               // 단순 코스트로 적용
        HAVE_ONLY_ATTACK,   // 손에 있는 카드가 전부 공격카드일때만
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
        NONE,               // 없음
        ATTACK,             // 일반 공격
        ARMOR,              // 일반 방어도
        ATTACK_ALL,         // 전체 공격
        ARMOR_ATTACK,       // 방어력으로 공격

        WEAK,               // 취약
        COPY_AT_ABANDON,    // 버린 카드 더미에 복사
        ATTACK_BY_ARMOR,    // 방어도만큼 피해        
        POWER_MULTIPLY,     // 힘효과 곱 연산
        PICK_AT_ABANDON,    // 버린 카드 더미에서 덱 맨위로

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
        EMPTY,          // 빈방
        MONSTER,        // 몬스터
        QUESTION_MARK,  // 이벤트
        ELITE,          // 엘리트
        RESET_SITE,     // 휴식
        MERCHANT,       // 상점
        TREASURE,       // 보물
        BOSS,
        
        MAX,
    }
    #endregion
}
