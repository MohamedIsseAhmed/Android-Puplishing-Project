using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : StaticSingeltonTemplate<ScoreManager>
{
   
    [SerializeField] private byte scoreCount = 0;
    [SerializeField] private Progression progression;
    protected override void Awake()
    {
        if (Instance != null)
        {

            Destroy(gameObject);
        }
        else
        {
            int count = FindObjectsOfType<LevelManager>().Length;
            if (count > 1)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            scoreCount = (byte)PlayerPrefs.GetInt("Score");

        }
        base.Awake();
    }
    private void Update()
    {
    }
    private void OnEnable()
    {
        Court.Score += Score;
    }
    private void OnDisable()
    {
        Court.Score -= Score;
    }
    
    public void Score(byte _score)
    {
        scoreCount += _score;
        PlayerPrefs.SetInt("Score",scoreCount);
        
    }
    public byte GetScore()
    {
        return scoreCount;
    }
    public void UpdateScoreOnLoad()
    {
       
        //scoreCount =(byte)PlayerPrefs.GetInt("score");
        //progression.scoreText.SetText("Score:" + scoreCount.ToString());
    }
}
