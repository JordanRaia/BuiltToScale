using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Text collectibleCountText;
    [SerializeField] private Text scaleText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCollectibleCount(int count)
    {
        collectibleCountText.text = "Crystals: " + count.ToString();
    }

    public void UpdateScaleText(float scale)
    {
        scaleText.text = "Scale: " + scale.ToString();
    }
}
