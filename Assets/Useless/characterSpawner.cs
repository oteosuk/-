using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSpawner : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject player;

    void Start()
    {
        //player = Instantiate(charPrefabs[(int)CharacterManager.instance.currentCharacter]);
        //player.transform.position = transform.position;
    }
}
