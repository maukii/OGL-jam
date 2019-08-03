using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    public Character[] Players { get { return players; } }
    [SerializeField] private Character[] players = new Character[4];


    [SerializeField] private List<ActionData> roundActions = new List<ActionData>();

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


    // Call this when all players have made their choice
    public void RoundEnd()
    {

        // Send message to enemy to make it's decision and add it to the list

        // Execute them (in order)
        foreach (var action in roundActions)
        {
            action.ExecuteAction();
        }

        // Clear the roundActions list
        roundActions.Clear();

    }


    public void StartTurn()
    {

    }


    public void SavePlayerData(int playerNumber, string name, CharacterData data)
    {
        // Save player inputs (name and character)
        players[playerNumber].PlayerName = name;
        players[playerNumber].CharacterProfile = data.profile;
    }


    // Creates a new action and saves to list of roundActionss
    public void MakeAction(Character user, Character target, Action action, SOBaseData item)
    {
        ActionData newAction = new ActionData(user, target, action, item);
        roundActions.Add(newAction);
    }


}
