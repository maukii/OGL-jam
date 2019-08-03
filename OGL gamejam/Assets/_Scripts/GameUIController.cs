using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    private Character choosingPlayer;


    // Use these with new UI
    [SerializeField] private GameObject actionScreenUI;
    [SerializeField] private GameObject privateScreenUI;

    [SerializeField] private GameObject menuBasicUI;
    [SerializeField] private GameObject menuInventoryUI;
    [SerializeField] private GameObject menuTargetUI;
    [SerializeField] private GameObject tumbleweed;
      

    // Update these params as player clicks UI buttons 
    private ActionData action = new ActionData();


    #region public UI funcs

    // Does basic UI reset when turn has ended
    public void TurnChange(Character newPlayer)
    {
        choosingPlayer = newPlayer;

        tumbleweed.SetActive(false);
        actionScreenUI.SetActive(false);
        privateScreenUI.SetActive(true);

        ChangeOpenMenu(menuBasicUI);
        action.user = choosingPlayer;
    }

    public void Attack()
    {
        // Save actual attack event somewhere to wait for action phace
        //GameManager.Instance.MakeAction(choosingPlayer, )
    }

    public void ChangeAction(Action newAction)
    {
        action.action = newAction;
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
