using UnityEngine;
using UnityEngine.Tilemaps;

public class BombScript : MonoBehaviour
{
    private Tilemap tilemap; // Reference to the Tilemap component

    public GameObject explosionEffect; // Reference to the explosion effect prefab

    private void Start() {
        // Get the Tilemap component from the GameObject
        tilemap = GameObject.FindObjectOfType<Tilemap>();

        // Destroy the bomb after 3 seconds
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.GetComponent<Tilemap>())
    {
        // Get the position of the collision in the grid
        Vector3 hitPosition = Vector3.zero;

        foreach (ContactPoint2D hit in collision.contacts)
        {
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
        }
        Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

        // Define the blast radius
        int blastRadius = 1;

        // Destroy the tiles in the blast radius
        for (int x = -blastRadius; x <= blastRadius; x++)
        {
            for (int y = -blastRadius; y <= blastRadius; y++)
            {
                Vector3Int position = new Vector3Int(cellPosition.x + x, cellPosition.y + y, cellPosition.z);
                tilemap.SetTile(position, null);
            }
        }

        // Instantiate the explosion effect at the bomb's position
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Destroy the bomb so it doesn't carry on and hit more things.
        Destroy(gameObject);
    }
}
}