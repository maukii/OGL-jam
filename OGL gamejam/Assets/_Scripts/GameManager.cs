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
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public static GameManager Instance;

    [SerializeField] private float actionPhaceTime = 3f;

    // Use these to assaing player names and graphics
    [SerializeField] private List<PlayerData> playerDatas = new List<PlayerData>(4); 
    public List<Character> Players;
    [SerializeField] private List<ActionData> roundActions = new List<ActionData>();


    [Header("Turn stuff")]
    [SerializeField] private float actionWaitTime = .75f;
 
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
            Players.Sort((x,y) => x.ID.CompareTo(y.ID));
    }

    // Call this when all players have made their choice
    public void RoundEnd()
    {

        StartCoroutine(ExecuteActions());

    }

    private IEnumerator ExecuteActions()
    {
        // Get enemys turn
        FindObjectOfType<EnemyAI>().Think();

        // Execute them (in order)
        foreach (var action in roundActions)
        {
            action.ExecuteAction();
            yield return new WaitForSeconds(actionWaitTime);
        }

        // Clear the roundActions list
        roundActions.Clear();

        // Start a new round
        StartCoroutine(StartNextRound());
    }

    private IEnumerator StartNextRound()
    {
        yield return new WaitForSeconds(actionPhaceTime);
        for(int i=0;i<GameManager.Instance.Players.Count;i++)
        {
            if(!GameManager.Instance.Players[i].isDead)
            {
                PlayerTurnUI.Instance.StartPlayerTurn(Players[i]);
                break;
            }
        }

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
