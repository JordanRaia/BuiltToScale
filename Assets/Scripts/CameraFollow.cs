using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public float cameraDistance = 5f; // Default distance of the camera from the player
    public float cameraHeight = 1f;   // Default height of the camera above the player
    public Camera cam;

    private float initialOrthographicSize;

    void Start()
    {
        // Store the initial orthographic size of the camera
        initialOrthographicSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the current orthographic size based on the player's scale
        float playerScale = player.localScale.x;
        cam.orthographicSize = initialOrthographicSize * (1 + 0.5f * (playerScale - 1));

        // Adjust the camera's position to follow the player
        Vector3 offset = new Vector3(0, cameraHeight, -cameraDistance);
        transform.position = player.position + offset;
    }
}