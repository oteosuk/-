using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float shakeTime = 0.4f;
    public float shakeSpeed = 1.5f;
    public float shakeAmount = 8f;
    public float destroytime;
    private Transform cam;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform; // 카메라 불러오기
        StartCoroutine(Shake());//스킬발동중 카메라 흔들림
        Destroy(gameObject, destroytime);
    }
    void Update()
    {

    }
    IEnumerator Shake() // 카메라 흔들림 효과 기능
    {
        Vector3 originPosition = cam.localPosition;
        float elapsedTime = 0.0f;

        while(elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            cam.localPosition = Vector3.Lerp(cam.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        cam.localPosition = originPosition;
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
