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
}
