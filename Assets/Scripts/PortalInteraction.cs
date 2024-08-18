using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalInteraction : MonoBehaviour
{
    public Text portalText; // UI text that appears when near the portal
    public GameObject player; // Reference to the player object
    public GameObject particleEffectPrefab; // Your particle system prefab
    public string sceneToLoad; // Name of the scene to load

    void Start()
    {
        if (portalText != null)
            portalText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            ShowPortalText(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            ShowPortalText(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && portalText.gameObject.activeSelf)
        {
            StartCoroutine(DisappearAndLoadScene());
        }
    }

    IEnumerator DisappearAndLoadScene()
    {
        // Instantiate the particle effect at the player's position and destroy the player
        Instantiate(particleEffectPrefab, player.transform.position, Quaternion.identity);

        player.SetActive(false);

        // Wait for the particle effect to finish
        yield return new WaitForSeconds(2); // Adjust this time to match your particle effect's duration

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }

    void ShowPortalText(bool show)
    {
        if (portalText != null)
            portalText.gameObject.SetActive(show);
    }
}
