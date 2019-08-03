using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInitialization : MonoBehaviour
{

    [SerializeField] private TMP_InputField playerNameInputField;
    private PlayerTurnUI turnUI;
    private int playerTurn = 1;

    private GameObject activeCharacter;


    private void Start()
    {
        // Enable players turn UI and
        turnUI = PlayerTurnUI.Instance;
        turnUI.StartPlayerTurn("Player " + playerTurn.ToString());
    }

    public void HighilghtCharacter(GameObject choice)
    {
        if(activeCharacter != null && activeCharacter != choice)
        {
            RemoveCharacterHighlight(activeCharacter);
        }

        activeCharacter = choice;
        choice.transform.localScale = Vector3.one * 1.1f;
        // Highlight somehow
    }

    private void RemoveCharacterHighlight(GameObject go)
    {
        go.transform.localScale = Vector3.one;
        // Remove highlight
    }

    public void Confirm()
    {
        if(playerNameInputField.text == string.Empty) // Can also compare to existing player names
        {
            Debug.LogError("Enter player name before continuing");
            return;
        }
        else if(activeCharacter == null)
        {
            Debug.LogError("Choose a character before continuing");
            return;
        }

        // Save player name and characterChoice to GameManager
        GameManager.Instance.SavePlayerData(playerTurn, playerNameInputField.text, activeCharacter.GetComponent<CharacterData>());

        // Disable chosenCharacter so others can't choose same one
        var button = activeCharacter.GetComponent<Button>();
        if (button == null)
            button = activeCharacter.GetComponentInChildren<Button>();

        if(button != null)
        {
            button.interactable = false;
            // Change character to grayed out
            // Save name and character to players data

            playerNameInputField.text = "";
            RemoveCharacterHighlight(activeCharacter);
            activeCharacter = null;
        }

        // Call end turn after this is done
        if (playerTurn < 4)
        {
            playerTurn++;
            turnUI.StartPlayerTurn("Player " + playerTurn);
        }
        else
        {
            // All players have picked a character
            SceneChangeManager.Instance.DoFadeOut();
        }
    }

}
