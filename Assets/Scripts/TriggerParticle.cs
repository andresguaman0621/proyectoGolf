using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticle : MonoBehaviour
{
    public ShotsRemainSystem shotsRemainSystem;
    private ParticleSystem particles;
    [SerializeField] public AudioSource winAudio;
    [SerializeField] public GameObject wonMenuCanvas;
    private WonMenu wonMenuScript;
    
    

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        var emission = particles.emission;
        emission.enabled = false;
        wonMenuScript = wonMenuCanvas.GetComponent<WonMenu>();

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            var emission = particles.emission;
            emission.enabled = true;
            winAudio.Play();

            //wonMenuCanvas.SetActive(true);
            //wonMenuScript.TextUpdate();
            StartCoroutine(ActivateWonMenu());
            shotsRemainSystem.hasWon = true;
            shotsRemainSystem.hasWon = true; 
        }
    }

    IEnumerator ActivateWonMenu()
    {
        yield return new WaitForSeconds(2.5f); // Wait 

        wonMenuCanvas.SetActive(true);
        wonMenuScript.TextUpdate();
    }




}
