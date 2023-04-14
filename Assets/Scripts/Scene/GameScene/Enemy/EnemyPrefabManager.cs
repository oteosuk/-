using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabManager : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject BounceEnemyPrefab;
    public GameObject FollowEnemyPrefab;
    public GameObject MoveUpEnemyPrefab;
    public GameObject MoveDownEnemyPrefab;
    public GameObject MoveRightEnemyPrefab;
    public GameObject MoveLeftEnemyPrefab;

    public GameObject Boss1Prefab;
    public GameObject Boss2Prefab;
    public GameObject Boss3Prefab;
    public GameObject Boss4Prefab;

    public GameObject[] enemies; // 생성된 객체를 받아올 배열

    // 카메라 크기
    public Camera camera_ = null; // 여기에 불러오고자 하는 카메라를 넣으면 된다.
    private float _cameraLeft;
    private float _cameraRight;
    private float _cameraBottom;
    private float _cameraTop;
    private float size_y;
    private float size_x;
    
    public int enemyMax = 25;

    public void SpawnBounceEnemy() // 튕기는 적 소환
    {
        float randomX = Random.Range(_cameraLeft, _cameraRight); //적이 나타날 X좌표를 랜덤으로 생성
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < enemyMax)
            {
                //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                GameObject enemy = (GameObject)Instantiate(BounceEnemyPrefab, new Vector3(randomX, _cameraTop, 0f),Quaternion.identity);

                //Debug.Log("EnemyPrefab : BounceEnemyPrefab 객체 생성");
            }

        }
    }

    public void SpawnFollowEnemy() // 따라오는 적 소환
    {

        float randomX = Random.Range(_cameraLeft, _cameraRight); //적이 나타날 X좌표를 랜덤으로 생성
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < enemyMax)
            {
                //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                GameObject enemy = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(randomX, _cameraTop, 0f), Quaternion.identity);


                //Debug.Log("EnemyPrefab : FollowEnemyPrefab 객체 생성");
            }
        }
    }

    public void SpawnBoss1Enemy() // 보스1
    {
        AudioManager.instance.PlaySFX("Boss1");
        int bossMax = 5;
        float randomY = Random.Range(_cameraBottom, _cameraTop); //적이 나타날 X좌표를 랜덤으로 생성
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Boss");
            if (enemies.Length < bossMax)
            {
                //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                GameObject enemy = (GameObject)Instantiate(Boss1Prefab, new Vector3(_cameraRight, randomY, 0f), Quaternion.identity);
                //Debug.Log("EnemyPrefab : FollowEnemyPrefab 객체 생성");
            }
        }
    }
    public void SpawnBoss2Enemy() // 보스2
    {
        AudioManager.instance.PlaySFX("Boss2");
        int bossMax = 5;
        float randomY = Random.Range(_cameraBottom, _cameraTop); //적이 나타날 X좌표를 랜덤으로 생성
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Boss");
            if (enemies.Length < bossMax)
            {
                //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                GameObject enemy = (GameObject)Instantiate(Boss2Prefab, new Vector3(_cameraLeft, randomY, 0f), Quaternion.identity);
                //Debug.Log("EnemyPrefab : FollowEnemyPrefab 객체 생성");
            }
        }
    }
    public void SpawnBoss3Enemy() // 보스3
    {
        AudioManager.instance.PlaySFX("Boss3");
        int bossMax = 5;
        float randomY = Random.Range(_cameraBottom, _cameraTop); //적이 나타날 X좌표를 랜덤으로 생성
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Boss");
            if (enemies.Length < bossMax)
            {
                //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                GameObject enemy = (GameObject)Instantiate(Boss3Prefab, new Vector3(_cameraRight, randomY, 0f), Quaternion.identity);
                //Debug.Log("EnemyPrefab : FollowEnemyPrefab 객체 생성");
            }
        }
    }
    public void SpawnBoss4Enemy() // 보스4
    {
        AudioManager.instance.PlaySFX("Boss4");
        int bossMax = 5;
        float randomY = Random.Range(_cameraBottom, _cameraTop); //적이 나타날 X좌표를 랜덤으로 생성
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Boss");
            if (enemies.Length < bossMax)
            {
                //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                GameObject enemy = (GameObject)Instantiate(Boss4Prefab, new Vector3(_cameraLeft, randomY, 0f), Quaternion.identity);
                //Debug.Log("EnemyPrefab : FollowEnemyPrefab 객체 생성");
            }
        }
    }

    public void SpawnSurroundPattern() // 사방에서 FollowEnemy가 나타나는 패턴
    {
        float xLength = _cameraRight - _cameraLeft; // 카메라 가로의 길이
        float yLength = _cameraTop - _cameraBottom; // 카메라 세로의 길이
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < enemyMax)
            {
                //패턴 (가로, 세로 비율을 이용하여 각 위치에 소환한다.)
                GameObject enemyup1 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft + xLength * 0.2f, _cameraTop, 0f), Quaternion.identity);
                GameObject enemyup2 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft + xLength * 0.4f, _cameraTop, 0f), Quaternion.identity);
                GameObject enemyup3 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft + xLength * 0.6f, _cameraTop, 0f), Quaternion.identity);
                GameObject enemyup4 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft + xLength * 0.8f, _cameraTop, 0f), Quaternion.identity);

                GameObject enemyleft1 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.2f, 0f), Quaternion.identity);
                GameObject enemyleft2 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.4f, 0f), Quaternion.identity);
                GameObject enemyleft3 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.6f, 0f), Quaternion.identity);
                GameObject enemyleft4 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.8f, 0f), Quaternion.identity);

                GameObject enemydown1 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft + xLength * 0.2f, _cameraBottom, 0f), Quaternion.identity);
                GameObject enemydown2 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft + xLength * 0.4f, _cameraBottom, 0f), Quaternion.identity);
                GameObject enemydown3 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft + xLength * 0.6f, _cameraBottom, 0f), Quaternion.identity);
                GameObject enemydown4 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraLeft + xLength * 0.8f, _cameraBottom, 0f), Quaternion.identity);

                GameObject enemyright1 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.2f, 0f), Quaternion.identity);
                GameObject enemyright2 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.4f, 0f), Quaternion.identity);
                GameObject enemyright3 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.6f, 0f), Quaternion.identity);
                GameObject enemyright4 = (GameObject)Instantiate(FollowEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.8f, 0f), Quaternion.identity);

                //Debug.Log("SpawnSurroundPattern : 패턴 생성");
            }
        }
    }

    public void SpawnGoRightPattern() // 왼쪽에서 오른쪽으로 일자 모양 패턴
    {
        float xLength = _cameraRight - _cameraLeft; // 카메라 가로의 길이
        float yLength = _cameraTop - _cameraBottom; // 카메라 세로의 길이
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < enemyMax)
            {
                //패턴 (가로, 세로 비율을 이용하여 각 위치에 소환한다.)
                GameObject enemyleft1 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.1f, 0f), Quaternion.identity);
                GameObject enemyleft2 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.3f, 0f), Quaternion.identity);
                GameObject enemyleft3 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.5f, 0f), Quaternion.identity);
                GameObject enemyleft4 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.7f, 0f), Quaternion.identity);
                GameObject enemyleft5 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.9f, 0f), Quaternion.identity);

                //Debug.Log("SpawnSurroundPattern : 패턴 생성");
            }
        }
    }

    public void SpawnDiaPattern() // 왼쪽에서 오른쪽으로 다이아모양으로 가는 패턴
    {
        float xLength = _cameraRight - _cameraLeft; // 카메라 가로의 길이
        float yLength = _cameraTop - _cameraBottom; // 카메라 세로의 길이
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < enemyMax)
            {
                //패턴 (가로, 세로 비율을 이용하여 각 위치에 소환한다.)
                GameObject e0 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 300f, _cameraBottom + yLength * 0.95f, 0f), Quaternion.identity);
                GameObject e1 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 400f, _cameraBottom + yLength * 0.80f, 0f), Quaternion.identity);
                GameObject e2 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 200f, _cameraBottom + yLength * 0.80f, 0f), Quaternion.identity);
                GameObject e3 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 500f, _cameraBottom + yLength * 0.65f, 0f), Quaternion.identity);
                GameObject e4 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 100f, _cameraBottom + yLength * 0.65f, 0f), Quaternion.identity);
                GameObject e5 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft, _cameraBottom + yLength * 0.5f, 0f), Quaternion.identity);
                GameObject e6 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 600f, _cameraBottom + yLength * 0.5f, 0f), Quaternion.identity);
                GameObject e7 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 100f, _cameraBottom + yLength * 0.35f, 0f), Quaternion.identity);
                GameObject e8 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 500f, _cameraBottom + yLength * 0.35f, 0f), Quaternion.identity);
                GameObject e9 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 200f, _cameraBottom + yLength * 0.20f, 0f), Quaternion.identity);
                GameObject e10 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 400f, _cameraBottom + yLength * 0.20f, 0f), Quaternion.identity);
                GameObject e11 = (GameObject)Instantiate(MoveRightEnemyPrefab, new Vector3(_cameraLeft - 300f, _cameraBottom + yLength * 0.05f, 0f), Quaternion.identity);

                //Debug.Log("SpawnSurroundPattern : 패턴 생성");
            }
        }
    }

    public void SpawnWavePattern() // 오른쪽에서 왼쪽으로 웨이브모양으로 가는 패턴
    {
        float xLength = _cameraRight - _cameraLeft; // 카메라 가로의 길이
        float yLength = _cameraTop - _cameraBottom; // 카메라 세로의 길이
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < enemyMax)
            {
                //패턴 (가로, 세로 비율을 이용하여 각 위치에 소환한다.)
                GameObject e0 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.05f, 0f), Quaternion.identity);
                GameObject e1 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 100f, _cameraBottom + yLength * 0.20f, 0f), Quaternion.identity);
                GameObject e2 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 200f , _cameraBottom + yLength * 0.35f, 0f), Quaternion.identity);
                GameObject e3 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 300f, _cameraBottom + yLength * 0.50f, 0f), Quaternion.identity);
                GameObject e4 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 400f, _cameraBottom + yLength * 0.65f, 0f), Quaternion.identity);
                GameObject e5 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 500f, _cameraBottom + yLength * 0.80f, 0f), Quaternion.identity);
                GameObject e6 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 600f, _cameraBottom + yLength * 0.95f, 0f), Quaternion.identity);
                GameObject e7 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 700f, _cameraBottom + yLength * 0.80f, 0f), Quaternion.identity);
                GameObject e8 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 800f, _cameraBottom + yLength * 0.65f, 0f), Quaternion.identity);
                GameObject e9 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 900f, _cameraBottom + yLength * 0.50f, 0f), Quaternion.identity);
                GameObject e10 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1000f, _cameraBottom + yLength * 0.35f, 0f), Quaternion.identity);
                GameObject e11 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1100f, _cameraBottom + yLength * 0.20f, 0f), Quaternion.identity);
                GameObject e12 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1200f, _cameraBottom + yLength * 0.05f, 0f), Quaternion.identity);
                GameObject e13 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1300f, _cameraBottom + yLength * 0.20f, 0f), Quaternion.identity);
                GameObject e14 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1400f, _cameraBottom + yLength * 0.35f, 0f), Quaternion.identity);
                GameObject e15 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1500f, _cameraBottom + yLength * 0.50f, 0f), Quaternion.identity);
                GameObject e16 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1600f, _cameraBottom + yLength * 0.65f, 0f), Quaternion.identity);
                GameObject e17 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1700f, _cameraBottom + yLength * 0.80f, 0f), Quaternion.identity);
                GameObject e18 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight + 1800f, _cameraBottom + yLength * 0.95f, 0f), Quaternion.identity);
                //Debug.Log("SpawnSurroundPattern : 패턴 생성");
            }
        }
    }

    public void SpawnGoLeftPattern() // 오른쪽에서 왼쪽으로 일자 모양 패턴
    {
        float xLength = _cameraRight - _cameraLeft; // 카메라 가로의 길이
        float yLength = _cameraTop - _cameraBottom; // 카메라 세로의 길이
        if (enableSpawn)
        {
            // Prefab의 태그를 이용해서 적 갯수 찾아오기
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < enemyMax)
            {
                //패턴 (가로, 세로 비율을 이용하여 각 위치에 소환한다.)
                GameObject enemyright1 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.2f, 0f), Quaternion.identity);
                GameObject enemyright2 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.4f, 0f), Quaternion.identity);
                GameObject enemyright3 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.6f, 0f), Quaternion.identity);
                GameObject enemyright4 = (GameObject)Instantiate(MoveLeftEnemyPrefab, new Vector3(_cameraRight, _cameraBottom + yLength * 0.8f, 0f), Quaternion.identity);
                //Debug.Log("SpawnSurroundPattern : 패턴 생성");
            }
        }
    }
    public void levelUp(){
        enemyMax++;
    }

    public void SpawnEnemyTimer(){
        //GameManager 에서 호출

        //Basic
        InvokeRepeating(nameof(SpawnBounceEnemy), 2, 1.5f); // 2초뒤부터 1.5초마다
        InvokeRepeating(nameof(SpawnFollowEnemy), 2, 4); 

        //Pattern
        //InvokeRepeating(nameof(SpawnGoRightPattern), 5, 12); 
        InvokeRepeating(nameof(SpawnGoLeftPattern), 5, 15); 
        InvokeRepeating(nameof(SpawnDiaPattern), 13, 100);
        InvokeRepeating(nameof(SpawnSurroundPattern), 21, 100); 
        InvokeRepeating(nameof(SpawnWavePattern), 29, 100); 

        //Boss
        InvokeRepeating(nameof(SpawnBoss1Enemy), 15, 100); 
        InvokeRepeating(nameof(SpawnBoss2Enemy), 30, 100);
        InvokeRepeating(nameof(SpawnBoss3Enemy), 45, 100); 
        InvokeRepeating(nameof(SpawnBoss4Enemy), 60, 100); 


        InvokeRepeating(nameof(levelUp), 8, 1); // 8초당 최대enemy수를 1씩 증가시킨다.
    }

    void Start()
    {

        //카메라 크기 할당
        size_y = camera_.orthographicSize;
        size_x = camera_.orthographicSize * Screen.width / Screen.height;
        
        _cameraRight = size_x + camera_.gameObject.transform.position.x;
        _cameraLeft = size_x * -1 + camera_.gameObject.transform.position.x;
        _cameraTop = size_y + camera_.gameObject.transform.position.y;
        _cameraBottom = size_y * -1 + camera_.gameObject.transform.position.y;
    }
}
