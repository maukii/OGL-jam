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

    public SOCharacterProfile profile;
    private AudioSource sfxAudiosource;
     public UnityEvent OnHpChange; //Fires on all hp- change events. 
     public UnityEvent OnCharacterChange;
    [SerializeField] private Transform effectLocalPosition;
    private void Awake() 
    {
        if(!IsAI)
        {
            GameManager.Instance.AssignPlayer(ID, this);
        }
        profile = characterData.character;
        OnCharacterChange.Invoke();
        sfxAudiosource = GameManager.Instance.sfxAudioSource;

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
                Instantiate(weapon.GoreEffect,targetCharacter.effectLocalPosition);
                sfxAudiosource.PlayOneShot(weapon.Sound);

            }
        }
        else
        {
            if(weapon.MissEffect)
            {
                Instantiate(weapon.MissEffect,targetCharacter.effectLocalPosition);
                sfxAudiosource.PlayOneShot(weapon.missAudio);
            }

        }
    }

    public void Heal(SOHealthPotion potion, Character targetCharacter)
    {
        targetCharacter.GainHP(potion.HealAmount);
        if(potion.HealingEffect)
        {
            Instantiate(potion.HealingEffect,targetCharacter.effectLocalPosition);
            sfxAudiosource.PlayOneShot(potion.Sound);
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

    [ContextMenu("ShowDamageEffect")]
    public void ShowGoreEffect()
    {
        Instantiate(profile.mainWeapon.GoreEffect,effectLocalPosition);
    }
    [ContextMenu("ShowHealingEffect")]
    public void ShowHealingEffect()
    {
        Instantiate(profile.potion.HealingEffect,effectLocalPosition);
    }

    [ContextMenu("ShowMissEffect")]
    public void ShowMissEffect()
    {
        Instantiate(profile.mainWeapon.MissEffect,effectLocalPosition);
    }

}
