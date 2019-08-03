using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    [SerializeField] private int ID = 0;
    [HideInInspector] public float HP = 100f;
    public PlayerData characterData;

    private SOCharacterProfile profile;
     public UnityEvent OnHpChange; //Fires on all hp- change events. 

    private void Awake() 
    {
        GameManager.Instance.AssignPlayer(ID, this);
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
        }
        else
        {
            Debug.Log("Missed target");
        }
    }

    public void Heal(SOHealthPotion potion, Character targetCharacter)
    {
        targetCharacter.GainHP(potion.HealAmount);
    }
}
