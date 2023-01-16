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
    public float gravityModifier = 1.5f;
    private bool isOnTheGround = true;
    
    //PARTICLES VARIABLES
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    //SOUND VARIABLES
    private AudioSource _audioSource; 
    public AudioClip jumpSound;
    public AudioClip crashSound;
   

    //GAME VARIABLES
    public bool gameOver = false;
    public bool crouchToggle = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //Get component <rigidbody> information of the player
        _animator = GetComponent<Animator>(); //Get component <animator> information of the player
        _audioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        crouchToggle = false;
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver) {
            isOnTheGround = false;
            dirtParticle.Stop();
            _animator.SetTrigger("Jump_trig");
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _audioSource.PlayOneShot(jumpSound, 1);           
        }

        if (Input.GetKey(KeyCode.LeftControl) && isOnTheGround && !gameOver) {
            crouchToggle = true;  
        }
        _animator.SetBool("Crouch_b", crouchToggle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            dirtParticle.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    private void GameOver() {
        int type = Random.Range(1, 3);
        gameOver = true;
        dirtParticle.Stop();
        _animator.SetBool("Death_b", true);
        _animator.SetInteger("DeathType_int", type);
        explosionParticle.Play();
        _audioSource.PlayOneShot(crashSound, 1);
    }
}
