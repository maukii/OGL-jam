using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int id;
    public string name;
    public GameObject chosenCharacter;
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    [SerializeField] private bool debugGameStateChanges = false;

    [SerializeField] private Player[] players = new Player[4];

    // TODO:: Keep track of which players turn it is

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }


    // Call when players time ends or player has made decision for this turn
    public void EndTurn()
    {
        // Change turn to next player 

        // If previus turn was last players --> change GameState to action phace

        // Disable user UI so we'r ready to give turn to next player
        // Give turn to next player (func below)

        
    }


    public void StartTurn()
    {

    }


    public void SavePlayerData(int playerNumber, string name, GameObject characterChoice)
    {
        // Save player inputs (name and character)
        players[playerNumber].name = name;
        players[playerNumber].chosenCharacter = characterChoice;
    }

    // TODO:: Make some UI controller to allow players to use when calling turn decision actions 
    //      --> Attack
    //      --> Heal
    //      --> DoSomethingElse



}
