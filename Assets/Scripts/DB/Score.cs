using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{

    public string userEmail; // email
    // acculate
    public int accGamesPlayed;
    public int accScorePlayed;
    public int accTimePlayed;
    public int accKillPlayed;
    public float accDistance;


    //Best
    public int LongestGame;
    public int BestScore;
    public int BestScoreTime;

    // Create Account
    public Score(string userEmail)
    {
        this.userEmail = userEmail;
        this.accGamesPlayed = 0;
        this.accScorePlayed = 0;
        this.accTimePlayed = 0;
        this.accKillPlayed = 0;
        this.accDistance = 0;

        
        this.LongestGame = 0;
        this.BestScore = 0;
        this.BestScoreTime = 0;
        
     }

    //ote
    public Score(string userEmail, int accGamesPlayed, int accScorePlayed, int accTimePlayed, int accKillPlayed, float accDistance, int LongestGame, int BestScore, int BestScoreTime)
    {
        this.userEmail = userEmail;
        this.accGamesPlayed = accGamesPlayed;
        this.accScorePlayed = accScorePlayed;
        this.accTimePlayed = accTimePlayed;
        this.accKillPlayed = accKillPlayed;
        this.accDistance = accDistance;


        this.LongestGame = LongestGame;
        this.BestScore = BestScore;
        this.BestScoreTime = BestScoreTime;
    }



    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["userEmail"] = userEmail;
        result["accGamesPlayed"] = accGamesPlayed;
        result["accScorePlayed"] = accScorePlayed;
        result["accTimePlayed"] = accTimePlayed;
        result["accKillPlayed"] = accKillPlayed;
        result["accDistance"] = accDistance;

        result["LongestGame"] = LongestGame;
        result["BestScore"] = BestScore;
        result["BestScoreTime"] = BestScoreTime;



        return result;
    }





}
