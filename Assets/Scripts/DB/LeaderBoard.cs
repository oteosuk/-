using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard
{
    public string userEmail;
    public string userName;

    public int BestScore;
    public int BestScoreTime;
    public int EnemyKill;
   

    public LeaderBoard(string userEmail, string userName, int BestScore, int BestScoreTime, int EnemyKill)
    {
        this.userEmail = userEmail;
        this.userName = userName; 
        this.BestScore = BestScore;
        this.BestScoreTime = BestScoreTime;
        this.EnemyKill = EnemyKill;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["userEmail"] = userEmail;
        result["userName"] = userName;
        result["BestScore"] = BestScore;
        result["BestScoreTime"] = BestScoreTime;
        result["EnemyKill"] = EnemyKill;



        return result;
    }
}
