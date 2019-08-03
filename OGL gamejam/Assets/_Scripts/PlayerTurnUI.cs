using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTurnUI : MonoBehaviour
{

    public static PlayerTurnUI Instance;

    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text playerNameText;
    public Button okButton;


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
        headerText.text = "";
        playerNameText.text = player.characterData.playerName;
        gameObject.SetActive(true);
    }


    public void StartPlayerTurn(string playerName) // Used to set text to Player 1's turn etc.
    {
        headerText.text = "Character creation";
        playerNameText.text = playerName;
        gameObject.SetActive(true);
    }

}
