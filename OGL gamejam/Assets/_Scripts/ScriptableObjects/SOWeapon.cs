using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Template for a weapon. 
//Actual weapon asset is created in Unity editor with Create/GameAssets/Weapon
[CreateAssetMenu(fileName = "Weapon.asset", menuName = "GameAssets/Weapon", order = 0)]
public class SOWeapon : SOBaseData {
    
   [Space(10)]  
   public float Damage;
   public float HitChance;

   public GameObject GoreEffect;
   public GameObject MissEffect;
   public AudioClip missAudio;


}
