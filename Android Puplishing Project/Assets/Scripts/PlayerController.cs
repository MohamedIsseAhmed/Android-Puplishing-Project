using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private float movemntSpeed = 5;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Vector3 xOffset;
    private Transform firstObject;

    CollisonHandler collisonHandler;

    private bool stopped = false;
    public bool IsStopped { get { return stopped; } set { stopped = value; } }   
    private void Awake()
    {
        collisonHandler = GetComponent<CollisonHandler>();
    }

    void Update()
    {
        if(!GameManager.instance.pauseTheGame)
             InputHandling();
    }

    private void InputHandling()
    {
        float mouseX = Input.GetAxis("Mouse X");


        transform.Translate(Vector3.forward * movemntSpeed * Time.deltaTime, Space.World);

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                firstObject = collisonHandler.GetCollectedObjects[0].transform;
                Vector3 hitpoint = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                Vector3 clampedVector = hit.point;
                clampedVector.y = firstObject.localPosition.y;
                clampedVector.z = firstObject.localPosition.z;

                transform.position = Vector3.MoveTowards(transform.position, hitpoint+xOffset, lerpSpeed * Time.deltaTime);


                firstObject.localPosition =
                    Vector3.MoveTowards(firstObject.localPosition, clampedVector, lerpSpeed * Time.deltaTime);

             
                collisonHandler.StartCoroutine(collisonHandler.AllignWithThPlayer());

            }
        }
        
    }
}
