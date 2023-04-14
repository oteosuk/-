using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UserNameCheck : MonoBehaviour
{
   public InputField userNameInputField;
   public Text UserName;

  
   private void Awake()
    {
        


    }

   public void OnClickUserNameUpdateBtn()
   {
      string usern = userNameInputField.text;
      UserName.text = usern;
      DataBaseManager.Instance.HandleUpdateUserName(usern);
      
   }
}
