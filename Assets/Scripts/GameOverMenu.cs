using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverMenu : MonoBehaviour
{

   public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(string name)
    {
        SceneManager.LoadScene(name);
    }



    //kind of pause the game, make tests
    //UnityEditor.EditorApplication.isPlaying = false;

 
}
