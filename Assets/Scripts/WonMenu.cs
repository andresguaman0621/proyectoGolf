using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WonMenu : MonoBehaviour
{
    public Text movementsText;
    public ShotsRemainSystem shotsRemainSystem;
    private void Update()
    {
        TextUpdate();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void TextUpdate()
    {

        movementsText.text = "Remaining shots: " + shotsRemainSystem.remainingShots;

    }
    public void NextLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
