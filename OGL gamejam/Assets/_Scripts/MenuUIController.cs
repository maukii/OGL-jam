using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{

    [SerializeField] private GameObject mainmenuUI;
    [SerializeField] private GameObject optionsUI;


    public void StartGame()
    {
        SceneChangeManager.Instance.DoFadeOut();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // works too
    }

    public void Options(bool active)
    {
        // Toggle options
        mainmenuUI.SetActive(!active);
        optionsUI.SetActive(active);
    }

    public void ExitGame()
    {
        // Exit game
        Application.Quit();
    }

}
