using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonHandler : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float alligningSpeed = 10f;
    [SerializeField] private List<Transform> collectedObject = new List<Transform>();
    public List<Transform> GetCollectedObjects
    {
        get
        {
            return collectedObject;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            CollectObject(other.transform);
            print("COLLÝDED");
            other.tag = "Untagged";
        }
    }
    private void Update()
    {
        if (transform.position.x != 0)
        {
           // StartCoroutine(AllignWithThPlayer());
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

        float zOffset = 1;
        for (int i = 1; i <= collectedObject.Count - 1; i++)
        {

            if (i == 1)
            {
                collectedObject[i].transform.localPosition =
                    new Vector3(transform.position.x, collectedObject[i].localPosition.y, transform.position.z + zOffset);
            }

            collectedObject[i].transform.localPosition =
                new Vector3(collectedObject[i - 1].localPosition.x, collectedObject[i].localPosition.y, collectedObject[i - 1].localPosition.z + zOffset);

            // yield return new WaitForSeconds(0.3f);
           
        }
        yield return StartCoroutine(ScallingObjects());
        yield return StartCoroutine(DeScallingObjects());

    }
    IEnumerator ScallingObjects()
    {
        for (int i = 1; i <= collectedObject.Count - 1; i++)
        {
            collectedObject[i].transform.localScale = collectedObject[i].transform.localScale * 1.3f;
            yield return null;
        }
    }
    IEnumerator DeScallingObjects()
    {
        for (int i = 1; i <= collectedObject.Count - 1; i++)
        {
            collectedObject[i].transform.localScale = collectedObject[i].transform.localScale / 1.3f;
            yield return null;
        }
    }
    public IEnumerator AllignWithThPlayer()
    {
        for (int i = 1; i <= collectedObject.Count - 1; i++)
        {

          
            Vector3 desiredPosition =
                 new Vector3(collectedObject[i - 1].localPosition.x, collectedObject[i].localPosition.y, collectedObject[i].localPosition.z);
            collectedObject[i].transform.localPosition = Vector3.MoveTowards(collectedObject[i].transform.localPosition, desiredPosition, lerpSpeed * Time.deltaTime);
            yield return new WaitForSeconds(alligningSpeed);  

           //yield return null;
        }
       
    }

}