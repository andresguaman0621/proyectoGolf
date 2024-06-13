using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsRemainSystem : MonoBehaviour
{

    [SerializeField] public GameObject menuGameOver;
    [SerializeField] public GameObject wonMenu;
    public int remainingShots;
    public Text shotsText;
    public bool hasWon = false;


    public int yellowCoinsCollected = 0; // Contador de monedas amarillas recolectadas
    public Text yellowCoinText; // Referencia al texto del canvas para mostrar el contador
    public float powerUpDuration = 3f;

    //BALL

    void Start()
    {
        TextUpdate();
        //remainingShots;
        

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



    public void IncreaseYellowCoinCount()
    {
        yellowCoinsCollected++;
        UpdateYellowCoinText();
    }

    public void UpdateYellowCoinText()
    {
        yellowCoinText.text = "Yellow Coins: " + yellowCoinsCollected;
    }


    public void IncreaseMovements()
    {
        remainingShots++;
        TextUpdate();
    }
}


