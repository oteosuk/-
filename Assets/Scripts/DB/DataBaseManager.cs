
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Reflection;


public class DataBaseManager : MonoBehaviour
{
    #region SingleTon
    public static DataBaseManager Instance { get; private set; }
    private void MakeSingleTon()
    {
        if(Instance != null)
            DestroyImmediate(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        MakeSingleTon();
    }
    #endregion

    private readonly string DatabaseURL = "https://elemental-hero-ef7eb-default-rtdb.firebaseio.com/";
    private DatabaseReference dbReference { get; set; }
    private string UserId;

    private string DB_USER;
    private string DB_SCOREBOARD;
    private string DB_LEADERBOARD;
    private string DB_INVENTORY;

    public User _userInfo;
    public Score _scoreInfo;
    

    

    public LeaderBoard[] _leaderBoard = new LeaderBoard[3];
    public List<Character> chrtmpList = new List<Character>();
    // Start is called before the first frame update
    void Start()
    {
        UserId = SystemInfo.deviceUniqueIdentifier;
        
        DB_USER = "users";
        DB_SCOREBOARD = "scoreboard";
        DB_LEADERBOARD = "leaderboard";
        DB_INVENTORY = "inventory";

        _leaderBoard.Initialize();

        Debug.Log("USERINFO START");
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DatabaseURL);
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    
    public void SaveUserInfo(string userName, string userEmail, string userPw)
    {
        Debug.Log("SaveUserInfo");
        Debug.Log("SaveUserInfo : " + userName + " / " + userEmail + " / " + userPw);
        SaveNewUser(userName, userEmail, userPw);
    }

    private void SaveNewUser(string userName, string userEmail, string userPw)
    {
        User user = new User(userName, userEmail, userPw);
        Score score = new Score(userEmail);
        Character character = new Character(userEmail);

        Debug.Log("SaveNewUser : " + user.userName + " / " + user.userEmail + " / " + user.userPw);
       
    
        // class 변수를 json 형태로 변환
        string ujson = JsonUtility.ToJson(user);
        string sjson = JsonUtility.ToJson(score);
        string ijson = JsonUtility.ToJson(character);


        //string key = user.UserEmail;
        Debug.Log("SaveNewUser() - userEmail " + user.userEmail);
        

        dbReference.Child(DB_USER).Child(user.userEmail).SetRawJsonValueAsync(ujson).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("user 정보를 db에 입력하였습니다");
            }
            else
            {
                Debug.Log("user 정보를 db에 입력하지 못했습니다");
            }
            
        });

        dbReference.Child(DB_SCOREBOARD).Child(user.userEmail).SetRawJsonValueAsync(sjson).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("ScoreBoard 정보를 db에 입력하였습니다");
            }
            else
            {
                Debug.Log("ScoreBoard 정보를 db에 입력하지 못했습니다");
            }
        });

        dbReference.Child(DB_INVENTORY).Push().SetRawJsonValueAsync(ijson).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("DB_INVENTORY 정보를 db에 입력하였습니다");
            }
            else
            {
                Debug.Log("DB_INVENTORY 정보를 db에 입력하지 못했습니다");
            }
        });
    }

    public void PsetUpUserInfo(string userEmail)
    {
        Debug.Log("PsetUpUserInfo");
        SetUpUserInfo(userEmail);
    }

    public void PsetUpScoreBoardInfo(string userEmail)
    {
        Debug.Log("PsetUpScoreBoardInfo");
        setUpScoreBoardInfo(userEmail);
    }
    
    
    private void SetUpUserInfo(string userEmail)
    {
        Debug.Log("SetUpUserInfo");
        dbReference.Child(DB_USER).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("User Info Load Canceled");
            }
            else if (task.IsFaulted)
            {
                Debug.Log("User Info Load Faulted");
            }
            // Data Access Success
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

               
                Debug.Log(snapshot.ChildrenCount);
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)data.Value;
                 
                    if (userEmail == Convert.ToString(dictUser["userEmail"]))
                    {
                        string _userName = Convert.ToString(dictUser["userName"]);
                        string _userEmail = Convert.ToString(dictUser["userEmail"]);
                        string _userPw = Convert.ToString(dictUser["userPw"]);
                        float _bgmVolume = float.Parse((dictUser["bgmVolume"]).ToString());
                        float _sfxVolume = float.Parse((dictUser["sfxVolume"]).ToString());
                        bool _gameType = Convert.ToBoolean(dictUser["gameType"]);
                        int _money = int.Parse((dictUser["money"]).ToString());
                        int _chrCode = int.Parse((dictUser["chrCode"]).ToString());

                        _userInfo = new User(_userName, _userEmail, _userPw, _bgmVolume, _sfxVolume, _gameType, _money, _chrCode);
                       
                        Debug.Log(_userInfo.userName);
                        Debug.Log(_userInfo.userEmail);
                        Debug.Log(_userInfo.userPw);

                    } else
                    {
                        Debug.Log("SetUpUserInfo Error");
                    }
                }
            }
        });
    }

    private void setUpScoreBoardInfo(string userEmail)
    {
        Debug.Log("setUpScoreBoardInfo");
        dbReference.Child(DB_SCOREBOARD).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("User Info Load Canceled");
            }
            else if (task.IsFaulted)
            {
                Debug.Log("User Info Load Faulted");
            }
            // Data Access Success
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                Debug.Log(snapshot.ChildrenCount);
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary dicScore = (IDictionary)data.Value;
                    
                    if (userEmail == Convert.ToString(dicScore["userEmail"]))
                    {
               
                        string _userEmail = Convert.ToString(dicScore["userEmail"]);
                        int _accGamesPlayed = int.Parse((dicScore["accGamesPlayed"]).ToString());
                        int _accScorePlayed = int.Parse((dicScore["accScorePlayed"]).ToString());
                        int _accTimePlayed = int.Parse((dicScore["accTimePlayed"]).ToString());
                        int _accKillPlayed = int.Parse((dicScore["accKillPlayed"]).ToString());
                        float _accDistance = float.Parse((dicScore["accDistance"]).ToString());
                        int _LongestGame = int.Parse((dicScore["LongestGame"]).ToString());
                        int _BestScore = int.Parse((dicScore["BestScore"]).ToString());
                        int _BestScoreTime = int.Parse((dicScore["BestScoreTime"]).ToString());

                        _scoreInfo = new Score(_userEmail, _accGamesPlayed, _accScorePlayed, _accTimePlayed, _accKillPlayed, _accDistance, _LongestGame, _BestScore, _BestScoreTime);
                    }
                    else
                    {
                        Debug.Log("setUpScoreBoardInfo Error");
                    }
                }
            }
        });
    }
    private void setUpInventoryInfo(string userEmail)
    {
        Debug.Log("SetUpInventoryInfo");
        
        dbReference.Child(DB_INVENTORY).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("SetUpInventoryInfo Canceled");
            }
            else if (task.IsFaulted)
            {
                Debug.Log("SetUpInventoryInfo Faulted");
            }
            // Data Access Success
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                chrtmpList.Clear();
                Debug.Log(snapshot.ChildrenCount);
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary dictInven = (IDictionary)data.Value;

                    var dataEmail = Convert.ToString(dictInven["userEmail"]);
                    
                    if (_userInfo.userEmail.Equals(dataEmail))
                    {
                        string _chrName = Convert.ToString(dictInven["chrName"]);
                        int _chrCode = int.Parse((dictInven["chrCode"]).ToString());
                        int _money = int.Parse((dictInven["money"]).ToString());
                        string _userEmail = Convert.ToString(dictInven["userEmail"]);

                        Character chrtmp = new Character(_chrName, _chrCode, _money, _userEmail);

                        chrtmpList.Add(chrtmp);
                    }
                }
                Debug.Log($"characterCount: {chrtmpList.Count}");
            }
        });
    }


    public void setUpLeaderBoard()
    {
        Debug.Log("setUpLeaderBoard");
        
        dbReference.Child(DB_LEADERBOARD).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("setUpLeaderBoard Canceled");
            }
            else if (task.IsFaulted)
            {
                Debug.Log("setUpLeaderBoard Faulted");
            }
            // Data Access Success
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                chrtmpList.Clear();
                Debug.Log(snapshot.ChildrenCount);
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary dictInven = (IDictionary)data.Value;

                   
                }
            }
        });
    }

    public void HandleUpdateUserName(string username)
    {
        //User user = new User();
        //user.UserNameChange(username);
        //Dictionary<string, object> update = 
        /*
        Debug.Log(_userInfo.userEmail);
        dbReference.Child(DB_USER).Child(_userInfo.userEmail).Child("userName").SetValueAsync(username);
        Debug.Log("UserName change Complete -> " + username);
        */
        Dictionary<string, object> userNameUpdate = new Dictionary<string, object>();
        userNameUpdate["/" + DB_USER + "/" + _userInfo.userEmail + "/" + "userName"] = username;

        dbReference.UpdateChildrenAsync(userNameUpdate).ContinueWith(
            task =>
            {
                Debug.Log(string.Format("HandleUpdateUserName::IsCompleted:{0} IsCanceled:{1} IsFaulted:{2}", task.IsCompleted, task.IsCanceled, task.IsFaulted));
            }
        );
    }
    public void UpdateScoreBoard(Score gScore)
    {
        Debug.Log("UpdateScoreBoard");
        string key = gScore.userEmail;
        Dictionary<string, object> gScoreValues = gScore.ToDictionary();

        Dictionary<string, object> childValueUpdates = new Dictionary<string, object>();
        childValueUpdates["/" + DB_SCOREBOARD + "/" + key] = gScoreValues;
        //childValueUpdates["/" + DB_LEADERBOARD + "/" + key] = gScoreValues;

        dbReference.UpdateChildrenAsync(childValueUpdates);
    }

    public void UpdateInventory(Character chr)
    {
        Debug.Log("UpdateInventory");
        string key = dbReference.Child(DB_INVENTORY).Push().Key;

        Dictionary<string, object> characterValues = chr.ToDictionary();

        Dictionary<string, object> childValueUpdates = new Dictionary<string, object>();
        childValueUpdates["/" + DB_INVENTORY + "/" + key] = characterValues;
        //childValueUpdates["/" + DB_LEADERBOARD + "/" + key] = gScoreValues;

        dbReference.UpdateChildrenAsync(childValueUpdates);
    }
    public void AddScoreToLeaders(Score gScore, string userName, int EnemyKill)
    {
        dbReference.Child(DB_LEADERBOARD).RunTransaction(mutableData =>
        {
            List<object> leaders = mutableData.Value as List<object>;
            short scoreLine = 3; // 저장할 유저 점수 갯수
  
            if (leaders == null)
            {
                leaders = new List<object>();
            }
            else if (mutableData.ChildrenCount >= scoreLine)
            {
                long minScore = long.MaxValue;
                string recentUserEmail = gScore.userEmail; // already write UserBestScore on leaderboard
                object minVal = null;
                foreach (var child in leaders)
                {
                    if (!(child is Dictionary<string, object>)) continue;
                    long childScore = (long)
                                ((Dictionary<string, object>)child)["BestScore"];
                    string childEmail = (string)
                               ((Dictionary<string, object>)child)["userEmail"];



                    Debug.Log("recentUserEmail : " + recentUserEmail);
                    Debug.Log("childEmail : " + childEmail);
                    if (recentUserEmail == childEmail)
                    {
                        Debug.Log("동일한 id 발견");
                        if (childScore < minScore)
                        {
                            minScore = childScore;
                            minVal = child;
                        }
                    }
                    else
                    {
                        Debug.Log("동일한 id 미발견");
                        if (childScore < minScore)
                        {
                            minScore = childScore;
                            minVal = child;
                        }
                    }
                   
                }

                if (minScore > gScore.BestScore)
                {
                    // The new score is lower than the existing 3 scores, abort.
                    return TransactionResult.Abort();
                }

                // Remove the lowest score.
                leaders.Remove(minVal);
            }
            // BestScore, BestScoreTime, accKillPlayed
            // Add the new high score.
            Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
            newScoreMap["userEmail"] = gScore.userEmail;
            newScoreMap["userName"] = userName;
            newScoreMap["BestScore"] = gScore.BestScore;
            newScoreMap["BestScoreTime"] = gScore.BestScoreTime;
            newScoreMap["EnemyKill"] = EnemyKill;
            
            leaders.Add(newScoreMap);
            mutableData.Value = leaders;
            return TransactionResult.Success(mutableData);
        }).ContinueWith(
            task =>
            {
                Debug.Log(string.Format("AddScoreToLeaders::IsCompleted:{0} IsCanceled:{1} IsFaulted:{2}", task.IsCompleted, task.IsCanceled, task.IsFaulted));
            });
    }

    public void UpdateSFX(string userEmail, float volume)
    {
        dbReference.Child(DB_USER).Child(userEmail).Child("sfxVolume").SetValueAsync(volume);
    }

    public void UpdateBGM(string userEmail, float volume)
    {
        dbReference.Child(DB_USER).Child(userEmail).Child("bgmVolume").SetValueAsync(volume);
    }

    public void UpdateGameType(string userEmail, bool type)
    {
        dbReference.Child(DB_USER).Child(userEmail).Child("gameType").SetValueAsync(type);
    }
    public void UpdateMoney(string userEmail, int money)
    {
        dbReference.Child(DB_USER).Child(userEmail).Child("money").SetValueAsync(money);
    }

    public void UpdateUsersChrCode(string userEmail, int chrCode)
    {
        dbReference.Child(DB_USER).Child(userEmail).Child("chrCode").SetValueAsync(chrCode);
    }
    public string GetUserEmail()
    {
        return _userInfo.userEmail;
    }
    public IEnumerator GetUserName(Action<string> onCallback)
    {
        Debug.Log(Convert.ToString(_userInfo.userEmail));
        var userNameData = dbReference.Child(DB_USER).Child(_userInfo.userEmail).Child("userName").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            
            onCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator GetGold(Action<string> onCallback)
    {
        var userMoney = dbReference.Child(DB_USER).Child(_userInfo.userEmail).Child("money").GetValueAsync();

        yield return new WaitUntil(predicate: () => userMoney.IsCompleted);

        if (userMoney != null)
        {
            DataSnapshot snapshot = userMoney.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }


    public IEnumerator GetUserRankData(Action<LeaderBoard[]> user)
    {
        int rankNum = 0;
        string rankNumStr = rankNum.ToString();

        var leaders = dbReference.Child(DB_LEADERBOARD).OrderByChild("BestScore").LimitToLast(3).GetValueAsync();
        yield return new WaitUntil(predicate: () => leaders.IsCompleted);

        if (leaders != null)
        {
            DataSnapshot snapshot = leaders.Result;
            foreach (DataSnapshot data in snapshot.Children)
            {
                IDictionary Leaders = (IDictionary)data.Value;

                string _userEmail = Convert.ToString(Leaders["userEmail"]);
                string _userName = Convert.ToString(Leaders["userName"]);
                int _BestScore = int.Parse((Leaders["BestScore"]).ToString());
                int _BestScoreTime = int.Parse((Leaders["BestScoreTime"]).ToString());
                int _EnemyKill = int.Parse((Leaders["EnemyKill"]).ToString());

                LeaderBoard leadertmp = new LeaderBoard(_userEmail, _userName, _BestScore, _BestScoreTime, _EnemyKill);

                _leaderBoard[rankNum] = leadertmp;
                rankNum++;
            }
            user.Invoke(_leaderBoard);
        }
    }
    public IEnumerator GetInventoryData(Action<List<Character>> chr)
    {

        List<Character> chrList = new List<Character>();
        var character = dbReference.Child(DB_INVENTORY).GetValueAsync();
        yield return new WaitUntil(predicate: () => character.IsCompleted);

        if (character != null)
        {
            DataSnapshot snapshot = character.Result;
            foreach (DataSnapshot data in snapshot.Children)
            {
                IDictionary characters = (IDictionary)data.Value;

                if (_userInfo.userEmail == Convert.ToString(characters["userEmail"]))
                {
                    string _chrName = Convert.ToString(characters["chrName"]);
                    int _chrCode = int.Parse((characters["chrCode"]).ToString());
                    int _money = int.Parse((characters["money"]).ToString());
                    string _userEmail = Convert.ToString(characters["userEmail"]);

                    Character chrtmp = new Character(_chrName, _chrCode, _money, _userEmail);

                    chrList.Add(chrtmp);
                }

            }
            chr.Invoke(chrList);
        }
    }

    // MainScene User Profile OnClick -> Update User Data
    public User getUserData()
    {
        string userEmail = GetUserEmail();

        PsetUpUserInfo(userEmail);

        return _userInfo;
    }

    public Score getScoreData()
    {
        string userEmail = GetUserEmail();
        PsetUpScoreBoardInfo(userEmail);
      
        return _scoreInfo;
    }

    public List<Character> getCharacterList()
    {
        string userEmail = GetUserEmail();
        setUpInventoryInfo(userEmail);

        return chrtmpList;
    }

}
