using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{

    // User Info
    public string userEmail; // email
    public string userPw; // p.w
    public string userName; // username

    public bool gameType; // gameType true - joystick // false - gyro
    // Sound
    public float bgmVolume; // bgm
    public float sfxVolume; // sfx
    
    // user money
    public int money;

    // user character code
    public int chrCode;

    public User()
    {
        
    }
   
    // User Account Create
    public User(string userName, string userEmail, string userPw)
        :  this(userName, userEmail, userPw, -20.0f, -20.0f, true, 30000, 100)
    {
        Debug.Log("Create Account");
        this.userName = userName; 
        this.userEmail = userEmail;
        this.userPw = userPw;

    }
    
    // User Login 
    public User(string userName, string userEmail, string userPw, float bgmVolume, float sfxVolume, bool gameType, int money, int chrCode)
    {
        Debug.Log("User Account Login");
        this.userName = userName; 
        this.userEmail = userEmail;
        this.userPw = userPw;

        this.bgmVolume = bgmVolume;
        this.sfxVolume = sfxVolume;
        this.gameType = gameType;
        this.money = money;
        this.chrCode = chrCode;
    }

    public void UserNameChange(string newUserName)
    {
        this.userName = newUserName;
    }


    public Dictionary<string, object> toDictionary()
    {
        Dictionary<string, object> accountInfo = new Dictionary<string, object>();
        
        accountInfo["gameType"] = gameType;
        accountInfo["bgVolume"] = bgmVolume;
        accountInfo["effectSoundVolume"] = sfxVolume;
        
        accountInfo["userEmail"] = userEmail;
        accountInfo["userPw"] = userPw;
        accountInfo["userName"] = userName;

        return accountInfo;
    }
}
