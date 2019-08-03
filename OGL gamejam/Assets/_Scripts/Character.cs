using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    public int ID = 0;
    [HideInInspector] public float HP = 100f;
    public PlayerData characterData;

    public bool IsAI = false;

    private SOCharacterProfile profile;
     public UnityEvent OnHpChange; //Fires on all hp- change events. 

    private void Awake() 
    {
        if(!IsAI)
        {
            GameManager.Instance.AssignPlayer(ID, this);
        }
        profile = characterData.character;

    }
    private void Start() 
    {
        HP = profile.MaxHP;
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
        HP = Mathf.Clamp(HP+amount,0,profile.MaxHP);

        OnHpChange.Invoke();
    }

    public void Attack(SOWeapon weapon, Character targetCharacter)
    {

        if(Random.Range(0,100)<weapon.HitChance)
        {
            targetCharacter.GetDamage(weapon.Damage);
            if(weapon.GoreEffect)
            {
                Instantiate(weapon.GoreEffect,targetCharacter.transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("Missed target");
            if(weapon.MissEffect)
            {
                Instantiate(weapon.MissEffect,targetCharacter.transform.position, Quaternion.identity);
            }

        }
    }

    public void Heal(SOHealthPotion potion, Character targetCharacter)
    {
        targetCharacter.GainHP(potion.HealAmount);
        if(potion.HealingEffect)
        {
            Instantiate(potion.HealingEffect,targetCharacter.transform.position, Quaternion.identity);
        }
    }

        [ContextMenu("SelfHeal")]
    public void SelfHeal()
    {
        Heal(profile.potion, this);
    }

    [ContextMenu("AttackSelf")]
    public void SelfAttack()
    {
        Attack(profile.mainWeapon,this);
    }
}
