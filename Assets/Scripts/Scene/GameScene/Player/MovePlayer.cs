using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;


public class MovePlayer : MonoBehaviour
{
    bl_Joystick _js;
    float _speed;
    private Vector3 previousPosition; //움직인거리를 계산하기위해 전 포지션을 저장할 변수
//=================================
    //private Gyroscope gyroscope;
    //private Vector3 angleGyro;

//=====================================
    //Vector2 _moveLimit = new Vector2(1600, 900);

    //public PlayCanvasFade playfade;
    //public GameObject scoreScene;
    
    //카메라크기받아오기 - start함수에서 초기화시켜준다.
    //=========================

    public Camera camera_ = null; // 여기에 불러오고자 하는 카메라를 넣으면 된다.
    private float _cameraLeft;
    private float _cameraRight;
    private float _cameraBottom;
    private float _cameraTop;
    private float size_y;
    private float size_x;
    //===========================
    private User pUser;
    private bool mode;
    GameObject scoreScene;
    GameObject clonetmp;
    private void Awake()
    {
        
    }
    private void Start()
    {
        scoreScene = GameObject.Find("scoreScene");


        //카메라 크기 할당
        
        size_y = camera_.orthographicSize;
        size_x = camera_.orthographicSize * Screen.width / Screen.height;
        
        _cameraRight = size_x + camera_.gameObject.transform.position.x;
        _cameraLeft = size_x * -1 + camera_.gameObject.transform.position.x;
        _cameraTop = size_y + camera_.gameObject.transform.position.y;
        _cameraBottom = size_y * -1 + camera_.gameObject.transform.position.y;
        
        //====================================================================
        previousPosition = transform.position;
        pUser = DataBaseManager.Instance.getUserData();

        mode = pUser.gameType;
        //Debug.LogError("mode" + mode);
        _js = GameObject.Find("UI/Joystick").GetComponent<bl_Joystick>(); // 조이스틱 오브젝트를 저장할 변수

    }
   
    void Update()
    {
        if (mode)
        {
            JSMove(); // if 환경설정에서 조이스틱을 설정했다면
        } else
        {
            GyroMove();
        }
    }

    void JSMove()
    {
        _speed = 1.3f;
        transform.localPosition = ClampPosition(transform.localPosition);

        //스틱이 향해있는 방향을 저장
        Vector3 direction = new Vector3(_js.Horizontal, _js.Vertical, 0);

        //오브젝트의 위치를 direction 방향으로 이동
        transform.position += direction * _speed * Time.deltaTime * 100;
        float dis = Vector3.Distance(transform.position, previousPosition); // 전위치와 현재위치 사이의 거리를 저장
        GameManager.Instance.distance += dis; // 누적해서 dis거리를 더해준다.
        //오브젝트의 방향을 가는 방향으로 회전
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg);
        
        previousPosition = transform.position; // 현재 위치가 곧 전 위치가 된다.(update함수로 돌리므로)

        //Debug.Log(transform.position);
    }

    void GyroMove()
    {
        _speed = 300f;// 조이스틱은 1.3가 적당
        transform.localPosition = ClampPosition(transform.localPosition);

        //스틱이 향해있는 방향을 저장
        Vector3 direction = Input.acceleration;

        //오브젝트의 위치를 direction 방향으로 이동
        transform.position += direction * _speed * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, previousPosition); // 전위치와 현재위치 사이의 거리를 저장
        GameManager.Instance.distance += dis; // 누적해서 dis거리를 더해준다.
        //오브젝트의 방향을 가는 방향으로 회전
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg);
        
        previousPosition = transform.position; // 현재 위치가 곧 전 위치가 된다.(update함수로 돌리므로)

        //Debug.Log(transform.position);

    }

    // 이동 범위 제한 벡터 함수
    public Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3
        (
            Mathf.Clamp(position.x, _cameraLeft, _cameraRight),
            Mathf.Clamp(position.y, _cameraBottom, _cameraTop),
            0
        );
    }


    // 적과 부딪혔을 때 게임오버UI를 뜨게 한다.
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            //Destroy(itemSpawner);
            GameManager.Instance.gameOverDestroy();
            Destroy(gameObject);
            Vibration.Vibrate(500);
            AudioManager.instance.PlaySFX("Gameover");
            AudioManager.instance.StopBGM();
            scoreScene.SetActive(false);
            PlayCanvasFade.instance.PlayCanvasFadeOnClick();
            //playfade.PlayCanvasFadeOnClick();
            //playfade.GetComponent<PlayCanvasFade>().PlayCanvasFadeOnClick();
        }

        Item item = other.GetComponent<Item>();
        if (other.CompareTag("Item"))
        {
            Vibration.Vibrate(50);
            GameManager.Instance.eatItem++;
            item.Use(gameObject);
            //Debug.Log("충돌!!!!");
        }
    }
}
