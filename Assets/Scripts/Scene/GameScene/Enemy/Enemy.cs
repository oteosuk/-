using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    //protected GameManager _gameManager;
    //int eLife = 1;
    public float enemySpeed;
    public float moveXRate;
    public float moveYRate;

    // 상속해서 
    public virtual void EnemyInitSpeedRate(){
        //Debug.Log("Enemy - enemyInitSpeedRate");
    }

     public virtual void EnemyUpdateSpeedRate(){
        //Debug.Log("Enemy - enemyUpdateSpeedRate");
    }

     public virtual void OnTriggerEnter2D(Collider2D other)
     {
        //Debug.Log("OnTriggerEnter2D");
     }
}
