using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaticSingeltonTemplate<T> :MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {

        if(Instance == null)
        {
            Instance = this as T;
          
        }
        else if(Instance!=null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
   
}
