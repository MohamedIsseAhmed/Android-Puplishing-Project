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
    protected override  void Awake()
    {

        base.Awake();
        savingAndLoading = GetComponent<SavingAndLoading>();     
      
        savingAndLoading.Load();

    }
    private void  Start()
    {

        SceneManager.LoadScene(currentSceneIndex);
        print(currentSceneIndex);

    }
    
    
    private void OnEnable()
    {
        MenuManager.LoadNextScene += MenuManager_LoadNextScene;
    }

    private void MenuManager_LoadNextScene()
    {
        
        savingAndLoading.Save();
        MonetizationManager.instance.Showinterstitial();
        

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
            print(currentSceneIndex);
            
            currentSceneIndex += 1;
            if (currentSceneIndex > 8)
            {
                savingAndLoading.DeleteDataSaved();
                currentSceneIndex = 0;
            }

            SceneManager.LoadScene(currentSceneIndex);
            MonetizationManager.instance.isAddFinished = false;
        }

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
