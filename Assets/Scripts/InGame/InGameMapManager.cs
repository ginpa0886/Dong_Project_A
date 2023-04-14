using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static _Enums;

public class Map_Data
{
    
}

public struct Map_Pos
{
    int x;
    int y;    
}

public class InGameMapManager
{
    const int MAX_X = 7;
    const int MAX_Y = 15;
    const int TREASURE_SPOT = 7;
    const int REST_SITE = 0;

    // �� ���� �� �� ������(����, �̺�Ʈ, ����, ���� ���� �Ѱ���!)
    private InGameManager m_ingameManager;
    private MAP_TYPE[,] m_ingameMap = new MAP_TYPE[MAX_Y, MAX_X];

    public InGameManager InGame { get { return m_ingameManager; } }
    public MAP_TYPE[,] Get_Map { get { return m_ingameMap; } }
    public MAP_TYPE Get_MapSpot(int y, int x)
    {
        return m_ingameMap[y, x];
    }
    public void Set_MapSpot(int y, int x, MAP_TYPE type)
    {
        m_ingameMap[y, x] = type;
    }

    public InGameMapManager(InGameManager ingameManager)
    {
        m_ingameManager = ingameManager;
        Generate_Map();
    }

    public void Generate_Map()
    {        
        // #.0 Map�� �ʱ�ȭ ��Ų��.
        {
            for(int i = 0; i < MAX_Y; ++i)
            {
                for(int j = 0; j < MAX_X; ++j)
                {
                    Set_MapSpot(i, j, MAP_TYPE.NONE);
                }
            }
        }
        // #.1 �������� ��´�. 
        // ���� ���� 2 ~ 4�� ���̿��� �����Ѵ�
        int r_start = UnityEngine.Random.Range(2, 5);
        int[] r_start_spots = new int[r_start];
        int none = -1;
        List<int>[] road = new List<int>[r_start]; // �������� ���� road�� �������        

        for (int i = 0; i < r_start_spots.Length; ++i)
        {
            r_start_spots[i] = none;
        }

        for(int i = 0; i < r_start; ++i)
        {
            road[i] = new List<int>();
        }

        while(r_start > 0)
        {
            int ran = UnityEngine.Random.Range(0, 7);
            for(int i = 0; i  < r_start_spots.Length; ++i)
            {
                if(r_start_spots[i] == none)
                {
                    r_start_spots[i] = ran;
                    road[i].Add(ran);
                    --r_start;
                    break;
                }

                if(r_start_spots[i] == ran)
                {
                    break;
                }
            }
        }

        // ���������� �ۼ����ش�.
        Array.Sort(r_start_spots);
        int cur_y_pos = MAX_Y - 1;
        for(int i = 0; i < r_start_spots.Length; ++i)
        {
            Set_MapSpot(cur_y_pos, r_start_spots[i], MAP_TYPE.MONSTER);
        }
        
        // #2. ��ĭ�� ���� �ö󰡸� x������ -1 0 1 �����ϳ��� �����ؼ� ���� �������ش�. ��ġ�� �͵� ���ȴ�.
        while(cur_y_pos > 0)
        {
            --cur_y_pos;
            for(int i = 0; i < r_start_spots.Length; ++i)
            {
                int ran = UnityEngine.Random.Range(-1, 2);
                int pos = r_start_spots[i];
                pos += ran;

                // x��ǥ�� �ּڰ��� 0
                if(pos < 0)
                {
                    pos = 0;
                }

                // x��ǥ�� �ִ��� MAX_X �̸��̿��� �Ѵ�.
                if(pos >= MAX_X)
                {
                    pos = MAX_X - 1;
                }
                r_start_spots[i] = pos;
                road[i].Add(pos);
                Set_MapSpot(cur_y_pos, pos, MAP_TYPE.EMPTY);
            }
        }

        // #.3 9��°���� ������ ������, 15��°���� rest�� �־��ش�.
        for(int i = 0; i < MAX_X; ++i)
        {
            if (Get_MapSpot(TREASURE_SPOT, i) == MAP_TYPE.EMPTY)
            {
                Set_MapSpot(TREASURE_SPOT, i, MAP_TYPE.TREASURE);
            }

            if(Get_MapSpot(REST_SITE, i) == MAP_TYPE.EMPTY)
            {
                Set_MapSpot(REST_SITE, i, MAP_TYPE.RESET_SITE);
            }                
        }

        // #.4 ��Ģ�� ���� Empty�κп� ���� �־��ֱ� �����Ѵ�.
        // 4-1. ����Ʈ �� �ްԼҴ� 6�� ���Ϸ� ������ �� �����ϴ�.
        // 4-2. Elite, Merchant �� Rest site�� �����ؼ� ����� �� �����ϴ�. ��ο� ���������� Rest Site�� ���� �� ����
        // 4-3. ������ ��ΰ� 2�� �̻��� ���� ��� �������� �����ؾ� �Ѵ� ������ �뿡�� ����ϴ� 2���� �������� ������ ��ġ�� ������ �� ���� => �ϳ��� �濡�� 2���� �̻��� ��� �������� ��񿡼��� �ٸ� Ÿ������ ����� �ξ�� �Ѵ�.
        // 4-4 �ްԼҴ� 14���� ���� �� ����

        const int MIN_SPECIAL = 15 - 6;
        const int EXCEPT_RESTSITE = 15 - 14;
        
        while(road[0].Count != 0)
        {
            for (int i = 0; i < road.Length; ++i)
            {
               
            }

            if (road[0].Count == EXCEPT_RESTSITE)
            {

            }

            if(road[0].Count > MIN_SPECIAL)
            {

            }

            if(road[0].Count <= MIN_SPECIAL)
            {

            }            
        }

        // Ȯ�� log
        {
            string str1  = $"{m_ingameMap[0, 0].ToString()[0]} | {m_ingameMap[0, 1].ToString()[0]} | {m_ingameMap[0, 2].ToString()[0]} | {m_ingameMap[0, 3].ToString()[0]} | {m_ingameMap[0, 4].ToString()[0]} | {m_ingameMap[0, 5].ToString()[0]} | {m_ingameMap[0, 6].ToString()[0]}\n";
            string str2  = $"{m_ingameMap[1, 0].ToString()[0]} | {m_ingameMap[1, 1].ToString()[0]} | {m_ingameMap[1, 2].ToString()[0]} | {m_ingameMap[1, 3].ToString()[0]} | {m_ingameMap[1, 4].ToString()[0]} | {m_ingameMap[1, 5].ToString()[0]} | {m_ingameMap[1, 6].ToString()[0]}\n";
            string str3  = $"{m_ingameMap[2, 0].ToString()[0]} | {m_ingameMap[2, 1].ToString()[0]} | {m_ingameMap[2, 2].ToString()[0]} | {m_ingameMap[2, 3].ToString()[0]} | {m_ingameMap[2, 4].ToString()[0]} | {m_ingameMap[2, 5].ToString()[0]} | {m_ingameMap[2, 6].ToString()[0]}\n";
            string str4  = $"{m_ingameMap[3, 0].ToString()[0]} | {m_ingameMap[3, 1].ToString()[0]} | {m_ingameMap[3, 2].ToString()[0]} | {m_ingameMap[3, 3].ToString()[0]} | {m_ingameMap[3, 4].ToString()[0]} | {m_ingameMap[3, 5].ToString()[0]} | {m_ingameMap[3, 6].ToString()[0]}\n";
            string str5  = $"{m_ingameMap[4, 0].ToString()[0]} | {m_ingameMap[4, 1].ToString()[0]} | {m_ingameMap[4, 2].ToString()[0]} | {m_ingameMap[4, 3].ToString()[0]} | {m_ingameMap[4, 4].ToString()[0]} | {m_ingameMap[4, 5].ToString()[0]} | {m_ingameMap[4, 6].ToString()[0]}\n";
            string str6  = $"{m_ingameMap[5, 0].ToString()[0]} | {m_ingameMap[5, 1].ToString()[0]} | {m_ingameMap[5, 2].ToString()[0]} | {m_ingameMap[5, 3].ToString()[0]} | {m_ingameMap[5, 4].ToString()[0]} | {m_ingameMap[5, 5].ToString()[0]} | {m_ingameMap[5, 6].ToString()[0]}\n";
            string str7  = $"{m_ingameMap[6, 0].ToString()[0]} | {m_ingameMap[6, 1].ToString()[0]} | {m_ingameMap[6, 2].ToString()[0]} | {m_ingameMap[6, 3].ToString()[0]} | {m_ingameMap[6, 4].ToString()[0]} | {m_ingameMap[6, 5].ToString()[0]} | {m_ingameMap[6, 6].ToString()[0]}\n";
            string str8  = $"{m_ingameMap[7, 0].ToString()[0]} | {m_ingameMap[7, 1].ToString()[0]} | {m_ingameMap[7, 2].ToString()[0]} | {m_ingameMap[7, 3].ToString()[0]} | {m_ingameMap[7, 4].ToString()[0]} | {m_ingameMap[7, 5].ToString()[0]} | {m_ingameMap[7, 6].ToString()[0]}\n";
            string str9  = $"{m_ingameMap[8, 0].ToString()[0]} | {m_ingameMap[8, 1].ToString()[0]} | {m_ingameMap[8, 2].ToString()[0]} | {m_ingameMap[8, 3].ToString()[0]} | {m_ingameMap[8, 4].ToString()[0]} | {m_ingameMap[8, 5].ToString()[0]} | {m_ingameMap[8, 6].ToString()[0]}\n";
            string str10 = $"{m_ingameMap[9, 0].ToString()[0]} | {m_ingameMap[9, 1].ToString()[0]} | {m_ingameMap[9, 2].ToString()[0]} | {m_ingameMap[9, 3].ToString()[0]} | {m_ingameMap[9, 4].ToString()[0]} | {m_ingameMap[9, 5].ToString()[0]} | {m_ingameMap[9, 6].ToString()[0]}\n";
            string str11 = $"{m_ingameMap[10, 0].ToString()[0]} | {m_ingameMap[10, 1].ToString()[0]} | {m_ingameMap[10, 2].ToString()[0]} | {m_ingameMap[10, 3].ToString()[0]} | {m_ingameMap[10, 4].ToString()[0]} | {m_ingameMap[10, 5].ToString()[0]} | {m_ingameMap[10, 6].ToString()[0]}\n";
            string str12 = $"{m_ingameMap[11, 0].ToString()[0]} | {m_ingameMap[11, 1].ToString()[0]} | {m_ingameMap[11, 2].ToString()[0]} | {m_ingameMap[11, 3].ToString()[0]} | {m_ingameMap[11, 4].ToString()[0]} | {m_ingameMap[11, 5].ToString()[0]} | {m_ingameMap[11, 6].ToString()[0]}\n";
            string str13 = $"{m_ingameMap[12, 0].ToString()[0]} | {m_ingameMap[12, 1].ToString()[0]} | {m_ingameMap[12, 2].ToString()[0]} | {m_ingameMap[12, 3].ToString()[0]} | {m_ingameMap[12, 4].ToString()[0]} | {m_ingameMap[12, 5].ToString()[0]} | {m_ingameMap[12, 6].ToString()[0]}\n";
            string str14 = $"{m_ingameMap[13, 0].ToString()[0]} | {m_ingameMap[13, 1].ToString()[0]} | {m_ingameMap[13, 2].ToString()[0]} | {m_ingameMap[13, 3].ToString()[0]} | {m_ingameMap[13, 4].ToString()[0]} | {m_ingameMap[13, 5].ToString()[0]} | {m_ingameMap[13, 6].ToString()[0]}\n";
            string str15 = $"{m_ingameMap[14, 0].ToString()[0]} | {m_ingameMap[14, 1].ToString()[0]} | {m_ingameMap[14, 2].ToString()[0]} | {m_ingameMap[14, 3].ToString()[0]} | {m_ingameMap[14, 4].ToString()[0]} | {m_ingameMap[14, 5].ToString()[0]} | {m_ingameMap[14, 6].ToString()[0]}\n";

            Debug.Log(str1 + str2 + str3 + str4 + str5 + str6 + str7 + str8 + str9 + str10 + str11 + str12 + str13 + str14 + str15);
        }                        
    }

    MAP_TYPE Get_RandomTypeByRate()
    {
        int[] room_rate = { 45, 22, 16, 12, 5 }; // Monster, Event, Elite, Rest Site, Merchant
        int ran = UnityEngine.Random.Range(0, 100);
        int temp = 0;
        for(int i = 0; i < room_rate.Length; ++i)
        {
            temp += room_rate[i];
            if(ran <= temp)
            {
                return (MAP_TYPE)(i - 1);
            }
        }

        return MAP_TYPE.NONE;
    }

    public Character_Monster[] Get_MonsterDatas()
    {

        return null;
    }
}
