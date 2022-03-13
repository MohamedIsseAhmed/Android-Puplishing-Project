using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class Obstacle : MonoBehaviour
{
   [SerializeField] private float xTweenTarget;
  
   [SerializeField] private float tweenDuration;
   [SerializeField] private float tweenDurationForBall;

    private int infinityLoop = -1;
    private Tween myTween;

    private void Start()
    {
        myTween = transform.DOLocalMoveX(xTweenTarget, tweenDuration, false).SetLoops(infinityLoop, LoopType.Yoyo);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Untagged")
        {
            Transform othterTransform = other.transform;
            othterTransform.GetComponentInParent<CollisonHandler>().GetCollectedObjects.Remove(othterTransform);
            othterTransform.parent = null;
            othterTransform.DOLocalMoveX(-othterTransform.localPosition.x * 5, tweenDurationForBall, false);
            print(other.name);
        }
        
    }

}
