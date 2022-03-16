using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class CylinderObstacle : ObstacleBase
{
    //[SerializeField] private float xTweenTarget;

    //[SerializeField] private float tweenDuration;
    //[SerializeField] private float tweenDurationForBall;

    // private int infinityLoop = -1;
    // private Tween myTween;
     public static event Action Check›fplayerChildCount;
    // private void Start()
    // {
    //     myTween = transform.DOLocalMoveX(xTweenTarget, tweenDuration, false).SetLoops(infinityLoop, LoopType.Yoyo);
    // }

    protected override void Start()
    {
        xTweenTarget = 6.45f;
        tweenDuration = 3;
        tweenDurationForBall = 1.75f;
        base.Start();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Untagged")
        {
            Transform othterTransform = other.transform;
            othterTransform.GetComponentInParent<CollisonHandler>().GetCollectedObjects.Remove(othterTransform);
            othterTransform.parent = null;
            othterTransform.DOLocalMoveX(-othterTransform.localPosition.x * 5, tweenDurationForBall, false);
     
           Check›fplayerChildCount?.Invoke();
            //TEST 
            Call();
        }
        
    }

}
