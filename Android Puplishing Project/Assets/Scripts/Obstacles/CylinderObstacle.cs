using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class CylinderObstacle : ObstacleBase
{
    [SerializeField] private float _xTweenTarget;
    [SerializeField] private float _zTweenTarget;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private float _tweenDurationForBall=1.75F;
    [SerializeField] private float _waitTime=1.75F;
    [SerializeField] private float _lerpTime=5F;
    [SerializeField] private float _distanceToPoint=0.05F;
    [SerializeField] private Vector3 _rotationEndValue;
    [SerializeField] private Transform[] _paths=new Transform[4];

   
    public static event Action Check›fplayerChildCount;
  

    protected override void Start()
    {
        xTweenTarget = _xTweenTarget;
        tweenDuration = _tweenDuration;
        tweenDurationForBall = _tweenDurationForBall;
        rotationEndValue = _rotationEndValue;
        zTweenTarget = _zTweenTarget;
        waitTime = _waitTime;
        lerpTime = _lerpTime;
        distanceToPoints = _distanceToPoint;
       
        paths = _paths;
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
           
        }
        
    }

}
