using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Scene Test",fileName ="Scene/Load")]
public class LoadingTest : ScriptableObject
{
    public int currentScene=0;
    public int nextScene;

    [System.NonSerialized]
    public UnityEvent LoadEvent;

    private void OnEnable()
    {
        if (LoadEvent == null) { LoadEvent = new UnityEvent(); }
    }
   
    public void LoadScene()
    {
        currentScene++;
        LoadEvent.Invoke();
        //SceneManager.LoadSceneAsync(currentScene);
    }
}
