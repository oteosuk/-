using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

// 아이템 타입들이 반드시 구현해야하는 인터페이스
public class Item : MonoBehaviour
{
    private float time = 5f;
    private float _size = 5f;
    private float _upSizeTime = 0.2f;
    private float _initsize = 5f;
    
    
    public virtual void sizeBounceEffect()
    {
        if(time <= _upSizeTime)
        {
            transform.localScale = Vector3.one * (1 + _size * time) * _initsize;
        }
        else if (time <= _upSizeTime*2)
        {
            transform.localScale = Vector3.one * (2*_size * _upSizeTime + 1 - time * _size)* _initsize;
        }
        else
        {
            transform.localScale = Vector3.one* _initsize;
        }
        time += Time.deltaTime;
    }
    // 입력으로 받는 target은 아이템 효과가 적용될 대상
    public virtual void Use(GameObject target)
    {
        Debug.Log("Use");
    }
}