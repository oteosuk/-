using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Developer : Shim eungi
public class UIManager : MonoBehaviour
{
    // 일시정지 기능 ( 화면 불투명도 50 % ), Fade 기능

    // inspector Access - SerializeField
    [SerializeField]
    private Image blackOutImage;
    [SerializeField]
    private float blackOutOpactiyChangeStep;
    private float blackOutTargetOpacity;

    void Start(){
      blackOutTargetOpacity = 0.0f;
    }

    void FixedUpdate() {
      BlackOut();
    }

    private void BlackOut() {
      float currentOpacity = blackOutImage.color.a;
      Debug.Log("Black out func()");
      // 현재 투명도가 목표 투명도보다 작을 때
      if ( currentOpacity < blackOutTargetOpacity ){
        blackOutImage.color = new Color(blackOutImage.color.r, blackOutImage.color.g, blackOutImage.color.b, blackOutImage.color.a + blackOutOpactiyChangeStep);

        if ( blackOutImage.color.a > blackOutTargetOpacity ){
          blackOutImage.color = new Color(blackOutImage.color.r, blackOutImage.color.g, blackOutImage.color.b, blackOutTargetOpacity);
        }
      }

      // 현재 투명도가 목표 투명도보다 클 때
      if ( currentOpacity > blackOutTargetOpacity ){
        blackOutImage.color = new Color(blackOutImage.color.r, blackOutImage.color.g, blackOutImage.color.b, blackOutImage.color.a - blackOutOpactiyChangeStep);

        if ( blackOutImage.color.a < blackOutTargetOpacity ){
          blackOutImage.color = new Color(blackOutImage.color.r, blackOutImage.color.g, blackOutImage.color.b, blackOutTargetOpacity);
        }
      }
    }

    public void SetBlackOutOpacity(float op){
      blackOutTargetOpacity = op;
      Debug.Log(blackOutTargetOpacity);
    }
}