using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Template for character profile. 
//Actual profile asset is created in Unity editor with Create/GameAssets/Character Profile
[CreateAssetMenu(fileName = "CharacterProfile", menuName = "GameAssets/Character Profile", order = 0)]
public class SOCharacterProfile : SOBaseData
{
    public float MaxHP=100f;
    [SerializeField, Space(10)] private SOWeapon mainWeapon;
    [SerializeField] private SOWeapon secondaryWeapon;
    [SerializeField, Space(10)] private SOHealthPotion potion;
}