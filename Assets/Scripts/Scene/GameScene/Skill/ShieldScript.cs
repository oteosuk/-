using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private GameObject player;
    public AudioSource audioSource;

    float time = 0;
    float blinktime = 0.1f;
    float xtime = 0;
    float waittime = 0.2f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 6f);
    }

    void Update()
    {
        transform.position = player.transform.position; // 플레이어에 붙어서 따라다니는 방패
        // 끝날 떄쯤 깜빡이는 기능
        if(time < 4f) // 버프 지속시간 -3초
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // 처음엔 켜져있고
        }
        else // 약 3초
        {
            if(xtime<blinktime) // 깜빡
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - xtime * 10); //꺼졌다가
            }
            else if(xtime<waittime + blinktime)
            {

            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (xtime - (waittime + blinktime)) * 10);
                //켜졌다가
                if(xtime> waittime+ blinktime * 2)
                {
                    xtime = 0;
                    waittime *= 0.8f; //깜빡이는 시간 줄어들기
                    if(waittime < 0.02f)
                    {
                        time = 0;
                        waittime = 0.2f;
                    }
                }
            }
            xtime += Time.deltaTime;
        }
        time += Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vibration.Vibrate(20);
            GameManager.Instance.killcount++;
            AudioManager.instance.PlaySFX("EnemyPop");
        }
    }
}