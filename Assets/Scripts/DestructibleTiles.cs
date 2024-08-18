using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTiles : MonoBehaviour
{
    [SerializeField] private Tilemap destructibleTilemap;
    [SerializeField] private TileBase crackedTile;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D playerRb;

    [SerializeField] private PlayerSizeController playerSizeController;
    [SerializeField] private float requiredScaleToBreak = 2.0f; // Scale required to break tiles
    [SerializeField] private GameObject particleEffectPrefab; // Reference to the particle system prefab

    private bool wasInTheAir = false; // Track if the player was in the air on the last frame
    private float fallVelocityThreshold = -3f; // Velocity threshold to consider it a fall

    // Dictionary to track the number of times a tile has been stepped on
    private Dictionary<Vector3Int, int> tileInteractions = new Dictionary<Vector3Int, int>();

    private void Start()
    {
        playerRb = groundCheck.GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bool isGrounded = IsGrounded();
        if (isGrounded && wasInTheAir && playerRb.velocity.y <= fallVelocityThreshold)
        {
            CheckAndCrackTilesUnderPlayer();
        }
        wasInTheAir = !isGrounded;
    }

    private void TriggerParticleEffect(Vector3Int tilePosition)
    {
        Vector3 worldPosition = destructibleTilemap.CellToWorld(tilePosition) + new Vector3(0.5f, 0.5f, 0); // Center the effect
        Instantiate(particleEffectPrefab, worldPosition, Quaternion.identity);
    }

    private void CheckAndCrackTilesUnderPlayer()
    {
        float currentScale = playerSizeController.GetCurrentScale(); // Get current scale

        Vector3[] pointsToCheck = {
            groundCheck.position,
            new Vector3(groundCheck.position.x - 0.5f, groundCheck.position.y, groundCheck.position.z),
            new Vector3(groundCheck.position.x + 0.5f, groundCheck.position.y, groundCheck.position.z),
            new Vector3(groundCheck.position.x - 0.5f, groundCheck.position.y - 0.5f, groundCheck.position.z),
            new Vector3(groundCheck.position.x + 0.5f, groundCheck.position.y - 0.5f, groundCheck.position.z)
        };

        foreach (var point in pointsToCheck)
        {
            Vector3Int tilePosition = destructibleTilemap.WorldToCell(point);
            if (destructibleTilemap.HasTile(tilePosition) && currentScale >= requiredScaleToBreak)
            {
                if (!tileInteractions.ContainsKey(tilePosition))
                {
                    tileInteractions[tilePosition] = 0;
                }

                tileInteractions[tilePosition]++;

                if (tileInteractions[tilePosition] == 1)
                {
                    destructibleTilemap.SetTile(tilePosition, crackedTile);
                }
                else if (tileInteractions[tilePosition] == 2)
                {
                    destructibleTilemap.SetTile(tilePosition, null); // Remove the tile after the second interaction
                    TriggerParticleEffect(tilePosition); // Trigger particle effects at the tile's position
                }
            }
        }
    }

    private bool IsGrounded()
    {
        Collider2D groundCheckCollider = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return groundCheckCollider != null && (groundLayer.value & (1 << groundCheckCollider.gameObject.layer)) != 0;
    }
}
