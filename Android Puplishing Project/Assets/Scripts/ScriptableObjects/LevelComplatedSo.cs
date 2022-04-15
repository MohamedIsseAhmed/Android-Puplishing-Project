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
        for (byte i = 0; i < playerBalls.Count; i++)
        {
            CollisonHandler.Instance.GetCollectedObjects[i].parent = null;
            CollisonHandler.Instance.GetCollectedObjects[i].position =
                Vector3.Lerp(CollisonHandler.Instance.GetCollectedObjects[i].position, FinishCourtsParent.GetChild(i).position+new Vector3(0,0.3f,0), lerpSpeed * Time.deltaTime);

            yield return new WaitForSeconds(waitTime);
            
        } 
        OnlevelComplated?.Invoke(); 
    }

}
