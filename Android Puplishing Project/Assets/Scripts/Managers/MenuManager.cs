using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelComplatedPanel;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreScriptabpleObject score;
    [SerializeField] private float scoreLerp;

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
    private void Update()
    {
        //;
       
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
