using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEnemy : Enemy
{
    int score;
    void Start()
    {
        int score = GameManager.Instance.enemyscore;
        EnemyInitSpeedRate();
    }

    void Update()
    {
        score = GameManager.Instance.enemyscore;
        EnemyUpdateSpeedRate();
    }
    public override void EnemyInitSpeedRate(){
        //Debug.Log("BounceEnemy - enemyInitSpeedRate");

        enemySpeed = 300.0f;
        moveXRate = Random.Range(-0.8f, 0.8f);
        moveYRate = Random.Range(-0.8f, 0.8f);

        while (Mathf.Abs(moveXRate) < 0.3f)
        {
            moveXRate = Random.Range(-0.8f, 0.8f);
        }

        while (Mathf.Abs(moveYRate) < 0.3f)
        {
            moveYRate = Random.Range(-0.8f, 0.8f);
        }
    }

    public override void EnemyUpdateSpeedRate(){
        //Debug.Log("BounceEnemy - enemyUpdateSpeedRate");

        transform.Translate(Vector3.right * Time.deltaTime * enemySpeed * moveXRate, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * enemySpeed * moveYRate, Space.World);

        
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.x < 0f)
        {
            position.x = 0f;
            moveXRate = Random.Range(0.3f, 0.8f);
        }
        if (position.y < 0f)
        {
            position.y = 0f;
            moveYRate = Random.Range(0.3f, 0.8f);
        }
        if (position.x > 1f)
        {
            position.x = 1f;
            moveXRate = Random.Range(-0.8f, -0.3f);
        }
        if (position.y > 1f)
        {
            position.y = 1f;
            moveYRate = Random.Range(-0.8f, -0.3f);
        }
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skill"))
        {
            //Debug.Log("GameManager Score");
            GameManager.Instance.AddScore(score);
            Destroy(gameObject);
        }
    }
}
