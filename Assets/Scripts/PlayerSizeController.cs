using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeController : MonoBehaviour
{
    //variables to control the scale
    public float scaleMultiplierIncrease = 2.0f;
    public float scaleMultiplierDecrease = 0.5f;
    public float maxScale = 6.0f;
    public float minScale = 0.25f;
    public float scaleSpeed = 2.0f; // speed of scaling transition

    private bool canInteract = false;
    private string interactionTag = "";

    private Vector3 targetScale;
    private bool isScaling = false;

    public int maxCollectibles = 99;
    private int currentCollectibles = 0;

    void Start()
    {
        targetScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCollectibles > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.localScale.x * scaleMultiplierIncrease <= maxScale)
            {
                SetTargetSize(transform.localScale.x * scaleMultiplierIncrease);
                currentCollectibles--;
                UpdateUI();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.localScale.x * scaleMultiplierDecrease >= minScale)
            {
                SetTargetSize(transform.localScale.x * scaleMultiplierDecrease);
                currentCollectibles--;
                UpdateUI();
            }
        }
        // Check for the interaction button press when within range


        // Smoothly interpolate the scale towards the target scale
        if (isScaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

            // Stop scaling when close enough to the target scale
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
                isScaling = false;
            }
        }
    }

    public void AddCollectible()
    {
        if (currentCollectibles < maxCollectibles)
        {
            currentCollectibles++;
            UpdateUI();
        }
        else
        {
            Debug.Log("Max collectibles reached: " + maxCollectibles);
        }
    }

    private void UpdateUI()
    {
        // Update the UI to show the number of collectibles
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateCollectibleCount(currentCollectibles);
        }
        else
        {
            Debug.LogError("UIManager instance not found.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player enters the trigger, allow interaction and store the tag
        if (collision.CompareTag("IncreaseSize") || collision.CompareTag("DecreaseSize"))
        {
            canInteract = true;
            interactionTag = collision.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When the player exits the trigger, disable interaction
        if (collision.CompareTag("IncreaseSize") || collision.CompareTag("DecreaseSize"))
        {
            canInteract = false;
            interactionTag = "";
        }
    }

    private void SetTargetSize(float newSize)
    {
        // Clamp the size to the defined min and max values
        newSize = Mathf.Clamp(newSize, minScale, maxScale);
        targetScale = new Vector3(newSize, newSize, newSize);
        isScaling = true;
    }

    private void IncreaseSize()
    {
        float newScale = transform.localScale.x * scaleMultiplierIncrease;

        if (newScale > maxScale)
        {
            newScale = maxScale;
        }

        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void DecreaseSize()
    {
        float newScale = transform.localScale.x * scaleMultiplierDecrease;

        if (newScale < minScale)
        {
            newScale = minScale;
        }

        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}