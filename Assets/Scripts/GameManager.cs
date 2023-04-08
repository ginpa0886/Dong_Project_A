using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private WindowController windowController;
    private DataManager dataManager;
    private TableDataManager tableDatamanager;
    private SceneManagerEx sceneManagerEx;
    private UIEventController uiEventController;

    public UIEventController UI_EVENT { get { return uiEventController; } }
    public WindowController Win { get { return windowController; } set { windowController = value; } }
    public DataManager Data { get { return dataManager; } }
    public TableDataManager Table { get { return tableDatamanager; } }
    public SceneManagerEx Scene { get { return sceneManagerEx; } set { sceneManagerEx = value; } }

    private void Awake()
    {
        dataManager       = new DataManager();
        tableDatamanager  = new TableDataManager();
        uiEventController = new UIEventController();
    }

    private void Start()
    {
        
    }
}
