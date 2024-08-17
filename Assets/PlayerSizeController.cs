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

    // Update is called once per frame
    void Update()
    {
        // Get input for increasing or decreasing size
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseSize();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseSize();
        }

        void IncreaseSize()
        {
            float newScale = transform.localScale.x * scaleMultiplierIncrease;

            // Clamp the scale to the maxScale
            if (newScale > maxScale)
            {
                newScale = maxScale;
            }

            // Apply the new scale uniformly to all axes
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }

        void DecreaseSize()
        {
            float newScale = transform.localScale.x * scaleMultiplierDecrease;

            // Clamp the scale to the minScale
            if (newScale < minScale)
            {
                newScale = minScale;
            }

            // Apply the new scale uniformly to all axes
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
