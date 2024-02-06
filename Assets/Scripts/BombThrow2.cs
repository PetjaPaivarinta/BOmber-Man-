using UnityEngine;

public class BombThrow2 : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject bombPrefab;

    private float throwCooldown = 1f; // Adjust the cooldown duration as needed
    private float throwTimer = 0f;
    public AudioSource throwSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   private void Update()
    {
        throwTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && throwTimer <= 0f)
        {
            throwSound.Play();
            ThrowBomb();
            throwTimer = throwCooldown;
        }
    }

    void ThrowBomb()
{
    // Instantiate the bomb at a position relative to the player
    Vector3 bombPosition = rb.transform.position + new Vector3(0, 1, 0); // Adjust the Vector3 values to position the bomb as needed
    GameObject bomb = Instantiate(bombPrefab, bombPosition, Quaternion.identity);

    Rigidbody2D bombRigidbody = bomb.GetComponent<Rigidbody2D>();

    bombRigidbody.isKinematic = false;

    // Determine the direction the player is facing
    float direction = transform.localScale.x > 0 ? 1 : -1;

    // Apply force in the direction the player is facing
    bombRigidbody.AddForce(new Vector2(direction * 500, 0)); // Adjust the force as needed
}
}