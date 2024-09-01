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
    public Button interactButton; // Reference to the UI button for interaction

    private bool isPlayerInPortal = false; // Tracks if the player is in the portal trigger area

    void Start()
    {
        if (portalText != null)
            portalText.gameObject.SetActive(false);

        interactButton.onClick.AddListener(OnInteractButtonPressed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            ShowPortalText(true);
            isPlayerInPortal = true; // Set to true when the player enters the portal trigger area
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            ShowPortalText(false);
            isPlayerInPortal = false; // Set to false when the player leaves the portal trigger area
        }
    }

    void OnInteractButtonPressed()
    {
        if (isPlayerInPortal) // Check if the player is in the portal trigger area
        {
            StartCoroutine(DisappearAndLoadScene());
        }
    }

    IEnumerator DisappearAndLoadScene()
    {
        Instantiate(particleEffectPrefab, player.transform.position, Quaternion.identity);

        player.SetActive(false);

        yield return new WaitForSeconds(2); // Adjust this time to match your particle effect's duration

        SceneManager.LoadScene(sceneToLoad);
    }

    void ShowPortalText(bool show)
    {
        if (portalText != null)
            portalText.gameObject.SetActive(show);
    }
}
