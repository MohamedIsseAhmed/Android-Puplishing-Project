using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Court : MonoBehaviour
{
    [SerializeField] private GameObject goalParticle;
    [SerializeField] private float waitTime = 3;

    public static event Action UnPaueTheGame;
    private void OnTriggerEnter(Collider other)
    {
        goalParticle.SetActive(true);
        StartCoroutine(DeactivateParentObject(other.transform));
        print(other.name);
    }
    IEnumerator DeactivateParentObject(Transform ball)
    {
       
        yield return new WaitForSeconds(waitTime);
        this.transform.parent.gameObject.SetActive(false);
        ball.transform.gameObject.SetActive(false);
        goalParticle.SetActive(true);

        UnPaueTheGame?.Invoke();
    }
}
