using UnityEngine;

public class SizeCollectible : MonoBehaviour
{
    // When the player collides with the collectible, increase their count and destroy the collectible
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerSizeController playerSizeController = collision.GetComponent<PlayerSizeController>();
            if (playerSizeController != null)
            {
                playerSizeController.AddCollectible();
                Destroy(gameObject);
            }
        }
    }
}
