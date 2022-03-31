using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingTest loadingTest;

    private int currentIndex;
    private void OnEnable()
    {
        currentIndex = loadingTest.currentScene;
        loadingTest.LoadEvent.AddListener(LoadScene);
    }
   
    private void Start()
    {
        //AsyncOperation operation=
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //currentIndex = loadingTest.currentScene;
        //SceneManager.LoadSceneAsync(currentIndex);
        //print("Loading" + currentIndex);
        StartCoroutine(LoadSceneOperation());
    }
    IEnumerator LoadSceneOperation()
    {
        yield return null;

      
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentIndex);
       
        asyncOperation.allowSceneActivation = false;
      
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
               
               asyncOperation.allowSceneActivation = true;

            }

            yield return null;
        }
    }
    private void OnDisable()
    {
        loadingTest.LoadEvent.RemoveListener(LoadScene);
    }
    public void LoadScene()
    {
        currentIndex=loadingTest.currentScene;
        SceneManager.LoadScene(currentIndex);
    }
}
