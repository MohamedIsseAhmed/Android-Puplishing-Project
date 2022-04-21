using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelComplatedPanel;
    [SerializeField] private GameObject failedPanel;
    private WaitForSeconds wait =new WaitForSeconds(2);

    public static event Action LoadNextScene;
    private void OnEnable()
    {
       
        GameManager.OnFailed += OnFailed;
        LevelComplatedSo.OnlevelComplated += OnLevelComplated;
    }

   
    private void OnDisable()
    {
        GameManager.OnFailed -= OnFailed;
        LevelComplatedSo.OnlevelComplated -= OnLevelComplated;
    }
    private void OnFailed()
    {
       failedPanel.SetActive(true);
       
    }
    private void OnLevelComplated ()
    {
        //GameManager.Instance.LevelComplated = true;
        //levelComplatedPanel.SetActive(true);
        StartCoroutine(LevelComplatedDelay());
    }
    IEnumerator LevelComplatedDelay()
    {
        GameManager.Instance.LevelComplated = true;
        levelComplatedPanel.SetActive(true);
        yield return wait;
        LoadNextScene?.Invoke();


    }
}
