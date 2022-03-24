using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColorChanger : MonoBehaviour
{
    MeshRenderer meshRenderer;

    private int minRandomValue=0;
    private int maxRandomValue=3;


    
    [SerializeField]  private float hueMin = 0f;
    [SerializeField]  private float hueMax = 1;
    [SerializeField]  private float saturationMin = 0.5f;
    [SerializeField]  private float saturationMax = 0.75f;
    [SerializeField]  private float valueMin = 0.5f;
    [SerializeField]  private float valueMax = 1f;
    [SerializeField]  private float alphaMin = 0f;
    [SerializeField]  private float alphaMax = 0.5f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        //System.Random rand=new System.Random();
        //bool randomBool= rand.Next(minRandomValue, maxRandomValue) % 2 == 0;
       // meshRenderer.material.color = randomBool ? new Color(0, 0.5f, 0, 1f) : new Color(0f, 0.4782309f, 0.7511321f, 1f);
        meshRenderer.material.color  = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);
    }

  
}
