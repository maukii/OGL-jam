using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    [SerializeField] private GameObject sherifWon;
    [SerializeField] private GameObject playersWon;


    private void Start()
    {
        ToggleUI(GameManager.Instance.sherifDead);
    }

    private void ToggleUI(bool sherifDead)
    {
        sherifWon.SetActive(false);
        playersWon.SetActive(false);

        if(sherifDead)
        {
            playersWon.SetActive(true);
        }
        else
        {
            sherifWon.SetActive(true);
        }
    }

    // Move to main menu and reset stuff
    public void MainMenu()
    {
        // Reset everything
        GameManager.Instance.Players.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
