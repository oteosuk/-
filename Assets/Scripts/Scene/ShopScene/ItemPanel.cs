using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPanel : MonoBehaviour
{
    public List<GameObject> previews;
    public List<GameObject> shopItemPanels;

    private int selected = 0;
    private void Start()
    {
        OnClickShopItem(selected);
        
    }
    public void OnClickShopItem(int id)
    {
        for(int i=0; i<shopItemPanels.Count; i++)
        {
            if( i == id)
            {
                //shopItemPanels[i].SetActive(true);
                previews[i].SetActive(true);
            }
            else
            {
                //shopItemPanels[i].SetActive(false);
                previews[i].SetActive(false);
            }
        }
    }
}
