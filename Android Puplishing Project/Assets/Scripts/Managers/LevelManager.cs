using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : StaticSingeltonTemplate<LevelManager>
{
    [SerializeField] private float currentLevel;
    [SerializeField] private float nextLevel;
    [SerializeField] private float totlaLvel;

    [SerializeField] private RawImage fillImage;
    [SerializeField] private Text currentLevelText; 
    [SerializeField] private Text nexttLevelText; 
    protected override void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        base.Awake();

        currentLevel = PlayerPrefs.GetFloat("Level", 1);
        nextLevel = PlayerPrefs.GetFloat("Level", currentLevel + 1);
        currentLevelText.text =currentLevel.ToString();
        nexttLevelText.text =nextLevel.ToString();
       
    }
    private void Update()
    {
        float ratio=currentLevel/ totlaLvel;
        
        ratio = Mathf.Clamp(ratio, 0, 1);
        fillImage.rectTransform.localScale = new Vector3(ratio, 1, 1);
        
    }
}
