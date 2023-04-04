using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UI_Lobby_Controller
{
    private static UI_Lobby_Controller _instance;

    public static UI_Lobby_Controller Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new UI_Lobby_Controller();
            }
            return _instance;
        }
    }

    public Action<bool> Contorl_SelectContainer; // ¼±ÅÃ UI ÆË¾÷ on / off
    
    
}
