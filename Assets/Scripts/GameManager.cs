using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public WindowController windowController;
    private DataManager dataManager;

    public WindowController Win { get { return windowController; } }
    public DataManager Data { get { return dataManager; } }

    private void Awake()
    {
        dataManager = new DataManager();
    }

    private void Start()
    {
        
    }
}
