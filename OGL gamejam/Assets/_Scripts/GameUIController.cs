using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    private Character choosingPlayer;

    [Header("Interaction UI")]

    // Use these with new UI
    [SerializeField] private GameObject actionScreenUI;
    [SerializeField] private GameObject privateScreenUI;

    [SerializeField] private GameObject menuBasicUI;
    [SerializeField] private GameObject menuInventoryUI;
    [SerializeField] private GameObject menuTargetUI;
    [SerializeField] private GameObject tumbleweed;


    [Header("Player dependant")]

    // Player stuff
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMPro.TMP_Text characterName;
    [SerializeField] private TMPro.TMP_Text className;


    [Header("Other player buttons")]
    [SerializeField] private TMPro.TMP_Text[] playerNames = new TMPro.TMP_Text[4];

    // Update these params as player clicks UI buttons 
    private ActionData action = new ActionData();

    private void Awake()
    {

        for (int i = 0; i < GameManager.Instance.Players.Count; i++)
        {
            playerNames[i].text = GameManager.Instance.Players[i].characterData.playerName;
        }

    }

    #region public UI funcs

    // Does basic UI reset when turn has ended
    public void TurnChange(Character newPlayer)
    {
        choosingPlayer = newPlayer;

        tumbleweed.SetActive(false);
        actionScreenUI.SetActive(false);
        privateScreenUI.SetActive(true);

        // Change player spesific stuff
        portraitImage.sprite = newPlayer.characterData.character.Icon;
        characterName.text = newPlayer.characterData.playerName;
        className.text = newPlayer.characterData.character.Description;

        ChangeOpenMenu(menuBasicUI);
        action.user = choosingPlayer;
    }

    public void ConfirmAction()
    {
        // Save actual attack event somewhere to wait for action phace
        GameManager.Instance.MakeAction(action);
    }

    public void ChangeActionAttack()
    {
        action.action = Action.Attack;
    }
    public void ChangeActionHeal()
    {
        action.action = Action.Heal;
    }

    public void ChangeTarget(Character target)
    {
        action.target = target;
    }

    public void ChangeOpenMenu(GameObject whatToOpen)
    {
        menuBasicUI.SetActive(false);
        menuInventoryUI.SetActive(false);
        menuTargetUI.SetActive(false);

        whatToOpen.SetActive(true);
    }

    #endregion


}
