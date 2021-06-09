using UnityEngine;
using TMPro;

public class ButtonCallbacks : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();    
    }

    public void ForceTextUpdate()
    {
        text.UpdateFontAsset();
    }
}
