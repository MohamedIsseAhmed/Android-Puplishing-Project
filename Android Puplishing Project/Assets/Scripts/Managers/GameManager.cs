using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singelton<GameManager>
{
    public static GameManager instance;

    public bool pauseTheGame = false;
    private Transform player;

    private bool levelComplated;
    public bool LevelComplated { get {return  levelComplated; } set { levelComplated = value; } }
    private bool failed;
    public bool Failed { get { return failed; } }

    public static event Action OnFailed;
    public int mCounter;
    protected override void Awake()
    {
        base.Awake();
        print(Instance.GetType());
      
       player = GameObject.FindGameObjectWithTag("Player").transform;
    
    }
    private void OnEnable()
    {
        Court.UnPaueTheGame += Court_UnPaueTheGame;     
        CylinderObstacle.Check›fplayerChildCount += Obstacle_Check›fplayerChildCount;
    }

    private void Obstacle_Check›fplayerChildCount()
    {
        
        if (player.transform.childCount == 0)
        {        
            OnFailed?.Invoke();
            failed = true;  
            return;
        }
    }

    private void OnLevelComplated()
    {
        levelComplated = true;     
    }

    private void OnDisable()
    {
        Court.UnPaueTheGame -= Court_UnPaueTheGame;
       
    }

    private void Court_UnPaueTheGame()
    {
       pauseTheGame = false;
    }
}
