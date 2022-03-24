using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelComplatedPanel;
    [SerializeField] private GameObject failedPanel;

   
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
        GameManager.Instance.LevelComplated = true;
     
        levelComplatedPanel.SetActive(true);
    }
}
