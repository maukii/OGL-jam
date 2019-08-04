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
    [SerializeField] private GameObject menuWeapon;
    [SerializeField] private GameObject tumbleweed;

    [SerializeField] private GameObject tratorUI;

    [SerializeField] private Image privateHealthBar;

    [Header("Weapon UI")]

    [SerializeField] private TMPro.TMP_Text weaponName;
    [SerializeField] private TMPro.TMP_Text weaponDescription;
    [SerializeField] private TMPro.TMP_Text weapon1;
    [SerializeField] private TMPro.TMP_Text weapon2;

    [SerializeField] private GameObject[] weaponDamage = new GameObject[5];
    [SerializeField] private GameObject[] weaponAccuracy = new GameObject[5];


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

        // Update private health bar in UI
        privateHealthBar.fillAmount = choosingPlayer.HP / choosingPlayer.profile.MaxHP;

        // Traitor UI
        if (choosingPlayer.characterData.isTrator)
            tratorUI.SetActive(true);
        else
            tratorUI.SetActive(false);

        // Weapon UI
        weapon1.text = choosingPlayer.characterData.character.mainWeapon.Name;
        weapon2.text = choosingPlayer.characterData.character.secondaryWeapon.Name;

        weaponName.text = choosingPlayer.characterData.character.mainWeapon.name;
        weaponDescription.text = choosingPlayer.characterData.character.mainWeapon.Description;

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
        int id = -1;
        for(int i=choosingPlayer.ID+1;i<GameManager.Instance.Players.Count;i++)
        {
            if(!GameManager.Instance.Players[i].isDead)
            {
                PlayerTurnUI.Instance.StartPlayerTurn(GameManager.Instance.Players[i]);
                id = i;
                break;
            }
        }
        if (id ==-1)
        {
            actionScreenUI.SetActive(true);
            privateScreenUI.SetActive(false);

            EndRound();
        }
            
    }


    private void EndRound()
    {
        GameManager.Instance.RoundEnd();
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


    public void ChangeWeapon(bool main)
    {
        if (main)
        {
            action.item = choosingPlayer.characterData.character.mainWeapon;
            weaponName.text = choosingPlayer.characterData.character.mainWeapon.name;
            weaponDescription.text = choosingPlayer.characterData.character.mainWeapon.Description;

            int damage = Mathf.RoundToInt(choosingPlayer.characterData.character.mainWeapon.Damage / 20);
            int accuracy = Mathf.RoundToInt(choosingPlayer.characterData.character.mainWeapon.HitChance / 20);

            for (int i = 0; i < weaponDamage.Length; i++)
            {
                weaponDamage[i].SetActive(damage >= i);
                weaponAccuracy[i].SetActive(accuracy >= i);
            }
        }
        else
        {
            action.item = choosingPlayer.characterData.character.secondaryWeapon;
            weaponName.text = choosingPlayer.characterData.character.secondaryWeapon.name;
            weaponDescription.text = choosingPlayer.characterData.character.secondaryWeapon.Description;

            int damage = Mathf.RoundToInt(choosingPlayer.characterData.character.secondaryWeapon.Damage / 20);
            int accuracy = Mathf.RoundToInt(choosingPlayer.characterData.character.secondaryWeapon.HitChance / 20);

            for (int i = 0; i < weaponDamage.Length; i++)
            {
                weaponDamage[i].SetActive(damage >= i);
                weaponAccuracy[i].SetActive(accuracy >= i);
            }
        }
    }


    public void ChangeOpenMenu(GameObject whatToOpen)
    {
        menuBasicUI.SetActive(false);
        menuInventoryUI.SetActive(false);
        menuTargetUI.SetActive(false);
        menuWeapon.SetActive(false);

        whatToOpen.SetActive(true);
    }

    #endregion


}
