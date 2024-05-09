using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsRemainSystem : MonoBehaviour
{

    [SerializeField] public GameObject menuGameOver;
    [SerializeField] public GameObject wonMenu;
    public int remainingShots = 3;
    public Text shotsText;
    public bool hasWon = false; 


    void Start()
    {
        TextUpdate();
        remainingShots = 3;
        

    }
    public void TextUpdate()
    {
        shotsText.text = "Remaining Shots: " + remainingShots;
    }


    public void DecreaseMovements()
    {
        if (remainingShots > 0)
        {
            remainingShots--;
            TextUpdate();

            if (remainingShots == 0)
            {
                StartCoroutine(CheckGameOver());
            }
        }
        else
        {
            remainingShots = 0; // Ensure remaining shots cannot go below 0
        }
    }

    IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(2.5f); // Wait 

        if (wonMenu.activeSelf == false && hasWon == false)
        {
            Debug.Log("¡Se han agotado los movimientos!");
            menuGameOver.SetActive(true);
        }
    }

}


