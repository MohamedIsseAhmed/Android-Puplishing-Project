using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public abstract class ObstacleBase:MonoBehaviour
{
    public float xTweenTarget { get; set; }
    public float zTweenTarget { get; set; }
    public float tweenDuration { get; set; }
    public float tweenDurationForBall { get; set; }
    public float waitTime { get; set; }
    public float lerpTime { get; set; }
    public float distanceToPoints { get; set; }

    private int infinityLoop = -1;
    private Tween myTween;

    public Vector3 rotationEndValue;
    //public static event Action Check›fplayerChildCount;
    public ObstacleTyoe obstacleTyoe;
    public Transform[] paths=new Transform[4];    
    protected virtual void Start()
    {
        if (obstacleTyoe == ObstacleTyoe.LineerObstacle)
        {
            myTween = transform.DOLocalMoveX(xTweenTarget, tweenDuration, false).SetLoops(infinityLoop, LoopType.Yoyo);
        }
        else
        {

            StartCoroutine(FollowingPath());
           
        }
    }
    IEnumerator FollowingPath()
    {
        int index=0;
        Vector3 currentPos=paths[index].position;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, paths[index].position, lerpTime * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentPos) < distanceToPoints)
            {
                index++;
                if (index >= paths.Length)
                {
                    index = 0;
                }
                currentPos =paths[index].position;
                yield return new WaitForSeconds(waitTime);
            }
            
            yield return null;
        }
    }
   
}
public enum ObstacleTyoe
{
    LineerObstacle,
    RotationObtacle
}
