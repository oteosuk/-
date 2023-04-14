using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : Enemy
{
    Rigidbody2D rb;
    Transform target;
    int score;

    void Start()
    {
        int score = GameManager.Instance.enemyscore;
        enemySpeed = 120f;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    void Update()
    {
        score = GameManager.Instance.enemyscore;
        FollowTarget();
    }

    void FollowTarget()
    {
        //Debug.Log("FollowEnemy - FollowTargetUpdate");
        if(target != null){
        transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
        }
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
