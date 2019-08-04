using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    public static GameUIController Instance;

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


    [Header("Other players")]
    [SerializeField] private Button[] playerButtons = new Button[4];

    // Update these params as player clicks UI buttons 
    private ActionData action = new ActionData();


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {

        for (int i = 0; i < GameManager.Instance.Players.Count; i++)
        {
            playerButtons[i].GetComponentInChildren<TMPro.TMP_Text>().text = GameManager.Instance.Players[i].characterData.playerName;
        }

        PlayerTurnUI.Instance.StartPlayerTurn(GameManager.Instance.Players[0]);

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

        // Check ID
        int id = choosingPlayer.ID + 1;
        if (id < GameManager.Instance.Players.Count)
            PlayerTurnUI.Instance.StartPlayerTurn(GameManager.Instance.Players[choosingPlayer.ID + 1]);
        else
        {
            actionScreenUI.SetActive(true);
            privateScreenUI.SetActive(false);
            GameManager.Instance.RoundEnd();
        }
    }

    public void ChangeActionAttack()
    {
        action.action = Action.Attack;
        action.item = choosingPlayer.characterData.character.mainWeapon;
    }
    public void ChangeActionHeal()
    {
        action.action = Action.Heal;
        action.item = choosingPlayer.characterData.character.potion;
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
