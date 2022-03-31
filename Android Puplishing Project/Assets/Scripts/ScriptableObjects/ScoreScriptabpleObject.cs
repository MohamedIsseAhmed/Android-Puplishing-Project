using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Score",menuName ="Score SO")]
public class ScoreScriptabpleObject : ScriptableObject
{
    [SerializeField] private int currentScore = 0;
    public int CurrentScore { get { return currentScore; } private set { } }
    //[SerializeField] private int nextScore = 0;
     
    [System.NonSerialized]
    public UnityEvent<int> ScoreEvent;

    private void OnEnable()
    {
        if(ScoreEvent == null)
        {
            ScoreEvent=new UnityEvent<int>();
            
        }
    }
    public void IncreaseScore(int score)
    {
        currentScore+=score;
        
        ScoreEvent.Invoke(currentScore);
    }
}
