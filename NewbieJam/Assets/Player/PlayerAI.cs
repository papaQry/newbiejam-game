using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float jumpForce;
    [SerializeField] float minjumpDelay, maxjumpDelay;
    [SerializeField] float detectionDistance;
    [SerializeField] float jumpCooldown;
    [SerializeField] float maxHealth = 1000;
    public float currentHealth;
    public bool isDead = false;

    [Header("References")]
    [SerializeField] GameObject detectionOriginPoint;
    [SerializeField] Transform planeDetectionOrigin;
    [SerializeField] PlayerHealth playerHealth;

    bool isGrounded;
    float currentJumpCD;
    bool jumpOnCooldown = false;

    #region Component Refs
    Animator animator;
    Rigidbody2D rb;
    Collider2D colliderPlayer;
    CapsuleCollider2D capsuleCollider;
    Timer timer;
    #endregion

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliderPlayer = GetComponent<Collider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        timer = FindObjectOfType<Timer>();

        currentHealth = maxHealth;
        playerHealth.SetMaxHealth(maxHealth);
        currentJumpCD = jumpCooldown;
    }
    
    private void Update() {
        ObstacleDetection();
        PlayerDeath();
        PlayerEscape();
    }

#region PlayerMovement

    void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    #endregion

    void ObstacleDetection ()
    {
        RaycastHit2D detectionLine = Physics2D.Raycast(detectionOriginPoint.transform.position, detectionOriginPoint.transform.right, detectionDistance);
        Debug.DrawRay(detectionOriginPoint.transform.position, detectionOriginPoint.transform.right * detectionDistance, Color.green);
        
        if(detectionLine.collider != null && detectionLine.collider.gameObject.tag != "Plane" && isGrounded && !jumpOnCooldown)
        {
            Debug.DrawRay(detectionOriginPoint.transform.position, detectionLine.point, Color.red);
            Invoke("Jump", Random.Range(minjumpDelay, maxjumpDelay));
            jumpOnCooldown = true;
        }

        if(jumpOnCooldown)
        {
            currentJumpCD -= 1 / jumpCooldown * Time.deltaTime;
            if(currentJumpCD <= 0)
            {
                jumpOnCooldown = false;
                currentJumpCD = jumpCooldown;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        playerHealth.SetHealth(currentHealth);
    }

    void PlayerDeath()
    {
        if(currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
        }
    }

    void PlayerEscape()
    {
        if(timer.timerRanOut)
        {
            animator.SetTrigger("TimerRanOut");
        }
    }

    public void LoadEndScreenIfDead()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void LoadEndScreenIfTimerRanOut()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
