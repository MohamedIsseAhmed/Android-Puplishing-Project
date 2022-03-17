using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Level Progression",fileName ="LevelProgression")]
public class LevelProgressionSO : ScriptableObject
{
    [SerializeField] private float currentLevel;
    public float CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
    [SerializeField] private float nextLevel;
    public float NextLevel { get { return nextLevel; } set { nextLevel = value; } }
    [SerializeField] private float totlaLvel;
    public float TotalLevel { get { return totlaLvel; } set { nextLevel = value; } }

     public RawImage fillImage;

    public  Text currentLevelText;
    public Text nexttLevelText;
    private void Awake()
    {
        Intialization();
    }

    public void Intialization()
    {
        currentLevel = PlayerPrefs.GetFloat("Level", 1);
        nextLevel = PlayerPrefs.GetFloat("Level", currentLevel + 1);
        currentLevelText.text = currentLevel.ToString();
        nexttLevelText.text = nextLevel.ToString();
    }

  
}
