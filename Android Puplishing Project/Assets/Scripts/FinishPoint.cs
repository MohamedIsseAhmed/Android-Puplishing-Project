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
    private bool hasFinished = false;

    public static event Action FinishedEvent;
  
    private void Update()
    {
        if (hasFinished)
        {
            StartCoroutine(GoToCourtOnFinish());
            GameManager.instance.pauseTheGame = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.pauseTheGame = true;
        hasFinished = true;
        FinishedEvent?.Invoke();
    }
    private IEnumerator GoToCourtOnFinish()
    {
       
        for(int i =0;i< CollisonHandler.instance.GetCollectedObjects.Count; i++)
        {
          
            CollisonHandler.instance.GetCollectedObjects[i].parent = null;
            CollisonHandler.instance.GetCollectedObjects[i].position =
                Vector3.Lerp(CollisonHandler.instance.GetCollectedObjects[i].position, courtsOnFinish[i].position, lerpSpeed * Time.deltaTime);

             yield return new WaitForSeconds(waitTime);
        
        }
        
    }
}
