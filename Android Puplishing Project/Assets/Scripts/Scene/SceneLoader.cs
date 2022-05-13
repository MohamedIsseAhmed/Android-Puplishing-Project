using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : StaticSingeltonTemplate<SceneLoader>,ISavable
{
    SavingAndLoading savingAndLoading;

     private static int currentSceneIndex;
    private int maxLevelNumber = 8;
    protected override  void Awake()
    {
        maxLevelNumber = 8;

        base.Awake();
        savingAndLoading = GetComponent<SavingAndLoading>();     
      
        savingAndLoading.Load();

    }
    private void  Start()
    {
        if (currentSceneIndex > maxLevelNumber)
        {
            savingAndLoading.DeleteDataSaved();
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex);
      

    }
    
    
    private void OnEnable()
    {
        MenuManager.LoadNextSceneEvent += MenuManager_LoadNextScene;
    }

    private void MenuManager_LoadNextScene()
    {
       savingAndLoading.Save();
      
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        MonetizationManager.instance.ShowBannerAd();
    }
    private void Update()
    {
        if (MonetizationManager.instance.isAddFinished)
        {
          
            currentSceneIndex += 1;
            if (currentSceneIndex > maxLevelNumber)
            {
                savingAndLoading.DeleteDataSaved();
                currentSceneIndex = 0;
            }

            SceneManager.LoadScene(currentSceneIndex);
            MonetizationManager.instance.isAddFinished = false;
        }

    }
    public void LoadAdd()
    {
        MonetizationManager.instance.Showinterstitial();
    }
    public void ReloadSceneOnFail()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public object CaptureState()
    {
        var sceneIndex=new SceneIndex();
        sceneIndex.index=currentSceneIndex;
     
        return sceneIndex;

    }

    public void RestoreState(object state)
    {

        var restoredSceneIndex=(SceneIndex)state;
       
        currentSceneIndex =restoredSceneIndex.index+1;
       
    }

    [System.Serializable]
    struct SceneIndex
    {
        public int index;
    }
}
