using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
public class BounceScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public float rotateSpeed;
    public float moveXRate;
    public float moveYRate;
    void Start()
    {
        InitSpeedRate();
        Destroy(gameObject, 8f);
    }

    void Update()
    {
        UpdateSpeedRate();
    }
    public void InitSpeedRate(){
        //Debug.Log("BounceSkill - InitSpeedRate");

        Speed = 800.0f;
        rotateSpeed = 100.0f;
        moveXRate = 0.5f;
        moveYRate = 0.5f;
    }
    
    public void UpdateSpeedRate(){
        //Debug.Log("BounceSkill - UpdateSpeedRate");

        transform.Translate(Vector3.right * Time.deltaTime * Speed * moveXRate, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * Speed * moveYRate, Space.World);

        
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.x < 0f)
        {
            position.x = 0f;
            moveXRate = 0.5f; //Random.Range(0.3f, 1.0f);
        }
        if (position.y < 0f)
        {
            position.y = 0f;
            moveYRate = 0.5f; // Random.Range(0.3f, 1.0f);
        }
        if (position.x > 1f)
        {
            position.x = 1f;
            moveXRate = -0.5f; // Random.Range(-1.0f, -0.3f);
        }
        if (position.y > 1f)
        {
            position.y = 1f;
            moveYRate = -0.5f; //Random.Range(-1.0f, -0.3f);
        }
        transform.position = Camera.main.ViewportToWorldPoint(position);
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
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