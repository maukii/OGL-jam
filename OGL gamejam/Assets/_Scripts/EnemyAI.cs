using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyAI : MonoBehaviour
{
    
    private Character AI;
    public AIStates CurrentAIState
    {
        get
        {
            return currentAIState;
        }
        private set
        {
            currentAIState = value;
            switch (value)
            {
                case AIStates.Defend:
                    HealSelf();
                    break;
                case AIStates.Smash:
                    Attack();
                    break;
                case AIStates.Tactics:
                    DecideTactics();
                    break;
                default:
                Debug.Log("AI- State error");
                break;
            }
            AIStateChange.Invoke();
        }
    }
    private AIStates currentAIState;
    [SerializeField] private float DefenceModeThreshold = 25f, SmashModeThreshold = 75f;
    [SerializeField] private int numberOfActions = 2;

    private GameManager manager;
    private float maxDamage, temporaryHp;

    public UnityEvent AIStateChange;
    public enum AIStates
    {
        Smash,
        Tactics,
        Defend
    }
    private void Awake() 
    {
        AI = GetComponent<Character>();    
        manager = GameManager.Instance;

        maxDamage = AI.CharacterProfile.mainWeapon.Damage > AI.CharacterProfile.secondaryWeapon.Damage?
        AI.CharacterProfile.mainWeapon.Damage:AI.CharacterProfile.secondaryWeapon.Damage;
    }
    [ContextMenu("Think")]
    public void Think()
    {
        temporaryHp = AI.HP;
        for(int i=0;i<numberOfActions;i++)
        {
            DecideAction();
        }
    }
    private void DecideAction()
    {
        if(temporaryHp>SmashModeThreshold)
        {
            CurrentAIState = AIStates.Smash;
        }
        else if (temporaryHp>DefenceModeThreshold)
        {
            CurrentAIState = AIStates.Tactics;
        }
        else
        {
            CurrentAIState = AIStates.Defend;
        }
       
    }

    private void Attack()
    {
        bool targetFound = false;
        Character target;
        SOWeapon selectedWeapon = Random.Range(0,2)==0?AI.CharacterProfile.mainWeapon:AI.CharacterProfile.secondaryWeapon;
        do
        {
            target = manager.Players[Random.Range(0,manager.Players.Length)];
            if(target.IsTraitor && target.HP<maxDamage)
            {
                continue;
            }
            else
            {
                targetFound = true;
            }
        } while(!targetFound);
       manager.MakeAction(AI,target,Action.Attack,selectedWeapon);
       Debug.Log("Attacked");
    }

    private void HealSelf()
    {
        temporaryHp += AI.CharacterProfile.potion.HealAmount;
        manager.MakeAction(AI,AI,Action.Heal,AI.CharacterProfile.potion);
        Debug.Log("Healing self");
    }

    //In-depth calculations for best possible action
    private void DecideTactics()
    {
        Debug.Log("Tactics:");
        if(Random.Range(0,2)==0)
        {
            HealSelf();
        }
        else
        {
            {
                Attack();
            }
        }    
    }

}
