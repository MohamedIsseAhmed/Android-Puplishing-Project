using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class CylinderObstacle : ObstacleBase
{
    [SerializeField] private float _xTweenTarget;

    [SerializeField] private float _tweenDuration;
    [SerializeField] private float _tweenDurationForBall=1.75F;

   
    public static event Action Check›fplayerChildCount;
    // private void Start()
    // {
    //     myTween = transform.DOLocalMoveX(xTweenTarget, tweenDuration, false).SetLoops(infinityLoop, LoopType.Yoyo);
    // }

    protected override void Start()
    {
        xTweenTarget = _xTweenTarget;
        tweenDuration = _tweenDuration;
        tweenDurationForBall = _tweenDurationForBall;
        base.Start();
        //TEST 
        
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
           
        }
        
    }

}
