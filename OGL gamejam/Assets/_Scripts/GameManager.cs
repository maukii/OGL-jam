using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    [SerializeField] private Character[] players = new Character[4];

    // TODO:: Keep track of which players turn it is

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new Character();
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


    public void SavePlayerData(int playerNumber, string name/*, characterp characterChoice*/)
    {
        // Save player inputs (name and character)
        players[playerNumber].PlayerName = name;
        //players[playerNumber].CharacterProfile = characterChoice;
    }

    // TODO:: Make some UI controller to allow players to use when calling turn decision actions 
    //      --> Attack
    //      --> Heal
    //      --> DoSomethingElse



}
