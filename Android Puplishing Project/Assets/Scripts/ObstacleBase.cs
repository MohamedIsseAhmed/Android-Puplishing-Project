using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public abstract class ObstacleBase:MonoBehaviour
{
    public float xTweenTarget { get; set; }
    public float tweenDuration { get; set; }
    public float tweenDurationForBall { get; set; }

    private int infinityLoop = -1;
    private Tween myTween;
    //public static event Action Check›fplayerChildCount;
    protected virtual void Start()
    {
        myTween = transform.DOLocalMoveX(xTweenTarget, tweenDuration, false).SetLoops(infinityLoop, LoopType.Yoyo);
    }
  
}
