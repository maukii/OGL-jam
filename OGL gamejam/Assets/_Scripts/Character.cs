using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{

    public string PlayerName;     //Actual players name set in setup phase
    public bool IsTraitor = false; //Set in set up phase
    [HideInInspector] public float HP = 100f;
    public SOCharacterProfile CharacterProfile; //Character profile selected in setup phase

     public UnityEvent OnHpChange; //Fires on all hp- change events. 

    private void Start() 
    {
        HP = CharacterProfile.MaxHP;
    }
    public void GetDamage(float damage)
    {
        HP-=damage;
        OnHpChange.Invoke();
        if(HP<0 || Mathf.Approximately(HP,0))
        {
            Debug.Log("Implement death");
        }
    }

    public void GainHP(float amount)
    {
        HP = Mathf.Clamp(HP+amount,0,CharacterProfile.MaxHP);

        OnHpChange.Invoke();
    }

    public void Attack(SOWeapon weapon, GameObject target)
    {
        Character targetCharacter = target.GetComponent<Character>();
        if(target)
        {
            if(Random.Range(0,100)<weapon.HitChance)
            {
                targetCharacter.GetDamage(weapon.Damage);
            }
            else
            {
                Debug.Log("Missed target");
            }
        }
    }

    public void Heal(SOHealthPotion potion, GameObject target)
    {
        Character targetCharacter = target.GetComponent<Character>();
        if(target)
        {
            targetCharacter.GainHP(potion.HealAmount);
        }
    }
}
