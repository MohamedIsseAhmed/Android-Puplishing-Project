using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonHandler : Singelton<CollisonHandler>
{
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float alligningSpeed = 10f;
    [SerializeField] private float timeToWaiteToScale = 0.2f;
    [SerializeField] private float yTarget = 0.2f;
     [SerializeField] float zOffset = 5;
     [SerializeField] float scleModifier = 1.3f;
    [SerializeField] private List<Transform> collectedObject = new List<Transform>();
    public List<Transform> GetCollectedObjects { get { return collectedObject; } }

    int index = 0;
    public static CollisonHandler instance;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            CollectObject(other.transform);
           
            other.tag = "Untagged";
        }
    }
  
    public void CollectObject(Transform targetTransform)
    {
        if (!collectedObject.Contains(targetTransform))
        {
           
            targetTransform.parent = transform;
            collectedObject.Add(targetTransform);
            
        }
        StartCoroutine(ScallingAndSnappingObjects());
    }
    IEnumerator ScallingAndSnappingObjects()
    {


        for (byte i = 1; i <= collectedObject.Count-1; i++)
        {

           
            collectedObject[i].transform.localPosition =
                new Vector3(collectedObject[i - 1].localPosition.x, yTarget, collectedObject[i - 1].localPosition.z + zOffset);

             yield return new WaitForSeconds(timeToWaiteToScale);
           
        }
        StartCoroutine(ScallingObjects());
        StartCoroutine(DeScallingObjects());

    }
    IEnumerator ScallingObjects()
    {
        for (byte i = 1; i <= collectedObject.Count-1; i++)
        {
            collectedObject[i].transform.localScale = collectedObject[i].transform.localScale * scleModifier;
            yield return null;
        }
    }
    IEnumerator DeScallingObjects()
    {
        for (byte i = 1; i <= collectedObject.Count - 1; i++)
        {
            collectedObject[i].transform.localScale = collectedObject[i].transform.localScale / scleModifier;
            yield return null;
        }
    }
    public IEnumerator AllignWithThPlayer()
    {
        if (collectedObject.Count >0)
        {
            for (byte i = 1; i <= collectedObject.Count - 1; i++)
            {


                Vector3 desiredPosition =
                     new Vector3(collectedObject[i - 1].localPosition.x,yTarget, collectedObject[i].localPosition.z);
                collectedObject[i].transform.localPosition = Vector3.MoveTowards(collectedObject[i].transform.localPosition, desiredPosition, lerpSpeed * Time.deltaTime);
                yield return new WaitForSeconds(alligningSpeed);

               
            }
        }
        
       
    }

}