using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Progression : MonoBehaviour,ISavable
{
    [SerializeField] private int currentLevel;
    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
    [SerializeField] private int nextLevel;
    public int NextLevel { get { return nextLevel; } set { nextLevel = value; } }
    [SerializeField] private float totlaLvel;
    public float TotalLevel { get { return totlaLvel; } set { totlaLvel = value; } }
    public RawImage fillImage;
    public TextMeshProUGUI  currentLevelText;
    public TextMeshProUGUI nexttLevelText;
    public TextMeshProUGUI scoreText;

    [SerializeField] private ScoreScriptabpleObject scoreScriptabpleObject;

    [SerializeField] private Text newScore;
    int n_score;
    private void OnEnable()
    {       
        scoreScriptabpleObject.ScoreEvent.AddListener(UpdateScore);
        scoreText.text = "Score:" +scoreScriptabpleObject.CurrentScore;
    }   
    private void OnDisable()
    {
        scoreScriptabpleObject.ScoreEvent.RemoveListener(UpdateScore);        
    } 
    public void UpdateScore(int _score)
    {
        scoreText.text = "Score:" +_score.ToString();
    }
    public object CaptureState()
    {     
        return new ScoreData
        {
            score = scoreScriptabpleObject.CurrentScore          
        };
    }  
    private void Update()
    {
        UpdateLevelTexts();
    }

    public void UpdateLevelTexts()
    {
        currentLevelText.text = LevelManager.Instance.currentLevel.ToString();
        nexttLevelText.text = LevelManager.Instance.nextLevel.ToString();
        float ratio = currentLevel / TotalLevel;
        fillImage.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
    public void RestoreState(object state)
    {        
        var scoreData=(ScoreData)state;
        scoreScriptabpleObject.CurrentScore=scoreData.score;
        n_score=scoreData.score;
    }

    [System.Serializable]
    struct ScoreData
    {
        public int score;
    }
}
