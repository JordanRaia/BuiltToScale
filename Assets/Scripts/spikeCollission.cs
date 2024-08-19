using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpikeCollision : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public GameObject particleEffectPrefab; // Your particle system prefab

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Make sure the player GameObject has the tag "Player"
        {
            StartCoroutine(DisappearAndRestartScene());
        }
    }

    IEnumerator DisappearAndRestartScene()
    {
        // Instantiate the particle effect at the player's position and destroy the player
        Instantiate(particleEffectPrefab, player.transform.position, Quaternion.identity);

        player.SetActive(false);

        // Wait for the particle effect to finish
        yield return new WaitForSeconds(2); // Adjust this time to match your particle effect's duration

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
