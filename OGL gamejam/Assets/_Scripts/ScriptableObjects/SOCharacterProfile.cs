using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Template for character profile. 
//Actual profile asset is created in Unity editor with Create/GameAssets/Character Profile
[CreateAssetMenu(fileName = "CharacterProfile", menuName = "GameAssets/Character Profile", order = 0)]
public class SOCharacterProfile : SOBaseData
{
    public float MaxHP=100f;
    [Space(10)] public SOWeapon mainWeapon;
    public SOWeapon secondaryWeapon;
    [Space(10)] public SOHealthPotion potion;
}