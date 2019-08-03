using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnUI : MonoBehaviour
{

    public static PlayerTurnUI Instance;


    [SerializeField] private Text playerNameText;
    [SerializeField] private string currentPlayerName;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameObject.SetActive(false);
    }

    
    public void StartPlayerTurn(Character player) // Use when players name is known
    {
        playerNameText.text = player.characterData.playerName + "'s turn!";
        gameObject.SetActive(true);
    }


    public void StartPlayerTurn(string playerName) // Used to set text to Player 1's turn etc.
    {
        playerNameText.text = playerName + "'s turn!";
        gameObject.SetActive(true);
    }

}
