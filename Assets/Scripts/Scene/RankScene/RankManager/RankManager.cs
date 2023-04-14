using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RankManager : MonoBehaviour
{
    private static RankManager instance = null;



    // RankScene menu
    public Canvas menuCanvas;
    public CanvasGroup menuCanvasGroup;

    #region myRankCanvas UI & Text
    // myRank
    public Canvas myRankCanvas;
    public CanvasGroup myRankCanvasGroup;

    // myRank Text
    public Text Plays;
    public Text TotalScore;

    public Text PlayTime;
    public Text KillEnemies;

    public Text Distance;
    public Text LongestTime;

    public Text BestScore;
    public Text BestScoreTime;

    #endregion

    #region userRankCanvas UI & Text
    // userRank
    public Canvas userRankCanvas;
    public CanvasGroup userRankCanvasGroup;

    public Text rank1_userName;
    public Text rank1_bestScore;

    public Text rank2_userName;
    public Text rank2_bestScore;

    public Text rank3_userName;
    public Text rank3_bestScore;

    public Text myRank_userName;
    public Text myRank_bestScore;

    #endregion

    private User rUser;
    private Score rScore;
    private LeaderBoard rLeaderBoard;

    // Fadein, FadeOut in Config
    private float alpha100 = 1.0f;
    private float alpha000 = 0.0f;

    public static RankManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        {
            if(null == instance)
            {
                instance = this;
            } else
            {
                Destroy(this.gameObject);
            }
        }
        rUser = DataBaseManager.Instance.getUserData();
        rScore = DataBaseManager.Instance.getScoreData();

        DataBaseManager.Instance.setUpLeaderBoard();

    
        menuCanvasGroup.alpha = alpha100;
        menuCanvas.enabled = true;

        myRankCanvasGroup.alpha = alpha000;
        myRankCanvas.enabled = false;

        userRankCanvasGroup.alpha = alpha000;
        userRankCanvas.enabled = false;

    }



    private void Start()
    {
        
    }

    public void OnClickUserRankLoadBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        StartCoroutine(DataBaseManager.Instance.GetUserRankData((LeaderBoard[] leaders) =>
        {
            string[] userEmail_format = new string[leaders.Length];

            for (int i = leaders.Length - 1; i >=0; i--)
            {

                if (leaders[i].userEmail.Equals(""))
                {
                    leaders[i].userName = "";
                    leaders[i].BestScore = 0;
                    //userEmail_format[i] = "no user";
                }
                
                
                leaders[i].userEmail = leaders[i].userEmail.Substring(0, 3);
                userEmail_format[i] = leaders[i].userName + "#" + leaders[i].userEmail + "**";
                
                

                Debug.Log("userName Set : " + leaders[i].userName);
                Debug.Log("BestScore Set : " + leaders[i].BestScore);
            }
             
            rank1_userName.text = userEmail_format[2];
            rank1_bestScore.text = Convert.ToString(leaders[2].BestScore);

            rank2_userName.text = userEmail_format[1];
            rank2_bestScore.text = Convert.ToString(leaders[1].BestScore);

            rank3_userName.text = userEmail_format[0];
            rank3_bestScore.text = Convert.ToString(leaders[0].BestScore);
        }));

        myRank_userName.text = rUser.userName;
        myRank_bestScore.text = Convert.ToString(rScore.BestScore);


        userRankCanvasGroup.alpha = alpha100;
        userRankCanvasGroup.interactable = true;
        userRankCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    public void OnClickMyRankLoadBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Plays.text = Convert.ToString(rScore.accGamesPlayed);
        Debug.Log(Plays.text);
        TotalScore.text = Convert.ToString(rScore.accScorePlayed);

        int second = int.Parse((rScore.accTimePlayed).ToString());
        int minute = second / 60;
        second = second % 60;
        int hour = minute / 60; 
        minute = minute % 60;
        PlayTime.text = Convert.ToString(hour) + "H " + Convert.ToString(minute) + "M " + Convert.ToString(second) + "S";


        KillEnemies.text = Convert.ToString(rScore.accKillPlayed);
        Distance.text = Convert.ToString(rScore.accDistance);


        second = int.Parse((rScore.LongestGame).ToString());
        minute = second / 60;
        second = second % 60;
        hour = minute / 60;
        minute = minute % 60;
        LongestTime.text = Convert.ToString(hour) + "H " + Convert.ToString(minute) + "M " + Convert.ToString(second) + "S";

        BestScore.text = Convert.ToString(rScore.BestScore);

        second = int.Parse((rScore.BestScoreTime).ToString());
        minute = second / 60;
        second = second % 60;
        hour = minute / 60;
        minute = minute % 60;
        BestScoreTime.text = Convert.ToString(hour) + "H " + Convert.ToString(minute) + "M " + Convert.ToString(second) + "S";


        myRankCanvasGroup.alpha = alpha100;
        myRankCanvasGroup.interactable = true;
        myRankCanvas.enabled = true;
        menuCanvas.enabled = false;
    }
    public void OnClickMyRankCancelBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        SceneLoader.Instance.LoadScene("RankScene");
    }

    public void OnClickUserRankCancelBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        SceneLoader.Instance.LoadScene("RankScene");
    }

    public void OnClickRankSceneCancelBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        SceneLoader.Instance.LoadScene("MainScene");
    }
}
