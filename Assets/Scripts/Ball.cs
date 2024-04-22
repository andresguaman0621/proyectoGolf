using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Camera mainCam;
    private GameObject putter;
    
    private Rigidbody rb;
    private Vector3 originPosition;
    private Vector3 oldPosition;

    public float AbsHorizonMagnitude;
    public float Speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        putter = GameObject.Find("Putter");
        rb=GetComponent<Rigidbody>();
        originPosition = transform.position;    
        oldPosition = originPosition;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //+X
            rb.AddForce(new Vector3(Speed, 0,0));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //-X
            rb.AddForce(new Vector3(-Speed, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //+Z
            rb.AddForce(new Vector3(0,0, Speed));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //-Z
            rb.AddForce(new Vector3(0, 0, -Speed));
        }

        if (Input.GetKeyDown(KeyCode.Space) || transform.position.y < -10)
        {
            transform.position = originPosition;
            rb.velocity = new Vector3();
            rb.angularVelocity = new Vector3();
        }
        //Slow down    
        AbsHorizonMagnitude = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);

        UpdatePutterPosition();

        UpdateCamera();

        oldPosition = transform.position;   

    }

    private void UpdateCamera()
    {
        //Rotate camera 
        if (Input.GetMouseButton(1))
        {
            mainCam.transform.RotateAround(transform.position,
                                            mainCam.transform.up,
                                            -Input.GetAxis("Mouse X") * 3f);

            mainCam.transform.RotateAround(transform.position,
                                            mainCam.transform.right,
                                            -Input.GetAxis("Mouse Y") * 3f);
            mainCam.transform.eulerAngles = new Vector3
                (
                    mainCam.transform.eulerAngles.x,
                    mainCam.transform.eulerAngles.y,
                    0
                );
        }

        //Follow camera
        mainCam.transform.Translate(transform.position - oldPosition, Space.World);
        //mainCam.transform.position = transform.position - camZoomOffset;
    }

    private void UpdatePutterPosition()
    {
        float putterDistanceAway = 0.7f;

        //Figure out the angle from the ball to the cursor
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        var putterLayer = 1 << LayerMask.NameToLayer("PutterCollider");

        if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo, 10, putterLayer)
            && Vector3.Distance(transform.position, hitInfo.point) > 0.3f)
        {
            Debug.DrawLine(ray.origin, hitInfo.point);

            float deltaY = hitInfo.point.y - transform.position.y;
            float deltaX = hitInfo.point.x - transform.position.x;
            float angle = Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;
            angle *= hitInfo.point.z < transform.position.z ? -1 : 1;

            Debug.Log(angle);
            //Move putter to distance from ball based on found angle
            var newPoint = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
            putter.transform.position = transform.position + (newPoint * putterDistanceAway);
            
        }



    }
}
