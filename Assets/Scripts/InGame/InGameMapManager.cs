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

    int[] row_Rate = { 45, 22, 0, 0, 5 }; // Monster, Event, Elite, Rest Site, Merchant
    int[] high_Rate = { 45, 22, 16, 12, 5 };

    // 맵 구성 및 맵 데이터(몬스터, 이벤트, 상점, 보스 등을 총괄함!)
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
        // #.0 Map을 초기화 시킨다.
        {
            for(int i = 0; i < MAX_Y; ++i)
            {
                for(int j = 0; j < MAX_X; ++j)
                {
                    Set_MapSpot(i, j, MAP_TYPE.NONE);
                }
            }
        }
        // #.1 시작점을 잡는다. 
        // 시작 점은 2 ~ 4개 사이에서 선택한다
        int r_start = UnityEngine.Random.Range(2, 5);
        int[] r_start_spots = new int[r_start];
        int none = -1;
        List<int>[] road = new List<int>[r_start]; // 시작점에 따라서 road를 만들어줌        

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

        // 시작점으로 작성해준다.
        Array.Sort(r_start_spots);
        int cur_y_pos = MAX_Y - 1;
        for(int i = 0; i < r_start_spots.Length; ++i)
        {
            Set_MapSpot(cur_y_pos, r_start_spots[i], MAP_TYPE.MONSTER);
        }
        
        // #2. 한칸씩 위로 올라가면 x축으로 -1 0 1 셋중하나를 선택해서 노드로 결정해준다. 겹치는 것도 허용된다.
        while(cur_y_pos > 0)
        {
            --cur_y_pos;
            for(int i = 0; i < r_start_spots.Length; ++i)
            {
                int ran = UnityEngine.Random.Range(-1, 2);
                int pos = r_start_spots[i];
                pos += ran;

                // x좌표의 최솟값은 0
                if(pos < 0)
                {
                    pos = 0;
                }

                // x좌표의 최댓값은 MAX_X 미만이여야 한다.
                if(pos >= MAX_X)
                {
                    pos = MAX_X - 1;
                }
                r_start_spots[i] = pos;
                road[i].Add(pos);
                Set_MapSpot(cur_y_pos, pos, MAP_TYPE.EMPTY);
            }
        }
        
        // #.3 규칙에 따라서 Empty부분에 값을 넣어주기 시작한다.
        // 3-1. 엘리트 및 휴게소는 6층 이하로 배정할 수 없습니다.
        // 3-2. Elite, Merchant 및 Rest site는 연속해서 사용할 수 없습니다. 경로에 연속적으로 Rest Site가 있을 수 없음
        // 3-3. 나가는 경로가 2개 이상인 방은 모든 목적지가 고유해야 한다 동일한 룸에서 출발하는 2개의 목적지는 동일한 위치를 공유할 수 없음 => 하나의 방에서 2가지 이상의 길로 갈라지는 골목에서는 다른 타입으로 만들어 두어야 한다.
        // 3-4 휴게소는 14층에 있을 수 없음

        const int MIN_SPECIAL = 15 - 6;
        const int EXCEPT_RESTSITE = 15 - 14;
        int r_length = road[0].Count - 1;
        int r_idx = 1;

        while (r_length != 0)
        {
            // 아래층과 윗층에 따라서 다른 확률을 적용시킴
            if (r_length > MIN_SPECIAL)
            {
                for(int i = 0; i < r_start_spots.Length; ++i)
                {
                    MAP_TYPE map_Type = Get_RandomMapTypeByRate(row_Rate);                    
                    int posX = road[i][r_idx];
                    int preX = road[i][r_idx - 1];

                    // 연속되는 특별한 맵 타입인 경우에는 수정을 진행한다.
                    MAP_TYPE pre_Type = Get_MapSpot(r_length, preX);
                    map_Type = Check_SpecialMapType(pre_Type, map_Type, row_Rate);
                    Set_MapSpot(r_length - 1, posX, map_Type);
                }
            }
            else
            {
                for(int i = 0; i < r_start_spots.Length; ++i)
                {
                    MAP_TYPE map_Type = Get_RandomMapTypeByRate(high_Rate);
                    int posX = road[i][r_idx]; int preX = road[i][r_idx - 1];

                    // 연속되는 특별한 맵 타입인 경우에는 수정을 진행한다.
                    MAP_TYPE pre_Type = Get_MapSpot(r_length, preX);
                    map_Type = Check_SpecialMapType(pre_Type, map_Type, high_Rate);
                    Set_MapSpot(r_length - 1, posX, map_Type);
                }
            }

            ++r_idx;            
            --r_length;
        }

        // 중간에 맵이 겹치거나 합쳐진 다음 나오는 맵 타입은 각기 다른 것으로 세팅해준다.
        Update_OverlapRoadNextSpot(road);

        // #.4 9번째에는 무조건 유물을, 15번째에는 rest를 넣어준다.
        for (int i = 0; i < MAX_X; ++i)
        {
            if (Get_MapSpot(TREASURE_SPOT, i) != MAP_TYPE.NONE)
            {
                Set_MapSpot(TREASURE_SPOT, i, MAP_TYPE.TREASURE);
            }

            if (Get_MapSpot(REST_SITE, i) != MAP_TYPE.NONE)
            {
                Set_MapSpot(REST_SITE, i, MAP_TYPE.RESET_SITE);
            }
        }

        // 확인 log
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

    MAP_TYPE Get_RandomMapTypeByRate(int[] r_Rate, int except = -1)
    {
        int max_rate = 0;        
        for(int i = 0; i < r_Rate.Length; ++i)
        {
            if(i == except)
            {
                continue;
            }
            max_rate += r_Rate[i];
        }

        int ran = UnityEngine.Random.Range(0, max_rate);
        int temp = 0;
        for(int i = 0; i < r_Rate.Length; ++i)
        {
            temp += r_Rate[i];
            if(ran <= temp)
            {
                return (MAP_TYPE)(i + (int)MAP_TYPE.MONSTER);
            }
        }

        return MAP_TYPE.NONE;
    }

    MAP_TYPE Check_SpecialMapType(MAP_TYPE previous, MAP_TYPE current, int[] rate)
    {
        // 맵의 데이터 타입이 연속해서 특별한 방타입이 나왔을 경우에는 다시 맵타입을 만들어서 만든다
        if((previous == MAP_TYPE.RESET_SITE || 
            previous == MAP_TYPE.MERCHANT || 
            previous == MAP_TYPE.ELITE) && 
            (current == MAP_TYPE.RESET_SITE || 
            current == MAP_TYPE.MERCHANT || 
            current == MAP_TYPE.ELITE))
        {
            current = Get_RandomMapTypeByRate(rate);
            return Check_SpecialMapType(previous, current, rate);
        }

        return current;
    }

    void Update_OverlapRoadNextSpot(List<int>[] roads)
    {
        List<int> same = new List<int>();
        List<int> temp = new List<int>();

        List<MAP_TYPE> m_types = new List<MAP_TYPE>();

        for(int i = 1; i < roads[0].Count; ++i)
        {
            for(int j = 0; j < roads.Length; ++j)
            {
                same.Clear();
                temp.Clear();

                if(same.Contains(roads[j][i]) == true)
                {
                    temp.Add(j);    // 겹치는 라인들만 넣어준다
                }
                else
                {
                    same.Add(roads[j][i]);
                }
            }

            // 겹치는 방이 없다면
            if(temp.Count == 0)
            {
                same.Clear();
                continue;
            }

            // 겹치는 방이 있다면
            if(i < roads[0].Count - 2)  // 제일 끝방이 아닐때만 적용가능한 로직이다
            {
                int next_idx = i + 1;

                for(int x = 0; x < temp.Count; ++x)
                {
                    MAP_TYPE m = Get_MapSpot(next_idx, temp[x]);

                    if(m_types.Contains(m) != false)
                    {
                        m_types.Add(m);
                        continue;
                    }

                    // 만약 동일한 맵 타입을 갖고 있다면....
                    const int MIN_SPECIAL = 15 - 6;

                    if (next_idx > MIN_SPECIAL)
                    {
                        MAP_TYPE ran = Get_RandomMapTypeByRate(row_Rate);
                        while (m_types.Contains(ran) == true)
                        {
                            ran = Get_RandomMapTypeByRate(row_Rate);
                        }
                    }
                    else
                    {
                        MAP_TYPE ran = Get_RandomMapTypeByRate(high_Rate);
                        while (m_types.Contains(ran) == true)
                        {
                            ran = Get_RandomMapTypeByRate(high_Rate);
                        }
                    }             
                }

                // 다 다른 맵 타입으로 구성된 리스트가 완성되었다면...
                for (int x = 0; x < temp.Count; ++x)
                {
                    Set_MapSpot(next_idx, temp[x], m_types[x]);
                }
            }
        }

        return;
    }

    public Character_Monster[] Get_MonsterDatas()
    {

        return null;
    }
}
