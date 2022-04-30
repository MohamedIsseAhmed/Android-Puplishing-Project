using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    
    public List<Transform> balls=new List<Transform>();

    private int courtNumber;
    [SerializeField] private Data data;
    private bool canMove;
    private float timer;
    private float lerpRatio;
    [SerializeField] private Vector3 lerpOffset;
    [SerializeField] private AnimationCurve curve;

    public static event Action Check›fplayerChildCount;

   [SerializeField] private CollisonHandler collisonHandler;
    private BoxCollider boxCollider;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
      
    }
    private void Update()
    {
        if (!canMove) return;
        if (canMove)
        {
            StartCoroutine(MovetToTarget());
            timer += Time.deltaTime;
            if (timer > data.lerpSpeed)
            {
                timer = data.lerpSpeed;
                lerpOffset.y = 4f;
            }
            lerpRatio = timer / data.lerpSpeed;

            lerpOffset = curve.Evaluate(lerpRatio) * lerpOffset;
        }
       
      
    }
    private void OnTriggerEnter(Collider other)
    {

        PlayerController controller = other.GetComponentInParent<PlayerController>();
        MovingBallToTarget(other, controller);

    }


    private void MoveTheBall(Transform ball)
    {
        ball.parent = null;
       
    }

    IEnumerator MovetToTarget()
    {
     
        for (byte i = 0; i < balls.Count; i++)
        {
            balls[i].transform.position = Vector3.MoveTowards(balls[i].transform.position, data.courts[i].position, data.lerpSpeed * Time.deltaTime)+lerpOffset;
            yield return new WaitForSeconds(data.waitTime);

        }
       
       
        yield return null;
        
    }

    public void MovingBallToTarget(Collider other, PlayerController controller)
    {
        if (controller != null)
        {
            GameManager.Instance.pauseTheGame = true;
            collisonHandler.AllignWithThPlayer();
            Transform objectcollidedwith = null;
            if (CollisonHandler.Instance.GetCollectedObjects.Contains(other.transform))
            {
                int indexOfCurrentCollidedBall = CollisonHandler.Instance.GetCollectedObjects.IndexOf(other.transform);
                for (byte i = 0; i < data.courts.Count; i++)
                {
                    balls.Add(CollisonHandler.Instance.GetCollectedObjects[indexOfCurrentCollidedBall]);
                    indexOfCurrentCollidedBall--;
                }
                objectcollidedwith = other.transform;
                CollisonHandler.Instance.GetCollectedObjects.Remove(other.transform);
                canMove = true;
                MoveTheBall(objectcollidedwith);
                boxCollider.enabled = false;

                Check›fplayerChildCount?.Invoke();
            }

        }
    }

    [System.Serializable]
    public class Data
    {
        public int courtNumber;
        public float lerpSpeed = 5f;
        public float waitTime = 1f;
        public List<Transform> courts = new List<Transform>();
    }
}
