using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPanel : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public List<GameObject> contentsPanels;
    private int selected = 0;
    private void Start()
    {
        OnClickTab(selected);
    }

   

    public void OnClickTab(int id)
    {
        for(int i=0; i< contentsPanels.Count; i++)
        {
            if (i == id)
            {
                contentsPanels[i].SetActive(true);
                tabButtons[i].Selected();
            }
            else
            {
                contentsPanels[i].SetActive(false);
                tabButtons[i].nonSelected();
            }
        }
        
    }
}
