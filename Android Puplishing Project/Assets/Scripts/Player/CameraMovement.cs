using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private float lerpSpeedOnFinish = 5;
   
    private bool shoulStopFollowingPlayer;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        FinishPoint.FinishedEvent += FinishPoint_FinishedEvent;
    }
    private void OnDisable()
    {
        FinishPoint.FinishedEvent -= FinishPoint_FinishedEvent;
    }
    private void FinishPoint_FinishedEvent()
    {
       shoulStopFollowingPlayer = true;
        StartCoroutine(CameraMovementOnFinishingGame());
    }

    void LateUpdate()
    {
        FollowThePlayer();
    }

    private void FollowThePlayer()
    {
        if (!shoulStopFollowingPlayer)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lerpSpeed * Time.deltaTime);
        }
    }
    private IEnumerator CameraMovementOnFinishingGame()
    {
       
        while (true)
        {
            transform.Translate(Vector3.forward * lerpSpeedOnFinish * Time.deltaTime, Space.World);
            if (GameManager.Instance.LevelComplated) yield break;
            yield return null;
        }
        //transform.position = Vector3.Lerp(transform.position, transform.position + targetOffset, lerpSpeedOnFinish * Time.deltaTime);
       
      
       
    }
}
