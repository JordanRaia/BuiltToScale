using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSizeController : MonoBehaviour
{
    public float scaleMultiplierIncrease = 2.0f;
    public float scaleMultiplierDecrease = 0.5f;
    public float maxScale = 6.0f;
    public float minScale = 0.25f;
    public float scaleSpeed = 2.0f; // speed of scaling transition
    public float initialSize = 1.0f;

    private bool canInteract = false;
    private string interactionTag = "";

    private Vector3 targetScale;
    private bool isScaling = false;

    public int maxCollectibles = 25;
    private int currentCollectibles = 0;

    void Start()
    {
        transform.localScale = new Vector3(initialSize, initialSize, initialSize);
        targetScale = transform.localScale;
        UpdateUIScale(parseVectorString(targetScale.ToString()));
    }

    private string parseVectorString(string str)
    {
        // remove characters in vector string
        var charsToRemove = new string[] { "(", ")", "," };
        foreach (var c in charsToRemove)
        {
            str = str.Replace(c, string.Empty);
        }

        //remove everything except first number
        int index = str.IndexOf(" ");
        if (index >= 0)
        {
            str = str.Substring(0, index);
        }

        return str;
    }

    void Update()
    {
        // Handle scaling input via UI buttons instead of keyboard
        if (isScaling)
        {
            UpdateUIScale(parseVectorString(targetScale.ToString()));
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
                isScaling = false;

                char[] parseChars = { '(', ',' };
                string targetScaleParsed = targetScale.ToString().Trim(parseChars);
            }
        }
    }

    public void OnIncreaseSizePressed()
    {
        if (currentCollectibles > 0 && transform.localScale.x * scaleMultiplierIncrease <= maxScale && !isScaling)
        {
            SetTargetSize(transform.localScale.x * scaleMultiplierIncrease);
            currentCollectibles--;
            UpdateUI();
        }
    }

    public void OnDecreaseSizePressed()
    {
        if (currentCollectibles > 0 && transform.localScale.x * scaleMultiplierDecrease >= minScale && !isScaling)
        {
            SetTargetSize(transform.localScale.x * scaleMultiplierDecrease);
            currentCollectibles--;
            UpdateUI();
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
    }

    private void UpdateUIScale(string scale)
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateScaleText(scale);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IncreaseSize") || collision.CompareTag("DecreaseSize"))
        {
            canInteract = true;
            interactionTag = collision.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IncreaseSize") || collision.CompareTag("DecreaseSize"))
        {
            canInteract = false;
            interactionTag = "";
        }
    }

    private void SetTargetSize(float newSize)
    {
        newSize = Mathf.Clamp(newSize, minScale, maxScale);
        targetScale = new Vector3(newSize, newSize, newSize);
        isScaling = true;
    }

    internal float GetCurrentScale()
    {
        return transform.localScale.x;
    }
}
