using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator animator;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private GameManager gameManager;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool doubleJump = true;
    public bool gameOver = false;
    public bool isSprinting = false;
    private float timer = 0f;
    private float scoreIncreaseInterval = 1f;
    private Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startingPos = new Vector3(0, 0, 0);
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //thực hiện động tác jump cho nhân vật, nếu như nhân vật chưa chạm đất có thể thực hiện việc nhảy 2 lần.
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {          
            isOnGround = false;
            jump();
            
        }else if(Input.GetKeyDown(KeyCode.Space) && !isOnGround && doubleJump)
        {
            doubleJump = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.Play("Running_Jump", 3, 0f);
            audioSource.PlayOneShot(jumpSound, 1.0f);
        }

        if (!gameOver)
            calScore(1);
        
        //thực hiện việc bắt event người dùng nhấn giữ left shift để tăng tốc cho nhân vật.
        if (Input.GetKey(KeyCode.LeftShift) && !gameOver)
        {
            calScore(2);
            gameManager.changeAnimatorSpeed(2);
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !gameOver)
        {
            
            gameManager.changeAnimatorSpeed(1);
            isSprinting = false;
        }
    }

    void calScore(int value)
    {
        timer += Time.deltaTime;
        if (timer >= scoreIncreaseInterval)
        {
            gameManager.addScore(value);
            timer = 0f;
        }
    }

    //private void WalkToSpawn()
    //{
    //    if (transform.position.x != startingPos.x)
    //        transform.position = Vector3.MoveTowards(transform.position, startingPos, 0.3f);
    //    else animator.SetFloat("Speed_f", 1);
    //}

    void jump()
    {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            audioSource.PlayOneShot(jumpSound, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJump = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {           
            if(gameOver != true)
            {   audioSource.Stop();
                audioSource.PlayOneShot(crashSound, 1.0f);
                explosionParticle.Play();
                dirtParticle.Stop();
                animator.SetBool("Death_b", true);
                animator.SetInteger("DeathType_int", 1);
                Debug.Log("Game over");
            }
            gameOver = true;
        }
    }
}
