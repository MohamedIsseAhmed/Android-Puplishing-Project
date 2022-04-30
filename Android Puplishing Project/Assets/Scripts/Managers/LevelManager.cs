using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class LevelManager : StaticSingeltonTemplate<LevelManager>,ISavable
{
    //[SerializeField] private float currentLevel;
    //[SerializeField] private float nextLevel;
    //[SerializeField] private float totalLevel;

    //[SerializeField] private RawImage fillImage;
    //[SerializeField] private Text currentLevelText;
    //[SerializeField] private Text nexttLevelText;

    [SerializeField] Progression progression;

    public int currentLevel;
    public int nextLevel;
    protected override void Awake()
    {
        base.Awake();
      
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {      
        float ratio= progression.CurrentLevel / progression.TotalLevel;
        ratio = Mathf.Clamp(ratio, 0, 1);
        progression.fillImage.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
  
    private void OnEnable()
    {
        SavingAndLoading.InitializeValuesEvent += SavingAndLoading_InitializeValuesEvent;
    }
    private void SavingAndLoading_InitializeValuesEvent()
    {
        currentLevel = 1;
        nextLevel = 2;
        progression.CurrentLevel = currentLevel;
        progression.NextLevel = nextLevel;            
        progression.currentLevelText.text = progression.CurrentLevel.ToString();
        progression.nexttLevelText.text = progression.NextLevel.ToString();
        progression.fillImage.rectTransform.localScale = Vector3.zero;
        StartCoroutine(LoadAndSaveWithDelay());
    }
    IEnumerator LoadAndSaveWithDelay()
    {
        yield return new WaitForSeconds(5);
        //FindObjectOfType<SavingAndLoading>().Load();
        yield return new WaitForSeconds(1);
        //FindObjectOfType<SavingAndLoading>().Save();

    }
    public object CaptureState()
    {
        SaveAndLoadLevel savedData = new SaveAndLoadLevel();
        savedData.currentLevel =progression.CurrentLevel;
        savedData.nexttLevel = progression.NextLevel;
        currentLevel = savedData.currentLevel;
        nextLevel = savedData.nexttLevel;
        return savedData;    
    }

    public void RestoreState(object state)
    {
        var restoredStateData = (SaveAndLoadLevel)state;

        progression.CurrentLevel=restoredStateData.currentLevel+1;
        progression.NextLevel=restoredStateData.nexttLevel+1;

        currentLevel = restoredStateData.currentLevel + 1;
        nextLevel= restoredStateData.nexttLevel+1;
      
    }

    [System.Serializable]
    struct SaveAndLoadLevel
    {
        public int currentLevel;
        public int nexttLevel;
    }
}
