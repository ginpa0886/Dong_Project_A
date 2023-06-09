using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if(transform == null)
        {
            return null;
        }
        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if(go == null)
        {
            
            return null;
        }

        if(recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; ++i)
            {
                Transform transform = go.transform.GetChild(i);
                if(string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = go.GetComponent<T>();
                    if(component != null)
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>(true))
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }

    public static bool FindAndSetComponent<T>(GameObject go, ref T[] r_Array) where T : UnityEngine.Object
    {
        T[] components = go.GetComponentsInChildren<T>();

        if(components.Length == 0)
        {
            return false;
        }

        r_Array = new T[components.Length];

        for(int i = 0; i < components.Length; ++i)
        {
            r_Array[i] = components[i];
        }

        return true;
    }


    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;

        while(n > 1)
        {
            --n;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}


