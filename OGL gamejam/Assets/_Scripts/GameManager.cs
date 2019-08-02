using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    [SerializeField] private bool debugGameStateChanges = false;

    // TODO:: Keep track of which players turn it is

    public enum GameState
    {
        Loading,
        InitializingPlayers,
        ShowTurnUI,
        ChoosingPhace,
        ActionPhace,
    }

    // Other scripts can listen to this event by subscribing with:: GameManager.Instance += FunctionToHappenWhenStateChanges
    [SerializeField] private GameState currentGameState = GameState.Loading;
    public delegate void GameStateChange();
    public static event GameStateChange OnGameStateChange;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    // Check for TurnEnd() funs here and if all turns have been played change state to action state
    public void ChangeGameState(GameState newState)
    { 
        if(currentGameState != newState)
        {
            // Swap to new state
            currentGameState = newState;

            if (debugGameStateChanges) Debug.Log("GameState changed to: " + currentGameState);

            // Send info about the game state change
            OnGameStateChange?.Invoke();
        }

        if ((int)currentGameState == Enum.GetValues(typeof(GameState)).Length)
            currentGameState = 0;

    } 


    // Call when players time ends or player has made decision for this turn
    public void EndTurn()
    {
        // Change turn to next player 

        // If previus turn was last players --> change GameState to action phace

        // Disable user UI so we'r ready to give turn to next player
        // Give turn to next player (func below)

        
    }


    // When end turn is called get the next player in the turn list 
    private void NewPlayersTurn(/*Player newPlayer*/)
    {

        // New player to play their turn is passed as param
        // Enable user UI so they can make their decisions for this turn
        
        // Check from newPlayer if they are marked as traitor and enable traitor UI as well

    }



    // TODO:: Make some UI controller to allow players to use when calling turn decision actions 
    //      --> Attack
    //      --> Heal
    //      --> DoSomethingElse



}
