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
    private SOCharacterProfile profile;
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
        profile = AI.characterData.character;
        manager = GameManager.Instance;


    }

    private void Start() 
    {
        maxDamage = profile.mainWeapon.Damage > profile.secondaryWeapon.Damage?
        profile.mainWeapon.Damage:profile.secondaryWeapon.Damage;
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

    //Attack
    private void Attack()
    {
        bool targetFound = false;
        Character target;
        SOWeapon selectedWeapon = Random.Range(0,2)==0?profile.mainWeapon:profile.secondaryWeapon;
        int i=0;
        do
        {
            
            target = manager.Players[Random.Range(0,manager.Players.Count)];
            if((target.characterData.isTrator && target.HP<=maxDamage)||target.isDead)
            {
                continue;
            }
            else
            {
                targetFound = true;
            }
            i++;
            if(i>10)
            {
                continue;
            }
        } while(!targetFound);
       if(targetFound) 
        manager.MakeAction(AI,target,Action.Attack,selectedWeapon);
    }

    
    private void HealSelf()
    {
        temporaryHp += profile.potion.HealAmount;
        manager.MakeAction(AI,AI,Action.Heal,profile.potion);
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
