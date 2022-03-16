using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singelton<T>:MonoBehaviour where T: MonoBehaviour
{
   public static T Instance { get; private set; }

    protected virtual void Awake()=>Instance = this as T;
   
    protected virtual void ApplicationOnQuit()
    {
        Instance = null ;
        Destroy(gameObject);
    }
}
