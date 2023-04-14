using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    Image background;
    public Sprite nonSelected_Img;
    public Sprite selected_Img;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void Selected()
    {
        background.sprite = selected_Img;
    }
    public void nonSelected()
    {
        background.sprite = nonSelected_Img;
    }
}
