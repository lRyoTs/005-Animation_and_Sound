using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody; //store all rigidboy infomation
    private float jumpForce = 100f;
    private bool isOnTheGround = true;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //Get component <rigidbody> information of the player
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround) {
            isOnTheGround = false;
           _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
        }
    }
}
