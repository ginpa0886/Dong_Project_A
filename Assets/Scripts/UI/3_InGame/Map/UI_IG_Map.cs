using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IG_Map : BaseWindow
{
    #region ENUM
    enum Texts
    {

    }

    enum GameObjects
    {
        Line_Container,

        Vertical_1,
        Vertical_2,
        Vertical_3,
        Vertical_4,
        Vertical_5,
        Vertical_6,
        Vertical_7, // Enum값 보호하기
    }

    enum Images
    {

    }

    enum Buttons
    {

    }
    #endregion

    const int MAX_WIDTH = 7;
    const int MAX_HEIGHT = 15;
    const float HORIZONTAL_GRID_VALUE = 30f;
    const float VERTICAL_GRID_VALUE = 50f;

    GridLayoutGroup m_horizontalLines;
    GridLayoutGroup[] m_VerticalLine;
    MapSpot_Prefab[] m_MapSpotPrefab;

    public override void Open()
    {
        base.Open();
        if(IsInit() == false)
        {
            Bind();
        }
    }

    void Bind()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        // Set Grid Lay Out Groups Value
        {
            int vertical_lines = 7;
            m_horizontalLines = GetGameObject((int)GameObjects.Line_Container).GetComponent<GridLayoutGroup>();
            m_VerticalLine = new GridLayoutGroup[vertical_lines];

            int vertical_idx = 1;
            for(int i = vertical_idx; i < (int)GameObjects.Vertical_7; ++i)
            {
                m_VerticalLine[i] = GetGameObject(i).GetComponent<GridLayoutGroup>();
            }

            m_horizontalLines.spacing = Vector2.right * HORIZONTAL_GRID_VALUE;
            for (int i = vertical_idx; i < (int)GameObjects.Vertical_7; ++i)
            {
                m_VerticalLine[i].spacing = Vector2.up * VERTICAL_GRID_VALUE;
            }
        }

        // Set Map Spot Prefab
        {
            GameObject go = GetGameObject((int)GameObjects.Line_Container);
            Util.FindAndSetComponent(go, ref m_MapSpotPrefab);

            // (14, 6)부터 주기시작해서 (13, 6), (12, 6)으로 진행해야 한다.
            int cnt = 0;
            for(int i = 0; i < MAX_WIDTH; ++i)
            {
                for(int j = MAX_HEIGHT - 1; j >= 0; --j)
                {
                    m_MapSpotPrefab[cnt++].Init(j, i, GameManager.Instance.InGame.Map);
                }
            }
        }

        // Set Event Controller
        {
            InGameUIEventController contorller = GameManager.Instance.InGame.UIEventController;
            contorller.Set_MapByData -= Set_MapByData;
            contorller.Set_MapByData += Set_MapByData;
        }
    }

    void Set_MapByData()
    {
        for(int i = 0; i < m_MapSpotPrefab.Length; ++i)
        {
            m_MapSpotPrefab[i].Get_MapType();
        }
    }

    public override void Close()
    {
        base.Close();
    }
}
