using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveLeftEnemy : Enemy
{
    int score;
    void Start()
    {
        int score = GameManager.Instance.enemyscore;
        enemySpeed = 160f;
        Destroy(gameObject, 30);
    }

    void Update()
    {
        score = GameManager.Instance.enemyscore;
        MoveUp();
    }

    void MoveUp()
    {
        transform.position += Vector3.left * enemySpeed * Time.deltaTime;
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
