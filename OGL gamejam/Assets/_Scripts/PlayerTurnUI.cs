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

    public void PlayerTurnEnd()
    {

    }
    
    public void PlayerTurnStart(/*Player newPlayer*/)
    {
        playerNameText.text = /*newPlayer.playerName + */"'s turn!";
        gameObject.SetActive(true);
    }


    public void StartPlayerTurn(string playerName)
    {
        playerNameText.text = playerName + "'s turn!";
        gameObject.SetActive(true);
    }

}
