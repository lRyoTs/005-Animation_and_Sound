using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30;
    private PlayerController PlayerControllerScript;
    public float leftBound;

    private void Start()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerControllerScript.gameOver) //If is not game over
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        //if obstacle out of bounds
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle")) {
            Destroy(gameObject); //Destroy obstacle
        }
    }
}
