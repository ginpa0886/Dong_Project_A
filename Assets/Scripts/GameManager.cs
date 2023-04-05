using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public WindowController windowController;
    private DataManager dataManager;
    private TableDataManager tableDatamanager;

    public WindowController Win { get { return windowController; } }
    public DataManager Data { get { return dataManager; } }
    public TableDataManager Table { get { return tableDatamanager; } }

    private void Awake()
    {
        dataManager = new DataManager();
        tableDatamanager = new TableDataManager();
    }

    private void Start()
    {
        
    }
}
