using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdatePlayerPortrait : MonoBehaviour
{
    private Image portrait;
    [SerializeField] private Character character;

    private void Awake() 
    {
        portrait = GetComponent<Image>();
        if(!portrait)
        {
            Debug.LogError("Image- component not found in  " + this.name);
            this.gameObject.SetActive(false);
        }    
    }
    public void SetImage()
    {
        portrait.sprite = character.characterData.character.Icon;
    }
}
