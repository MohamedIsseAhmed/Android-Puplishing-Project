using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singelton<GameManager>
{
    [SerializeField] private WaitForSeconds waitTime = new WaitForSeconds(1);
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
      
      
       player = GameObject.FindGameObjectWithTag("Player").transform;
    
    }
    private void OnEnable()
    {
        Court.UnPaueTheGame += Court_UnPaueTheGame;     
        CylinderObstacle.Check›fplayerChildCount += Obstacle_Check›fplayerChildCount;
       BallManager.Check›fplayerChildCount+= Obstacle_Check›fplayerChildCount;
    }

    private void Obstacle_Check›fplayerChildCount()
    {

        StartCoroutine(CheckPlayerChild());
    }

    IEnumerator CheckPlayerChild()
    {
        yield return waitTime;
        if (player.transform.childCount == 0)
        {
            OnFailed?.Invoke();
            failed = true;
            yield break;
        }
    }

    private void OnDisable()
    {
        Court.UnPaueTheGame -= Court_UnPaueTheGame;
        CylinderObstacle.Check›fplayerChildCount -= Obstacle_Check›fplayerChildCount;
        BallManager.Check›fplayerChildCount -= Obstacle_Check›fplayerChildCount;
    }

    private void Court_UnPaueTheGame()
    {
       pauseTheGame = false;
    }
}
