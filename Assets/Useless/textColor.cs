using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textColor : MonoBehaviour
{
    // Start is called before the first frame update
    float R = 255/255f;
    float G = 0/255f;
    float B = 0/255f;
    
    public Text mainTitle;

    string state = "INIT";

    void Start()
    {
        mainTitle.color = new Color(R, G, B, 255/255f);
    }

    // Update is called once per frame
    void Update() // 백그라운드 색깔 효과 기능
    {
        float time = Time.deltaTime * 40f; // 색깔의 변하는 속도

        if(state == "INIT" && B <= 255/255f){
            mainTitle.color = new Color(R, G, B, 255/255f);
            B += 5/255f * time;
            if(B >= 240/255f) state = "2";
        }
        else if(state == "2" && R >= 0/255f){
            mainTitle.color = new Color(R, G, B, 255/255f);
            R -= 5/255f * time;
            if(R <= 15/255f) state = "3";
        }
        else if(state == "3" && G <= 255/255f){
            mainTitle.color = new Color(R, G, B, 255/255f);
            G += 5/255f * time;
            if(G >= 240/255f) state = "4";
        }
        else if(state == "4" && B >= 0/255f){
            mainTitle.color = new Color(R, G, B, 255/255f);
            B -= 5/255f * time;
            if(B <= 15/255f) state = "5";
        }
        else if(state == "5" && R <= 255/255f){
            mainTitle.color = new Color(R, G, B, 255/255f);
            R += 5/255f * time;
            if(R >= 240/255f) state = "6";
        }
        else if(state == "6" && G >= 0/255f){
            mainTitle.color = new Color(R, G, B, 255/255f);
            G -= 5/255f * time;
            if(G <= 15/255f) state = "INIT";
        }
    }
}
