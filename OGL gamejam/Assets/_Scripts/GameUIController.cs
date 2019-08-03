using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    private Character choosingPlayer;

    // Base UI windows
    [SerializeField] private GameObject rootUI;
    [SerializeField] private GameObject basicActionsUI;
    [SerializeField] private GameObject traitorUI;

    // Action spesific extra windows
    [SerializeField] private GameObject attackWindowUI;
    [SerializeField] private GameObject inventoryWindowUI;

    [SerializeField] private Button[] targetButtons; // All others than the player
    private Character[] otherPlayers = new Character[3];

    // This will manage turn changes and UI updateing 
    // Take player input from UI buttons
    // Send message when coice has been made


    #region public UI funcs

    // Does basic UI reset when turn has ended
    public void TurnChange(Character newPlayer)
    {
        choosingPlayer = newPlayer;

        rootUI.SetActive(true);
        basicActionsUI.SetActive(true);
        traitorUI.SetActive(newPlayer.characterData.isTrator);

        attackWindowUI.SetActive(false);
        inventoryWindowUI.SetActive(false);

        otherPlayers = GetOtherPlayers();

        // Update targets list (names and references)
        for (int i = 0; i < otherPlayers.Length; i++)
        {
            targetButtons[i].GetComponentInChildren<TMPro.TMP_Text>().text = otherPlayers[i].characterData.playerName;
        }

    }

    private Character[] GetOtherPlayers()
    {
        List<Character> otherCharacters = new List<Character>();
        List<Character> players = GameManager.Instance.Players;

        foreach (Character player in players)
        {
            if (player != choosingPlayer)
                otherCharacters.Add(player);
        }

        return otherCharacters.ToArray();
    }

    public void ToggleAttackWindow(bool active)
    {
        attackWindowUI.SetActive(active);
    }

    public void Attack()
    {
        // Save actual attack event somewhere to wait for action phace
        //GameManager.Instance.MakeAction(choosingPlayer, )
    }

    public void ToggleInventory(bool active)
    {
        inventoryWindowUI.SetActive(active);
    }

    // Add more as we need

    #endregion


}
