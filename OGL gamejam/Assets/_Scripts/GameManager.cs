using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public bool isTrator;
    public SOCharacterProfile character;

    public PlayerData(string playerName, SOCharacterProfile character)
    {
        this.playerName = playerName;
        this.character = character;
    }

}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private float actionPhaceTime = 3f;

    // Use these to assaing player names and graphics
    [SerializeField] private List<PlayerData> playerDatas = new List<PlayerData>(4); 
    public List<Character> Players;
    [SerializeField] private List<ActionData> roundActions = new List<ActionData>();


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        int trator = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < 4; i++)
        {
            PlayerData data = new PlayerData("", null);
            if (trator == i)
                data.isTrator = true;

            playerDatas.Add(data);
        }
    }

    public void AssignPlayer(int id, Character character)
    {
        if(id<playerDatas.Count)
        {
            character.characterData = playerDatas[id];
            Players.Add(character);
        }
        else
        {
            Debug.Log("Failed to assign " + id);
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

        StartCoroutine(StartNextRound());

    }

    private IEnumerator StartNextRound()
    {
        yield return new WaitForSeconds(actionPhaceTime);
        PlayerTurnUI.Instance.StartPlayerTurn(Players[0]);
    }


    // Call this when player enters name and chooses character and confirms
    public void SavePlayerData(int playerNumber, string name, CharacterData data)
    {
        playerDatas[playerNumber - 1].playerName = name;
        playerDatas[playerNumber - 1].character = data.profile;
    }


    public void InitializePlayer()
    {
        // Loop throught
    }


    // Creates a new action and saves to list of roundActionss
    public void MakeAction(Character user, Character target, Action action, SOBaseData item)
    {
        ActionData newAction = new ActionData(user, target, action, item);
        roundActions.Add(newAction);
    }
    public void MakeAction(ActionData action)
    {
        roundActions.Add(action);
    }


}
