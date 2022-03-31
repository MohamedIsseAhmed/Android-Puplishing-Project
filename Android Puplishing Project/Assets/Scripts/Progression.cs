using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Progression : MonoBehaviour
{
    [SerializeField] private float currentLevel;
    public float CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
    [SerializeField] private float nextLevel;
    public float NextLevel { get { return nextLevel; } set { nextLevel = value; } }
    [SerializeField] private float totlaLvel;
    public float TotalLevel { get { return totlaLvel; } set { nextLevel = value; } }
    public RawImage fillImage;
    public Text currentLevelText;
    public Text nexttLevelText;
    public TextMeshProUGUI scoreText;

    [SerializeField] private ScoreScriptabpleObject scoreScriptabpleObject;
    private void OnEnable()
    {
        scoreScriptabpleObject.ScoreEvent.AddListener(UpdateScore);
        scoreText.text = "Score:" +scoreScriptabpleObject.CurrentScore;
    }
    
    private void OnDisable()
    {
        scoreScriptabpleObject.ScoreEvent.RemoveListener(UpdateScore);
    }
    private void Update()
    {
        
    }
    public void UpdateScore(int _score)
    {
        scoreText.text = "Score:" +_score.ToString();
    }
}
