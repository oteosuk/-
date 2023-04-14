using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; 

    private BoxCollider2D _area; 
    //카메라 크기 받아오기
    public Camera camera_; // 여기에 불러오고자 하는 카메라를 넣으면 된다.
    private float _cameraLeft;
    private float _cameraRight;
    private float _cameraBottom;
    private float _cameraTop;
    private float size_y;
    private float size_x;
    //=====================================
    public float timeSpawnMax = 2f;
    public float timeSpawnMin = 1f;
    private float _timeSpawn;

    private float _lastSpawnTime;

    private void Start()
    {
        size_y = camera_.orthographicSize;
        //Debug.LogError(size_y); // 450
        size_x = camera_.orthographicSize * Screen.width / Screen.height;
        //Debug.LogError(size_x); // 1000
        _cameraRight = size_x + camera_.gameObject.transform.position.x;
        _cameraLeft = size_x * -1 + camera_.gameObject.transform.position.x;
        _cameraTop = size_y + camera_.gameObject.transform.position.y;
        _cameraBottom = size_y * -1 + camera_.gameObject.transform.position.y;
        //Debug.LogError(camera_.gameObject.transform.position.x); // 800
        //Debug.LogError(camera_.gameObject.transform.position.y); // 450

        //=====================================================================

        _timeSpawn = Random.Range(timeSpawnMin, timeSpawnMax);
        _lastSpawnTime = 0;
        
    }
    public void SpawnItemTimer(){
        InvokeRepeating("SpawnItem", 2, 1);
        //if(GameObject.FindWithTag("ItemSpawner"))

    }
    public void CancelSpawnItemTimer(){
        CancelInvoke("SpawnItem");
    }
    private void SpawnItem(){
         if (Time.time >= _lastSpawnTime + _timeSpawn)
        {
            _lastSpawnTime = Time.time;
            _timeSpawn = Random.Range(timeSpawnMin, timeSpawnMax);
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector2 spawnPosition = GetRandomPosition();

        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        Destroy(item, 1000f); // 아이템이 1000초뒤에 사라짐
    }

    private Vector2 GetRandomPosition()
    {
        // 화면 가장자리쪽에는 나오지않게
        float posX = Random.Range(0f, 1500f); // 화면 가장자리쪽에는 나오지않게
        float posY = Random.Range(100f, 700f);

        /*
        Debug.LogError(posX);
        Debug.LogError(posY);
        Debug.LogError(_cameraLeft);
        Debug.LogError(_cameraRight);
        Debug.LogError(_cameraBottom);
        Debug.LogError(_cameraTop);
        */
        
        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos; 
    }
}
