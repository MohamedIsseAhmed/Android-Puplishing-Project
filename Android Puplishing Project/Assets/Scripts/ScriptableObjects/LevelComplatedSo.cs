using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName ="UI State",menuName ="State On Finish", order =0)]
public class LevelComplatedSo : ScriptableObject
{

    [SerializeField] private Transform FinishCourtsParent;
    [SerializeField] private float waitTime = 0.10f;
    [SerializeField] private float lerpSpeed = 10f;
    public static event Action OnlevelComplated;
    private Vector3 offset=new Vector3(0,0.3f,0);
    public IEnumerator GoToCourt(List<Transform> playerBalls)
    {
       
        for (int i = playerBalls.Count-1; i >= 0; i--)
        {
            int currentIndex = playerBalls.Count - 1 - i;
            
            CollisonHandler.Instance.GetCollectedObjects[i].parent = null;
            CollisonHandler.Instance.GetCollectedObjects[i].position =
                Vector3.Lerp(CollisonHandler.Instance.GetCollectedObjects[i].position, FinishCourtsParent.GetChild(currentIndex).position  + offset,
                lerpSpeed * Time.deltaTime);
          
            yield return new WaitForSeconds(waitTime);
            
        } 
        OnlevelComplated?.Invoke(); 
    }

}
