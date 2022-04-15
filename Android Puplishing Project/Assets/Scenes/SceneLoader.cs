using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : StaticSingeltonTemplate<SceneLoader>
{
    SavingAndLoading savingAndLoading;
    protected override  void Awake()
    {
        savingAndLoading = GetComponent<SavingAndLoading>();
        base.Awake();
        savingAndLoading.Load();

    }
    private void Start()
    {
       
    }
    private void OnEnable()
    {
        LevelComplatedSo.OnlevelComplated += LevelComplatedSo_OnlevelComplated;
    }

    private void LevelComplatedSo_OnlevelComplated()
    {
        savingAndLoading.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       // StartCoroutine(LoadNextScene());
    }
    IEnumerator LoadNextScene()
    {
       
        savingAndLoading.Save();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
