using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //VARIABLEs FOR COMPONENTS
    private Rigidbody _rigidbody; //store all rigidboy infomation
    private Animator _animator; //store all animator information

    //PHYSICS VARIABLES
    private float jumpForce = 600f;
    private float gravityModifier = 1.5f;
    private bool isOnTheGround = true;

    //PARTICLES VARIABLES
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    //SOUND VARIABLES
    private AudioSource _audioSource;
    public AudioClip jumpSound;
    public AudioClip crashSound;


    //GAME VARIABLES
    public bool gameOver = false; //GameOver variable of the game
    private bool crouchToggle = false;
    private int playerLife = 3;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //Get component <rigidbody> information of the player
        _animator = GetComponent<Animator>(); //Get component <animator> information of the player
        _audioSource = GetComponent<AudioSource>(); //Get component <audioSource> information of the player
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver) {
            isOnTheGround = false;
            dirtParticle.Stop();
            _animator.SetTrigger("Jump_trig"); //Trigger jump animator
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _audioSource.PlayOneShot(jumpSound, 1);
        }

        //Crounch controls
        crouchToggle = false;
        if (Input.GetKey(KeyCode.LeftControl) && isOnTheGround && !gameOver) {
            crouchToggle = true;
        }
        _animator.SetBool("Crouch_b", crouchToggle);
    }

    //Function of the player collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            if (!gameOver) {
                dirtParticle.Play();
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerLife--; //Reduce life
            Debug.Log($"You lost a life. Remaining lifes {playerLife}");
            Destroy(collision.gameObject); //Destroy object collided

            //No lifes left
            if (playerLife == 0)
            {
                GameOver();
            }
            else {
                _audioSource.PlayOneShot(crashSound, 0.5f); //Hitted sound
            }
        }
    }

    //Function that manages the player GameOver
    private void GameOver() {
        int type = Random.Range(1, 3);
        gameOver = true;
        _animator.SetBool("Death_b", true); //Toggle on Death animator
        _animator.SetInteger("DeathType_int", type); //Get Death type
        explosionParticle.Play();
        _audioSource.PlayOneShot(crashSound, 1);
        dirtParticle.Stop();
    }
}
