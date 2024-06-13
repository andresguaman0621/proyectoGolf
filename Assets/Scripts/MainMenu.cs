using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    [SerializeField] public AudioSource BackgroundMusic;
    [SerializeField] private Light sceneLight;


    void Awake()
    {
        BackgroundMusic.Play();
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        BackgroundMusic.Stop();
    }
    
        public void Quit()
    {
        Debug.Log("You exit the game");
        Application.Quit();
    }

    public void ToggleLightOnClick()
    {
        if (sceneLight != null)
        {
            sceneLight.enabled = !sceneLight.enabled; // Cambia el estado de la luz
        }
    }
}
