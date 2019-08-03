using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public static TurnManager Instance;

    private bool updateTimer = false;

    [Range(60, 360)]
    [SerializeField] private float turnTime = 120f; // 2min by default
    [SerializeField] private float timer;

    private List<Character> players = new List<Character>();
    private PlayerTurnUI turnUI;
    [SerializeField] private GameUIController gameUI;

    [HideInInspector] public Character choosingPlayer;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Players list is populated
    private void Start()
    {
        players = GameManager.Instance.Players;
        turnUI = PlayerTurnUI.Instance;

        turnUI.okButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartTurn());

        // Choose who starts / randomize order / go in order ??
        choosingPlayer = players[0];

        turnUI.StartPlayerTurn(choosingPlayer);

        if (gameUI != null)
            gameUI.TurnChange(choosingPlayer);
    }

    private void StartTurn()
    {
        updateTimer = true;
        timer = turnTime;
    }

    private void EndTurn()
    {
        // Send turn action to GameManager
        GameManager.Instance.MakeAction(choosingPlayer, null, Action.Idle, null);

        // Disable ActionUI

        // Give turn to next player if there is players left
        if(choosingPlayer.ID < players.Count)
        {
            choosingPlayer = players[choosingPlayer.ID + 1];
            gameUI.TurnChange(choosingPlayer);
            turnUI.StartPlayerTurn(choosingPlayer);
        }
        // Else send message to GameManager.Instance.EndRound();
        else
        {
            GameManager.Instance.RoundEnd();
        }

    }

    private void Update()
    {
        if (!updateTimer)
            return;

        timer -= Time.deltaTime;
        // Update timer graphics

        if (timer <= 0f)
        {
            updateTimer = false;
            timer = 0f;
            EndTurn();
        }
    }


    private void OnDestroy()
    {
        UnityEngine.UI.Button button = turnUI.okButton;

        if(button != null)
            turnUI.okButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(() => StartTurn());
    }

}
