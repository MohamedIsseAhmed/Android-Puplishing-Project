using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Score",menuName ="Score SO")]
public class ScoreScriptabpleObject : ScriptableObject
{
    [SerializeField] private int currentScore = 0;
    public int CurrentScore { get { return currentScore; } set { currentScore = value; } }
    //[SerializeField] private int nextScore = 0;
    public int acumulatedScor;
    [System.NonSerialized]
    public UnityEvent<int> ScoreEvent;

    private void OnEnable()
    {
        if(ScoreEvent == null)
        {
            ScoreEvent=new UnityEvent<int>();
            
        }
        
    }

    public void SetScore()
    {
        acumulatedScor = currentScore;
    }

    public void IncreaseScore(int score)
    {
        currentScore+=score;
        ScoreEvent.Invoke(currentScore);
    }

 
}

