using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Court : MonoBehaviour
{
    [SerializeField] private GameObject goalParticle;
    [SerializeField] private float waitTime = 3;
    [SerializeField] private ScoreScriptabpleObject scoreScriptabpleObject;
    private enum CourtType
    {
        InGameCourt,
        FinishCourt
    }
    [SerializeField] private CourtType courtType;    
    public static event Action UnPaueTheGame;
    public static event Action<byte> Score;
    private void OnTriggerEnter(Collider other)
    {
        goalParticle.SetActive(true);
        StartCoroutine(DeactivateParentObject(other.transform));
       // Score?.Invoke(0);
        scoreScriptabpleObject.IncreaseScore(1);
    }
    IEnumerator DeactivateParentObject(Transform ball)
    {
        if (!ball.CompareTag("Player")) 
        {

            yield return new WaitForSeconds(waitTime);
            this.transform.parent.gameObject.SetActive(false);
       
            ball.transform.gameObject.SetActive(false);
            UnPaueTheGame?.Invoke();
            goalParticle.SetActive(true);

         
        }
       
    }
}
