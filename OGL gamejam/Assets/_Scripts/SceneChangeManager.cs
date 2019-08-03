using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    public static SceneChangeManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FadeIn();

        //if(arg0.name == "Game")
        //{
        //    GameManager.Instance.ChangeGameState(GameManager.GameState.InitializingPlayers);
        //}
    }


    public void LoadNextScene()
    {
        // Just load instantly next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void DoFadeOut()
    {
        FadeOut();
    }

    private void FadeIn()
    {
        // FadeIn
        // just animate
    }

    private void FadeOut()
    {
        // animate FadeOut
        // then change scene

        LoadNextScene();
    }

}
