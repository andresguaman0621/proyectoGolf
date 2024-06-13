using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCoin : MonoBehaviour
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
            shotsSystem.DecreaseMovements(); // Disminuye el n�mero de movimientos disponibles
            Destroy(gameObject);
            Collect.Play();
        }
    }
}
