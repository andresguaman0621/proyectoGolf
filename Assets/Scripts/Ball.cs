using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{

    
    
    [SerializeField] private float someThreshold = 0.2f;

    //variable for control the turns
    public ShotsRemainSystem shotsRemainSystem;

    //variables for follow camera
    private Camera mainCam;
    private float camZoomLevel;
    private Vector2 camRotationValues;


    //variables for handle control ball
    [SerializeField] private float shotPower;
    [SerializeField] private float stopVelocity = .05f;
    [SerializeField] private LineRenderer lineRenderer;
    public bool isIdle;
    private bool isAiming;
    private Rigidbody rigidbody;



    [SerializeField] public AudioSource respawnAudio;
    [SerializeField] public AudioSource newTurnAudio;

    public Transform respawnPoint;



    // Start is called before the first fraime update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camZoomLevel = (mainCam.transform.position - transform.position).magnitude;
        camRotationValues = new Vector2(mainCam.transform.eulerAngles.x, mainCam.transform.eulerAngles.y);
        rigidbody = GetComponent<Rigidbody>();

    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || transform.position.y < -10)
        {
            transform.position = respawnPoint.position;
            respawnAudio.Play();

            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        if (rigidbody.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        
        //aiming process (changed from fixedupdate to update beacuase it was affecting the shoot)
        ProcessAim();

        UpdateCamera();


    }


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        isAiming = false;
        lineRenderer.enabled = false;
        

    }

    //update camera when shoot
    private void UpdateCamera()
    {

        //Rotate camera 
        if (Input.GetMouseButton(1))
        {
            var mouseMovement = new Vector2(-Input.GetAxis("Mouse Y") * 3f, Input.GetAxis("Mouse X") * 3f);
            camRotationValues += mouseMovement * Time.unscaledDeltaTime * 300f;
        }

        //Follow camera player
        var curRotation = Quaternion.Euler(new Vector3(Mathf.Clamp(camRotationValues.x, -80f, 80f), camRotationValues.y, 0));
        var lookDirection = curRotation * Vector3.forward;
        var lookPosition = transform.position - lookDirection * camZoomLevel;
        mainCam.transform.SetPositionAndRotation(lookPosition, curRotation);

    }

    

    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;

        }

    }

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return;
        }

        Vector3? worldPoint = CastMouseClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }

        DrawLine(worldPoint.Value);

        if (Input.GetMouseButtonUp(0))
        {

            Shoot(worldPoint.Value);
        }


    }


    private void Shoot(Vector3 worldPoint)
    {
        
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        rigidbody.AddForce(direction * strength * shotPower);
        isIdle = false;
        
        //validate minimum movement to consider a shot
        if (Vector3.Distance(transform.position, horizontalWorldPoint) > someThreshold)
        {
            shotsRemainSystem.DecreaseMovements();

        }
        //shotsRemainSystem.DecreaseMovements();

    }


    private void DrawLine(Vector3 wordlPoint)
    {

        Vector3[] positions = {
            transform.position,
            wordlPoint};
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;

    }

    private void Stop()
    {

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        isIdle = true;

        
    }


    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }

        
    }

}

