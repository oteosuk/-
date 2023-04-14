using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    // Start is called before the first frame update
    float R = 25/255f;
    float G = 200/255f;
    float B = 25/255f;
    
    string state = "INIT";
    
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(R, G, B, 200/255f);
    }

    // Update is called once per frame
    void Update() // 백그라운드 색깔 효과 기능
    {
        float time = Time.deltaTime * 2f;
        if(state == "INIT" && R <= 200/255f){
            GetComponent<SpriteRenderer>().color = new Color(R, G, B, 200/255f);
            R += 5/255f * time;
            if(R >= 200/255f) state = "REDDOWN";
        }
        else if(state == "REDDOWN" && R >= 25/255f){
            GetComponent<SpriteRenderer>().color = new Color(R, G, B, 200/255f);
            R -= 5/255f * time;
            if(R <= 25/255f) state = "BLUEUP";
        }
        else if(state == "BLUEUP" && B <= 200/255f){
            GetComponent<SpriteRenderer>().color = new Color(R, G, B, 200/255f);
            B += 5/255f * time;
            if(B >= 200/255f) state = "BLUEDOWN";
        }
        else if(state == "BLUEDOWN" && B >= 25/255f){
            GetComponent<SpriteRenderer>().color = new Color(R, G, B, 200/255f);
            B -= 5/255f * time;
            if(B <= 25/255f) state = "INIT";
        }
    }
}
