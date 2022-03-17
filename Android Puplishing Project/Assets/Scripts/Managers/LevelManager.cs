using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class LevelManager : StaticSingeltonTemplate<LevelManager>
{
    //[SerializeField] private float currentLevel;
    //[SerializeField] private float nextLevel;
    //[SerializeField] private float totalLevel;

    //[SerializeField] private RawImage fillImage;
    //[SerializeField] private Text currentLevelText;
    //[SerializeField] private Text nexttLevelText;

    [SerializeField] Progression progression;
    protected override void Awake()
    {
        
        if (Instance != null)
        {
            int count = FindObjectsOfType<LevelManager>().Length;
            if(count > 1)
            {
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            progression.CurrentLevel = PlayerPrefs.GetFloat("CurrentLevel Level", 1);
            PlayerPrefs.SetFloat("CurrentLevel Level", progression.CurrentLevel);
            progression.NextLevel = PlayerPrefs.GetFloat("NextLevel Level", progression.CurrentLevel + 1);
            PlayerPrefs.SetFloat("NextLevel Level", progression.NextLevel);
            print(progression.NextLevel);
            progression.currentLevelText.text = progression.CurrentLevel.ToString();
            progression.nexttLevelText.text = progression.NextLevel.ToString();
        }
        base.Awake();

     

    }
    private void Update()
    {
       
        float ratio= progression.CurrentLevel / progression.TotalLevel;

        ratio = Mathf.Clamp(ratio, 0, 1);
        progression.fillImage.rectTransform.localScale = new Vector3(ratio, 1, 1);
       

    }
    public void ÝncreaseCurrentLevel()
    {   
        progression.CurrentLevel += 1;
        PlayerPrefs.SetFloat("Level", progression.CurrentLevel);
        progression.NextLevel = PlayerPrefs.GetFloat("nextLevel", progression.CurrentLevel + 1);
        PlayerPrefs.SetFloat("Level", progression.NextLevel);
        float ratio = progression.CurrentLevel / progression.TotalLevel;
        progression.fillImage.rectTransform.localScale = new Vector3(ratio, 1, 1);

        progression.currentLevelText.text = progression.CurrentLevel.ToString();
        progression.nexttLevelText.text = progression.NextLevel.ToString();
    }
}
