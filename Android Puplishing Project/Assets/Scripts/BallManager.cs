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
    private void Update()
    {
        if (canMove)
        {
            StartCoroutine(MovetToTarget());
            timer += Time.deltaTime;
            if(timer > data.lerpSpeed)
            {
                timer= data.lerpSpeed;
                lerpOffset.y = 4f;
            }
            lerpRatio=timer/data.lerpSpeed;
          
            lerpOffset=curve.Evaluate(lerpRatio)*lerpOffset;
        }
       
        print(canMove);
    }
    private void OnTriggerEnter(Collider other)
    {
       
        PlayerController controller = other.GetComponentInParent<PlayerController>();
         if (controller != null)
        {
           GameManager.instance.pauseTheGame = true;
            FindObjectOfType<CollisonHandler>().AllignWithThPlayer();
            Transform objectcollidedwith = null;
            if (CollisonHandler.instance.GetCollectedObjects.Contains(other.transform))
            {
                int lastIndex = CollisonHandler.instance.GetCollectedObjects.IndexOf(other.transform);
                for (int i = 0; i < data.courts.Count; i++)
                {
                    balls.Add(CollisonHandler.instance.GetCollectedObjects[lastIndex]);
                    lastIndex--;
                }
                objectcollidedwith = other.transform;
                
                CollisonHandler.instance.GetCollectedObjects.Remove(other.transform);
                canMove = true;
                MoveTheBall(objectcollidedwith);
                GetComponent<BoxCollider>().enabled = false;
            }
            
        }
      
    }
  
    private void MoveTheBall(Transform ball)
    {
        ball.parent = null;
       
    }

    IEnumerator MovetToTarget()
    {
     
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].transform.position = Vector3.MoveTowards(balls[i].transform.position, data.courts[i].position, data.lerpSpeed * Time.deltaTime)+lerpOffset;
            yield return new WaitForSeconds(data.waitTime);

        }
       
       
        yield return null;
        
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
