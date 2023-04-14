using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    public int characterCode;

    private void Awake()
    {
        
    }

    public void OnClick()
    {
        MainManager.Instance.SetCharacter(characterCode);
    }
}
