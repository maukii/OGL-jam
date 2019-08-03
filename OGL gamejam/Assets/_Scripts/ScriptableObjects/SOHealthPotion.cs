using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Template for health potion profile. 
//Actual potion asset is created in Unity editor with Create/GameAssets/HealthPotion
[CreateAssetMenu(fileName = "HealthPotion.asset", menuName = "GameAssets/Health Potion", order = 0)]
public class SOHealthPotion : SOBaseData 
{
    [Space(10)]
    public float HealAmount;
    public GameObject HealingEffect;

}
