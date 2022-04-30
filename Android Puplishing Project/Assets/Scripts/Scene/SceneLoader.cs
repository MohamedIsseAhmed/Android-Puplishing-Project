using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : StaticSingeltonTemplate<SceneLoader>,ISavable
{
    SavingAndLoading savingAndLoading;

    [SerializeField] private static int currentSceneIndex;
    protected override  void Awake()
    {
        savingAndLoading = GetComponent<SavingAndLoading>();
        base.Awake();
        savingAndLoading.Load();

    }
    private void  Start()
    {
        SceneManager.LoadScene(currentSceneIndex);
      
    }
    
    
    private void OnEnable()
    {
        MenuManager.LoadNextScene += MenuManager_LoadNextScene;
    }

    private void MenuManager_LoadNextScene()
    {
        savingAndLoading.Save();
        SceneManager.LoadScene(currentSceneIndex + 1);
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
