using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    [SerializeField] private Text playerXTurnText;
    [SerializeField] private InputField choosingPlayerNameField;
    [SerializeField] private GameObject[] characterChoices = new GameObject[4];


    public void ChooseCharacter(GameObject character)
    {
        if(choosingPlayerNameField.text == string.Empty)
        {
            Debug.LogError("Enter player name before continuing!");
            return;
        }

        // Remove this character from choosable characteres
        // We need to get a reference to the choosing player and save this character to the players data

        gameObject.GetComponent<Button>().interactable = false;
        GameManager.Instance.EndTurn();

    }


    // Change this to get the actual data from the player and use it to change the player name text and inform whos turn it is
    public void PlayerTurnChanged(/*Player newPlayer*/)
    {
        playerXTurnText.text = /*newPlayer.data.name +*/ "'s turn!";
    }

}
