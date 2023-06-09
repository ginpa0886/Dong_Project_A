using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject obj = GameObject.Find("GameManager");
                if(obj != null)
                {
                    _instance = obj.GetComponent<T>();
                    DontDestroyOnLoad(obj);
                    return _instance;
                }

                GameObject go = new GameObject(typeof(T).ToString());
                _instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
}
