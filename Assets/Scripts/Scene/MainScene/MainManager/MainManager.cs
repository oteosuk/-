using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
// Developer - Shim eungi
public class MainManager : MonoBehaviour
{
    
    private static MainManager instance = null;
    
     // Scene
    //public GameObject MainScene;

    // Main Scene Menu Btn
    //public GameObject GameStartBtn; // 게임시작
    //public GameObject settingBtn; // 환경설정
    //public GameObject settingCancelBtn; // 환경설정 취소
    //public GameObject RankBtn; // 랭크버튼

    // MainScene
    public Canvas menuCanvas;
    public CanvasGroup menuCanvasGroup;

    public Text UserName;
    public Text GoldTxT;
    //public StartBtnEvent startBtnEvent;
    
    // settingScene 
    public Canvas settingCanvas;
    public CanvasGroup settingCanvasGroup;

    public Toggle ModeToggle;

    
    // UserProfileScene
    public Canvas userProfileCanvas;
    public CanvasGroup userProfileCanvasGroup;

    /*
    // gameExplainScene
    public Canvas explainCanvas;
    public CanvasGroup explainCanvasGroup;
    */

    public Text GamesPlayed;
    public Text TimePlayed;
    public Text LongestGame;
    public Text BestScore;
    public Text ScorePlayed;
    public InputField UserNameInputField;
    
    
    // Fadein, FadeOut in Config
    private float alpha100 = 1.0f;
    private float alpha000 = 0.0f;

    // User Data
    private User mUser;
    private Score mScore;

    public List<Sprite> _characterImage = new ();
    public Dictionary<int, Sprite> _characterDic = new();
    public Canvas _characterCanvas;
    public GameObject characterButton;
    public Image _mainCharacterImage;
    public static MainManager Instance
    {
        get {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance) {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        Time.timeScale = 1;

        mUser = DataBaseManager.Instance.getUserData();
        mScore = DataBaseManager.Instance.getScoreData();

        // DB 에 저장되어있는 UserName - userName Text print
        StartCoroutine(DataBaseManager.Instance.GetUserName((string nameText) =>
        {
            Debug.Log("UserName Set : " + nameText);
            UserName.text = nameText;

        }));

        // users - money data
        StartCoroutine(DataBaseManager.Instance.GetGold((string money) =>
        {
            Debug.Log("UserMoney Set : " + money);
            GoldTxT.text = money;

        }));

        ModeToggle.isOn = mUser.gameType;

        menuCanvasGroup.alpha = alpha100;
        menuCanvas.enabled = true;
        
        settingCanvasGroup.alpha = alpha000;
        settingCanvas.enabled = false;
        
        userProfileCanvasGroup.alpha = alpha000;
        userProfileCanvas.enabled = false;

        _characterDic.Clear();
        var key = 100;
        foreach(var image in _characterImage)
        {
            _characterDic.Add(key++, image);
        }
        DataBaseManager.Instance.getCharacterList();

        _characterCanvas.gameObject.SetActive(false);
    }
    public void Start(){
        if(!AudioManager.instance.CheckBGM("GameSceneBGM"))
            AudioManager.instance.PlayBGM("GameSceneBGM");
    }

    public void OnClickStartBtn(){
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickStartBtn Click");
        SceneLoader.Instance.LoadScene("GameScene");
    }
    
    public void OnClickShopBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickShopBtn Click");
        SceneLoader.Instance.LoadScene("ShopScene");
    }
    public void OnClickSettingBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickSettingBtn Click");
        Time.timeScale = 0; // 화면 정지

        // setting Canvas Access
        
