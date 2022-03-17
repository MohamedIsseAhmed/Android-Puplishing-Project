using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private float movemntSpeed = 5;
    [SerializeField] private float maxXrange = 3.3f;
    [SerializeField] private float minXrange = -3.3f;

     private Rigidbody rigidbody;
    [SerializeField] private Vector3 xOffset;
    private Transform firstObject;

    CollisonHandler collisonHandler;

    private bool stopped = false;
    public bool IsStopped { get { return stopped; } set { stopped = value; } }

    private Vector3 movementVector;
    private void Awake()
    {
        collisonHandler = GetComponent<CollisonHandler>();
       rigidbody = GetComponent<Rigidbody>();
       
    }

    void Update()
    {
        if (GameManager.Instance.Failed) return;
        if(!GameManager.Instance.pauseTheGame)
             InputHandling();
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
            LevelManager.Instance.ÝncreaseCurrentLevel();
        }
    }

    private void InputHandling()
    {
        //float mouseX = Input.GetAxis("Mouse X");


        transform.Translate(Vector3.forward * movemntSpeed * Time.deltaTime,Space.World);

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {

                if (GetComponent<CollisonHandler>().GetCollectedObjects.Count > 0)
                {
                    firstObject = collisonHandler.GetCollectedObjects[0].transform;
                }
                    
                Vector3 hitpoint = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                Vector3 clampedVector = hit.point.normalized;
                clampedVector.y = firstObject.localPosition.y;
                clampedVector.z = firstObject.localPosition.z;

                transform.position = Vector3.MoveTowards(transform.position, hitpoint, lerpSpeed * Time.deltaTime);
             
               // ClampingXRange();

                if (GetComponent<CollisonHandler>().GetCollectedObjects.Contains(firstObject))
                {
                    firstObject.localPosition =
                  Vector3.MoveTowards(firstObject.localPosition, clampedVector, lerpSpeed * Time.deltaTime);
                }
                if (GetComponent<CollisonHandler>().GetCollectedObjects.Count>0)
                    collisonHandler.StartCoroutine(collisonHandler.AllignWithThPlayer());

            }
        }
        
    }
    private void ClampingXRange()
    {
        if (transform.position.x > maxXrange)
        {
            transform.position = new Vector3(maxXrange, transform.position.y, transform.position.z);
        }
       if(transform.position.x < minXrange)
        {
            transform.position = new Vector3(minXrange, transform.position.y, transform.position.z);
        }
    }
}
