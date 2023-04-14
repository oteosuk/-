using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBtnEvent : MonoBehaviour
{
   // 버튼이 눌렸을 때
   [SerializeField]
   private GameObject GameStartBtn;

   public void OnclickBtnChangeImage(){
      GameStartBtn.GetComponent<Image>().color = Color.blue;
   }
   
   
}