        settingCanvasGroup.alpha = alpha100;
        settingCanvasGroup.interactable = true;
        settingCanvas.enabled = true;
        menuCanvas.enabled = false;
        
    }
    public void OnClickSettingCancelBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickSettingCancelBtn Click");
        

        // setting Canvas Access
        
        menuCanvasGroup.alpha = alpha100;
        menuCanvasGroup.interactable = true;
        menuCanvas.enabled = true;
        settingCanvas.enabled = false;
    }
    
    public void OnClickUserProfileBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickUserProfileBtn");
        
        Time.timeScale = 0; // 화면 정지

        // UserProfile Canvas Access
        

        userProfileCanvasGroup.alpha = alpha100;
        userProfileCanvasGroup.interactable = true;
        userProfileCanvas.enabled = true;
        menuCanvas.enabled = false;

        Debug.Log(mUser.userName);
        UserNameInputField.text = mUser.userName;

      
        GamesPlayed.text = Convert.ToString(mScore.accGamesPlayed);
        TimePlayed.text = Convert.ToString(mScore.accTimePlayed); // 시간 누적
        LongestGame.text = Convert.ToString(mScore.LongestGame); // 가장 오래 플레이한 시간
        BestScore.text = Convert.ToString(mScore.BestScore);
        ScorePlayed.text = Convert.ToString(mScore.accScorePlayed);

    }
    public void OnClickUserProfileCancelBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickUserProfileCancelBtn");
        
        Time.timeScale = 1; // 화면 재개
                            // setting Canvas Access
        menuCanvasGroup.alpha = alpha100;
        menuCanvasGroup.interactable = true;
        menuCanvas.enabled = true;
        userProfileCanvas.enabled = false;
    }
    
    public void OnClickRankBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickRankBtn Click");
        SceneLoader.Instance.LoadScene("RankScene");
    }

    private List<GameObject> characterList = new();
    public void OnClickCharacterSelect()
    {
        _characterCanvas.gameObject.SetActive(true);
        foreach (var data in characterList)
        {
            Destroy(data);
        }
        characterList.Clear();

        List<Character> invenCharData = DataBaseManager.Instance.getCharacterList();

        //test
        //invenCharData.Clear();

        //var data1 = new Character();
        //data1.chrCode = 100;
        //invenCharData.Add(data1);

        //var data2 = new Character();
        //data2.chrCode = 101;
        //invenCharData.Add(data2);

        //var data3 = new Character();
        //data3.chrCode = 102;
        //invenCharData.Add(data3);

        

        var moveX = 400;
        var startPos = new Vector2(400, 450);
        var i = 0;
        foreach(var charData in invenCharData)
        {
            var charImage = _characterDic[charData.chrCode];
            var button = Instantiate(characterButton, _characterCanvas.transform);
            button.GetComponent<RectTransform>().position = new Vector2(startPos.x + (i++ * moveX), startPos.y);
            button.GetComponent<Image>().sprite = charImage;
            button.GetComponent<CharacterSelectButton>().characterCode = charData.chrCode;
            characterList.Add(button);

        }
    }

    public void SetCharacter(int characterCode)
    {
        mUser.chrCode = characterCode;
        _mainCharacterImage.sprite = _characterDic[characterCode];
        _characterCanvas.gameObject.SetActive(false);

        DataBaseManager.Instance.UpdateUsersChrCode(mUser.userEmail, mUser.chrCode);

        mUser = DataBaseManager.Instance.getUserData();
    }

    public void OnClickModeToggle(Toggle mode)
    {
        if (mode.isOn)
        {
            Debug.Log("JoyStick " + mode.isOn);
        }
        else
        {
            Debug.Log("Gyro " + mode.isOn);
        }

        DataBaseManager.Instance.UpdateGameType(mUser.userEmail, mode.isOn);
    }
 
     /*
    public void OnClickExplainBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickExplainBtn Click");
        Time.timeScale = 0; // 화면 정지

        // setting Canvas Access
        
        explainCanvasGroup.alpha = alpha100;
        explainCanvasGroup.interactable = true;
        explainCanvas.enabled = true;
        menuCanvas.enabled = false;
        
    }
    public void OnClickExplainCancelBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickExplainCancelBtn Click");
        

        // setting Canvas Access
        
        menuCanvasGroup.alpha = alpha100;
        menuCanvasGroup.interactable = true;
        menuCanvas.enabled = true;
        explainCanvas.enabled = false;
    }
    */
    
}
