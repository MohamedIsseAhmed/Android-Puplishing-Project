using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private float waitTime = 0.10f;
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float lerpRatio = 0f;
    [SerializeField] private float timer = 10f;
    [SerializeField] private Vector3 lerpOffset;
    [SerializeField] private List<Transform> courtsOnFinish = new List<Transform>();
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private LevelComplatedSo levelComplatedSO;
    private bool hasFinished = false;

    public static event Action ChangeCameraBehaviourOnFinish;
 
    private void Update()
    {
        if (hasFinished)
        {
            GoToCourtOnFinish();
            GameManager.Instance.pauseTheGame = true;

        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.pauseTheGame = true;
        hasFinished = true;
        ChangeCameraBehaviourOnFinish?.Invoke();
    }
    private void GoToCourtOnFinish()
    {
        StartCoroutine(levelComplatedSO.GoToCourt(CollisonHandler.Instance.GetCollectedObjects));
      
    }
}
