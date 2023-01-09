using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefabs;
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
        if (PlayerControllerScript.gameOver) {
            CancelInvoke("SpawnObstacle");
        }    
    }

    private void SpawnObstacle() {
        Instantiate(obstaclePrefabs, spawnPos, obstaclePrefabs.transform.rotation);
    }
}
