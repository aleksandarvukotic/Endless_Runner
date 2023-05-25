using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;

    private Vector3 spawnPos = new Vector3(25,0,0);
    private float startDelay = 2f;
    private float minRepeatRate = 1.5f;
    private float maxRepeatRate = 3f;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("SpawnObstacle", startDelay);
    }

    void SpawnObstacle()
    {
        if(playerControllerScript.gameOver == false)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, Quaternion.identity);

            float repeatRate = Random.Range(minRepeatRate, maxRepeatRate);
            Invoke("SpawnObstacle", repeatRate);
        }
    }
}
