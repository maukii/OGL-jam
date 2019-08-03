using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    // Base UI windows
    [SerializeField] private GameObject rootUI;
    [SerializeField] private GameObject basicActionsUI;
    [SerializeField] private GameObject traitorUI;

    // Action spesific extra windows
    [SerializeField] private GameObject attackWindowUI;
    [SerializeField] private GameObject inventoryWindowUI;

    // This will manage turn changes and UI updateing 
    // Take player input from UI buttons
    // Send message when coice has been made


    #region public UI funcs

    // Does basic UI reset when turn has ended
    public void TurnChange(Character newPlayer)
    {
        rootUI.SetActive(true);
        basicActionsUI.SetActive(true);
        traitorUI.SetActive(newPlayer.characterData.isTrator);

        attackWindowUI.SetActive(false);
        inventoryWindowUI.SetActive(false);
    }

    public void ToggleAttackWindow(bool active)
    {
        attackWindowUI.SetActive(active);
    }

    public void Attack()
    {
        // Save actual attack event somewhere to wait for action phace
    }

    public void ToggleInventory(bool active)
    {
        inventoryWindowUI.SetActive(active);
    }

    // Add more as we need

    #endregion


}
