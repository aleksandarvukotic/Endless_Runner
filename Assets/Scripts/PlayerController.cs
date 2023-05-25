using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crushSound;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround;
    public bool doubleJumpAccess;
    public bool dashAbility = false;
    private float dashMultiplier = 1.5f;
    public bool gameOver = false;

    public float playerScore;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        playerScore = 0f;
    }


    void Update()
    {
        playerScore += Time.time / 10;

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumpAccess = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && doubleJumpAccess && !gameOver && !isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumpAccess = false;
        }

        if (Input.GetKeyDown(KeyCode.D) && !dashAbility)
        {
            Time.timeScale *= dashMultiplier;
            dashAbility = true;
        }
        else if(Input.GetKeyDown(KeyCode.D) && dashAbility)
        {
            Time.timeScale /= dashMultiplier;
            dashAbility = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crushSound, 1.0f);
            Debug.Log("Game Over!");
            gameOver = true;
        }
    }
}
