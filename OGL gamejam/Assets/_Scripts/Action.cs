using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Idle,
    Attack,
    Heal,
    // add more
};

[System.Serializable]
public struct ActionData 
{

    public Character user;
    public Character target;
    public Action action;
    public SOBaseData item;

    public ActionData(Character user, Character target, Action action, SOBaseData item)
    {
        this.user = user;
        this.target = target;
        this.action = action;
        this.item = item;
    }

    public void ExecuteAction()
    {
        switch (action)
        {
            case Action.Idle:
                Debug.Log(user + " idling...");
                break;
            case Action.Attack:
                user.Attack((SOWeapon)item, target);
                break;
            case Action.Heal:
                user.Heal((SOHealthPotion)item, target);
                break;
            default:
                Debug.LogError("Execute action error");
                break;
        }
    }

}
