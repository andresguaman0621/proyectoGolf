using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCoin : MonoBehaviour
{
    public ShotsRemainSystem shotsSystem;
    [SerializeField] public AudioSource Collect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            shotsSystem.IncreaseMovements(); // Aumenta el número de movimientos disponibles
            Destroy(gameObject);
            Collect.Play();
        }
    }
}
