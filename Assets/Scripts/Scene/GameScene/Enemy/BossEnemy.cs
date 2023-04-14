using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : Enemy
{
    Rigidbody2D rb;
    Transform target;
    int score;

    public int destroyTime;

    void Start()
    {
        int score = GameManager.Instance.enemyscore;
        enemySpeed = 90f;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Destroy(gameObject, destroyTime);
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
}
