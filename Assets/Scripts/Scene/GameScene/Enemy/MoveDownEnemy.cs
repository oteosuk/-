using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDownEnemy : Enemy
{
    int score;
    void Start()
    {
        int score = GameManager.Instance.enemyscore;
        enemySpeed = 160f;
        Destroy(gameObject, 8);
    }

    void Update()
    {
        MoveUp();
        score = GameManager.Instance.enemyscore;
    }

    void MoveUp()
    {
        transform.position += Vector3.down * enemySpeed * Time.deltaTime;
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
