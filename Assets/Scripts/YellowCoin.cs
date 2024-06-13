using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowCoin : MonoBehaviour
{
    

    public ShotsRemainSystem shotsSystem;
    [SerializeField] public AudioSource powerUp;
    [SerializeField] public AudioSource Clock;
    [SerializeField] public AudioSource Collect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            
       
            shotsSystem.IncreaseYellowCoinCount();
            Destroy(gameObject);
            Collect.Play();

            if (shotsSystem.yellowCoinsCollected >= 3)
            {
                // Activa el PowerUp (movimiento de la bola con teclas AWSD)
                powerUp.Play();
                other.GetComponent<Ball>().ActivatePowerUp();
                Clock.Play();
                Debug.Log("inica el power up"); 
            }
        }

    }

   

}
