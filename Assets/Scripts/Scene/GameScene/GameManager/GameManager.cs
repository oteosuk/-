using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

/* 
 개발할 내용

  - 충돌처리를 적 위치가 가까이 왔을 때만 충돌체크 메소드를 ON 시키고 나머지는 OFF
  - 패턴 방식
  - Fade In, Out 방식 ( 구현완료 )

  - 카카오 API 구글 API 로그인 계정
*/
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    
    //public static GameManager instance;
    public EnemyPrefabManager enemyPrefabManager;
    
    public GameObject scoreScene;
    public Text scoreText;
    public Text timeText;
    public Text killText;

        //========================================================
    public int enemyscore; // enemy 하나당 점수
    public int score; // 그 판의점수
    public float distance; // 그 판의 움직인 거리
    public int playtime; // 그 판의 플레이한 시간
    public int killcount; // 그 한판의 죽인 enemy 수
    public int eatItem; // 그 판의 먹은 아이템 수

    //ote
    public int money; // 그 판의 번 돈

    //========================================================
    // 필요할지 모르지만 우선 만들어둔 변수들
    public int playcount = 0; // 총 플레이한 횟수
    public int bestscore = 0; // 모든 판 중의 최고 점수
    public float  accDistance = 0; // 총 판의 거리
    public int allplaytime = 0; // 총 플레이 시간
    public int allkillcount = 0; // 총 죽인 enemy 수
    public int alleatItem = 0; // 총 먹은 아이템 수
    //========================================================

    // Result Canvas - Shimeungi
    public CanvasGroup ResultCanvasGroup;
    public Canvas ResultCanvas;

    // Play Canvas
    public CanvasGroup PlayCanvasGroup;
    public Canvas PlayCanvas;

    // Result Text
    public Text PlayTime;
    public Text Scores;
    public Text Distance;
    public Text EnemiesKilled;

    // Object
    public GameObject Joystick;

    
    public GameObject player;

    
    // BGM
    //private bool musicStart = false;

    // Fadein, FadeOut in Config
    private float alpha100 = 1.0f;
    private float alpha000 = 0.0f;

    private User gUser;
    private Score gScore;

    public ItemSpawner[] _itemSpawner;
    public ItemSpawner itemSpawner;
    //public GameObject[] _characterPrefab;
    public List<GameObject> _characterPrefab = new ();
    //public Dictionary<int, GameObject> _characterDic = new();

    //public ItemSpawner[] _itemSqawnerPrefab;
    //public List<GameObject> _itemSpawnerPrefab = new ();
    //public Dictionary<int, GameObject> _itemSpawnerDic = new();

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static GameManager Instance
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


        gUser = DataBaseManager.Instance.getUserData();
        gScore = DataBaseManager.Instance.getScoreData();
        
        if(gUser.chrCode == 100){
            Instantiate(_characterPrefab[0], new Vector3(800f, 450f, 0f), Quaternion.identity);
            Instantiate(_itemSpawner[0], new Vector3(800f,450f,0), Quaternion.identity);
            itemSpawner = _itemSpawner[0];
            player = _characterPrefab[0];
        }
        else if(gUser.chrCode == 101){
            Instantiate(_characterPrefab[1], new Vector3(800f, 450f, 0f), Quaternion.identity);
            Instantiate(_itemSpawner[1], new Vector3(800f,450f,0), Quaternion.identity);
            itemSpawner = _itemSpawner[1];
            player = _characterPrefab[1];
        }
        else if(gUser.chrCode == 102){
            Instantiate(_characterPrefab[2], new Vector3(800f, 450f, 0f), Quaternion.identity);
            Instantiate(_itemSpawner[2], new Vector3(800f,450f,0), Quaternion.identity);
            itemSpawner = _itemSpawner[2];
            player = _characterPrefab[2];
        }
        
    }

    // OTE - 점수함수
    public void AddScore(int num) // 스코어에 num을 더해준다.
    {
        //Debug.Log("AddScore");
        score += num;
    }


    void Start()
    {
        
        //player = Instantiate(charPrefabs[(int)CharacterManager.instance.currentCharacter]);

        playcount++;
        enemyscore = 10;
        score = 0; // 점수
        distance = 0; // 움직인 거리
        playtime = 0; // 플레이한 시간
        killcount = 0; // 그 한판의 죽인 enemy 수

        ResultCanvasGroup.alpha = alpha000;
        ResultCanvas.enabled = false;

        PlayCanvasGroup.alpha = alpha100;
        PlayCanvas.enabled = true;

        scoreScene.SetActive(true);

        // gameType(true) : joystick, gameType(false) : gyro
        if (gUser.gameType)
        {
            Joystick.SetActive(true); // joyStick 활성화 -> EmtyCreateScene 으로 게임 Scene 을 하나로 묶어서 나중에 처리해줘야함
            Joystick.GetComponent<bl_Joystick>().enabled = true;
        } else
        {
            Joystick.SetActive(false); // joyStick 활성화 -> EmtyCreateScene 으로 게임 Scene 을 하나로 묶어서 나중에 처리해줘야함
            Joystick.GetComponent<bl_Joystick>().enabled = false;
        }

        
        player.SetActive(true);
        player.GetComponent<MovePlayer>().enabled = true;

        enemyPrefabManager.SpawnEnemyTimer();

        //=========================================
        itemSpawner.SpawnItemTimer();
        
        StartCoroutine("ScoreTimer");
        StartCoroutine("Second1Timer");
        StartCoroutine("Second10Timer");
        
    }

    IEnumerator ScoreTimer() // 0.25초마다 실행되는 점수타이머, 0.25초마다 점수가 1점씩오른다.
    {
        yield return new WaitForSeconds(0.25f);
        score++;
        StartCoroutine("ScoreTimer");
    }

    IEnumerator Second1Timer(){ // 1초마다 실행되는 타이머, 1초마다 playtime이 1씩 증가한다.
        yield return new WaitForSeconds(1f);
        playtime++;
        StartCoroutine("Second1Timer");
    }

    IEnumerator Second10Timer(){ // 10초마다 실행되는 타이머, 10초마다 enemy당 점수가 1씩 증가한다.
        yield return new WaitForSeconds(10f);
        enemyscore++;
        StartCoroutine("Second10Timer");
    }
    public void OnClickRestartBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        AudioManager.instance.PlayBGM("GameSceneBGM");
        Debug.Log("OnClickRestartBtn Click");
        Time.timeScale = 1; // 화면 재개

        //startBtnEvent.OnclickBtnChangeImage();
        //SceneLoader.Instance.LoadScene("GameScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //BlackOutFunction();
    }

    public void OnClickCancelBtn()
    {
        AudioManager.instance.PlaySFX("Touch");
        AudioManager.instance.PlayBGM("GameSceneBGM");
        Debug.Log("OnClickCancelBtn Click");
      
        ResultCanvasGroup.alpha = alpha000;
        ResultCanvas.enabled = false;
        //Time.timeScale = 1; // 화면 재개

        SceneLoader.Instance.LoadScene("MainScene");

    }
    /*
    public void ResultCanvasFadeOn()
    {
        StartCoroutine(IResultCanvasFadeOn());
    }
    
    IEnumerator IResultCanvasFadeOn()
    {
        Debug.Log("settingCanvasFadeOnClick ");
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        Time.timeScale = 1; // 화면 재개

        while (canvasGroup.alpha > alpha000)
        {
            canvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;

        GameManager.Instance.ResultSceneLoad();
    }
    */
    public void ResultSceneLoad()
    {

        Time.timeScale = 0; // 화면 정지

        // acculate
        gScore.accGamesPlayed += 1;
        gScore.accScorePlayed += score;
        gScore.accTimePlayed += playtime;
        gScore.accKillPlayed += killcount;
        gScore.accDistance += distance;

        //ote
        money = score - score % 100;

            

        // LongestGame Check
        if (gScore.LongestGame < playtime)
            gScore.LongestGame = playtime;

        // BestScore Check
        if (gScore.BestScore < score)
        {
            gScore.BestScore = score;
            // LeaderBoard Update
            DataBaseManager.Instance.AddScoreToLeaders(gScore, gUser.userName, killcount);
            //BestScoreTime 도 해야됨.
        }

        int second = playtime;
        int minute = second/60;
        second = second % 60;
        int hour = minute/60;
        minute = minute % 60;

        PlayTime.text = Convert.ToString(hour) + "H " + Convert.ToString(minute) + "M " + Convert.ToString(second) + "S";
        Scores.text = Convert.ToString(score);
        EnemiesKilled.text = Convert.ToString(killcount);
        Distance.text = Convert.ToString(distance);

        // setting Canvas Access
        ResultCanvasGroup.alpha = alpha100;
        ResultCanvasGroup.interactable = true;
        ResultCanvas.enabled = true;
        
       
        
        // UserScoreBoard Update
        DataBaseManager.Instance.UpdateScoreBoard(gScore);
        
    }

    public void gameOverDestroy(){
        itemSpawner.CancelSpawnItemTimer();
    }
   

    void Update() // 매순간의 점수를 업데이트
    {
        scoreText.text = "" + score;
        timeText.text = "Time : " + playtime;
        killText.text = "Kill : " + killcount;
    }
}
