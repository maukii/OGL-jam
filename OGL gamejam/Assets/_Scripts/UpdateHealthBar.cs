using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateHealthBar : MonoBehaviour
{
    private Image healthBar;
    [SerializeField] Character character;
    // Start is called before the first frame update
    private void Awake() 
    {
        healthBar = GetComponent<Image>();
        if(!healthBar)
        {
            Debug.LogError("Fill bar not found in  " + this.name);
            this.gameObject.SetActive(false);
        }
    }
    public void SetHealt()
    {
        if(!Mathf.Approximately(character.HP,0))
        {
            healthBar.fillAmount = character.HP/100;
        }
        else
        {
            healthBar.fillAmount = 0;
        }

    }
}
