using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutTest : MonoBehaviour
{
    public Transform camPivot;
    public Transform cam;
    float heading;
   public float museSensitivity=0.2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heading += Input.GetAxis("Mouse X")*Time.deltaTime*180*museSensitivity;
        camPivot.rotation = Quaternion.Euler(0, heading, 0);

        Vector2 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 camF = cam.forward;
        Vector3 camR= cam.right;

        camF.y = 0;
        camR.y = 0;

        camF=camF.normalized;
        camR=camR.normalized;   

       // transform.position+=new Vector3(input.x,0, input.y)*Time.deltaTime*5;
        transform.position+=(input.y*camF+camR*input.x)*Time.deltaTime*5;
    }
}
