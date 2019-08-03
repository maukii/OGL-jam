using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Baseclass for scriptable objects needing a name, descritption etc.
public class SOBaseData : ScriptableObject 
{
    public string Name;
    [TextArea] public string Description;
    [Space(10)]
    public Sprite Icon;
    public AudioClip Sound;    
}