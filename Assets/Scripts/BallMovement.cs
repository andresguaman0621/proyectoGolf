using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento de la pelota
    public float powerUpDuration = 3f; // Duración del PowerUp

    private bool isPowerUpActive = false;
    private float powerUpTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPowerUpActive)
        {
            MoveBallWithKeys();
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0f)
            {
                isPowerUpActive = false;
            }
        }
    }

    private void MoveBallWithKeys()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);
    }

    public void ActivatePowerUp()
    {
        isPowerUpActive = true;
        powerUpTimer = powerUpDuration;
    }
}
