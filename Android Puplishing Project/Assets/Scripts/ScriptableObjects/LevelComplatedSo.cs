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

    public IEnumerator GoToCourt(List<Transform> playerBalls)
    {
        for (int i = 0; i < playerBalls.Count; i++)
        {
            CollisonHandler.instance.GetCollectedObjects[i].parent = null;
            CollisonHandler.instance.GetCollectedObjects[i].position =
                Vector3.Lerp(CollisonHandler.instance.GetCollectedObjects[i].position, FinishCourtsParent.GetChild(i).position, lerpSpeed * Time.deltaTime);

            yield return new WaitForSeconds(waitTime);
        } 
        OnlevelComplated?.Invoke(); 
    }
    
   
}
public enum UITypeOnFinish
{
    Fialed,
    LevelComplated
}