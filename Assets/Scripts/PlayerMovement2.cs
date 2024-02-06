using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
  
    public AudioSource powerupSound;

    public AudioSource deathSound;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
      private float powerupTimer = 0f;
        private float powerupDuration = 5f;
        private bool isPowerupActive = false;



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
    Vector2 movement = new Vector2(Input.GetKey(KeyCode.RightArrow) ? 1 : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0), Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.DownArrow) ? -1 : 0)).normalized;
    rb.velocity = movement * moveSpeed;

    // Flip player sprite based on direction
    if (Input.GetKey(KeyCode.RightArrow))
    {
        transform.localScale = new Vector3(0.055f, 0.055f, 1);
    }
    else if (Input.GetKey(KeyCode.LeftArrow))
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
    if (collision.gameObject.CompareTag("Bomb1"))
    {
        deathSound.Play();
        Destroy(gameObject);
        Destroy(collision.gameObject);
        StartCoroutine(ReloadSceneAfterDelay(1f));
    }
}
private IEnumerator ReloadSceneAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Restart the game
}
}