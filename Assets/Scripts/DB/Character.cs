using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string chrName;
    public int chrCode;
    public int money;
    public string userEmail;

    public Character(){ }
    public Character(string userEmail)
    {
        chrName = "노멀 히어로";
        chrCode = 100;
        money = 0;
        this.userEmail = userEmail;
    }
    public Character(string chrName, int chrCode, int money, string userEmail)
    {
        this.chrName = chrName;
        this.chrCode = chrCode;
        this.money = money;
        this.userEmail = userEmail; 
    }

    public string getChrName(int chrCode)
    {
        string chName;
        if(chrCode == 101)
        {
            chName = "물물이 히어로";
        }
        else if (chrCode == 102)
        {
            chName = "풀푸르르 히어로";
        }
        else
        {
            chName = "";
     
        }

        return chName;
    }

    public int getChrMoney(int chrCode)
    {
        int money;
        if (chrCode == 101)
        {
            money = 5000;
        }
        else if (chrCode == 102)
        {
            money = 5000;
        }
        else
        {
            money = 0;

        }

        return money;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["chrName"] = chrName;
        result["chrCode"] = chrCode;
        result["money"] = money;
        result["userEmail"] = userEmail;

        return result;
    }

   
}
