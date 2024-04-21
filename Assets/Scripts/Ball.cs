using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameObject mainCam;
    private Vector3 camZoomOffset;
    private Rigidbody rb;
    private Vector3 oldPosition;

    public float AbsHorizonMagnitude;
    public float Speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera");
        camZoomOffset = transform.position - mainCam.transform.position;
        rb=GetComponent<Rigidbody>();
        oldPosition = transform.position;   
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

        //Slow down    
        AbsHorizonMagnitude = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);

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
}
