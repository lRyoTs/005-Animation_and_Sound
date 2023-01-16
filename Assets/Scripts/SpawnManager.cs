using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject [] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private PlayerController PlayerControllerScript; //get player controller info
    private float startDelay = 2f;
    private float repeatRate = 2f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        PlayerControllerScript = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        //GameOver Management
        if (PlayerControllerScript.gameOver) {
            CancelInvoke("SpawnObstacle");
        }    
    }

    private void SpawnObstacle() {
        int index = Random.Range(0, obstaclePrefabs.Length); //Get index
        Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
    }
}
