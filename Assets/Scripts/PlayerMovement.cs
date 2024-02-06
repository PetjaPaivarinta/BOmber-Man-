using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;

    public GameManager gameManager;

    public AudioSource deathSound;


  public AudioSource powerupSound;
  private float powerupTimer = 0f;
        private float powerupDuration = 5f;
        private bool isPowerupActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
      

    void Update()
{
    // Update powerup timer
    if (isPowerupActive)
    {
        powerupTimer -= Time.deltaTime;
        if (powerupTimer <= 0f)
        {
            // Reset speed
            moveSpeed = 5f;
            isPowerupActive = false;
        }
    }

    // Handle player movement
    Vector2 movement = new Vector2(Input.GetKey(KeyCode.D) ? 1 : (Input.GetKey(KeyCode.A) ? -1 : 0), Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0)).normalized;
    rb.velocity = movement * moveSpeed;

    // Flip player sprite based on direction
    if (Input.GetKey(KeyCode.D))
    {
        transform.localScale = new Vector3(0.055f, 0.055f, 1);
    }
    else if (Input.GetKey(KeyCode.A))
    {
        transform.localScale = new Vector3(-0.055f, 0.055f, 1);
    }
}

        public void ActivatePowerup()
        {
            // Start powerup timer
            powerupTimer = powerupDuration;
            isPowerupActive = true;

            // Modify speed for powerup
            moveSpeed = 10f;
        }

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Powerup"))
    {
        // Destroy the powerup so it doesn't carry on and hit more things.
        Destroy(collision.gameObject);
        // Activate the powerup
        ActivatePowerup();
        powerupSound.Play();
    } 
}

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Bomb2"))
    {
        deathSound.Play();
        Debug.Log("Player 1 hit a bomb!");
        Destroy(gameObject);
        Destroy(collision.gameObject);
        // Assuming you have a reference to a GameManager script
        gameManager.ReloadSceneAfterDelay(2f);
    }
}
}