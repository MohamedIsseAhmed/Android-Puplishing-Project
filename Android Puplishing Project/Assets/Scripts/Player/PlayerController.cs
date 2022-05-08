using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private float movemntSpeed = 5;
    [SerializeField] private float maxXrange = 8f;
    [SerializeField] private float minXrange = -6f;

     private Rigidbody rigidbody;
    [SerializeField] private Vector3 xOffset;
    private Transform firstObject;

    CollisonHandler collisonHandler;

    private bool stopped = false;
    public bool IsStopped { get { return stopped; } set { stopped = value; } }

    private Vector3 movementVector;
    [SerializeField] private LoadingTest loadingTest;
    private Vector3 touchOrigin;
    private Vector3 desiredPosition;
   
    private void Awake()
    {
        collisonHandler = GetComponent<CollisonHandler>();
       rigidbody = GetComponent<Rigidbody>();
       
    }
    private void Start()
    {
       
    }
    void Update()
    {
        if (GameManager.Instance.Failed) return;
        if(!GameManager.Instance.pauseTheGame)
             InputHandling();
     
    }
    
    private void InputHandling()
    {

      

        transform.Translate(Vector3.forward * movemntSpeed * Time.deltaTime,Space.World);

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {

                if (collisonHandler.GetCollectedObjects.Count > 0)
                {
                    firstObject = collisonHandler.GetCollectedObjects[0].transform;
                }
                    
                Vector3 hitpoint = new Vector3(hit.point.x, 0.33f, transform.position.z);
                Vector3 clampedVector = hit.point.normalized;
                if(firstObject != null)
                {
                    clampedVector.y = 0.2f;
                    clampedVector.z = firstObject.localPosition.z;
                }
               

                transform.position = Vector3.MoveTowards(transform.position, hitpoint, lerpSpeed * Time.deltaTime);
             
             

                if (collisonHandler.GetCollectedObjects.Contains(firstObject))
                {
                    firstObject.localPosition =
                  Vector3.MoveTowards(firstObject.localPosition, clampedVector, lerpSpeed * Time.deltaTime);
                }
                if (collisonHandler.GetCollectedObjects.Count>0)
                    collisonHandler.StartCoroutine(collisonHandler.AllignWithThPlayer());

            }
        }
#if UNITY_ANDROID
        ClampingXRange();
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);
            print(firstTouch.position);
            if (firstTouch.phase == TouchPhase.Began)
            {
                touchOrigin = firstTouch.position;
            }
            else if (firstTouch.phase == TouchPhase.Moved && touchOrigin.x >= 0)
            {
                Vector3 tochEnd = firstTouch.position;
                float x = tochEnd.x - touchOrigin.x;
                float y = tochEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    desiredPosition.x += x > 0 ? 3 : -3;
                }
                else
                {
                    desiredPosition.y = +y > 0 ? 3 : -3;
                }
                print("left or right");
            }
            else if (firstTouch.phase == TouchPhase.Canceled || firstTouch.phase == TouchPhase.Ended)
            {
                desiredPosition = transform.position;
            }
        }
        Vector3 des = transform.position;
        des.x=Mathf.Lerp(des.x, desiredPosition.x,5*Time.deltaTime);
        transform.position = des;
        #endif 

    }
    //NOT IN USE
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
