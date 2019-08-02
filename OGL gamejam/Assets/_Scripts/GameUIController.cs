using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    [SerializeField] private Text choosingPlayerText;
    [SerializeField] private GameObject[] characterChoices = new GameObject[4];

    public void ChooseCharacter(GameObject character)
    {
        // Remove this character from choosable characteres
    }

}
