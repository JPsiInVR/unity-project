using UnityEngine;
using TMPro;

public class ButtonCallbacks : MonoBehaviour
{
    [SerializeField]
    private bool rememberStateWhenDisabled = true;

    private TextMeshProUGUI text;
    private Animator animator;


    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();

        animator = GetComponent<Animator>();
        if(animator != null)
        {
            animator.keepAnimatorControllerStateOnDisable = rememberStateWhenDisabled;
        }
    }

    public void ForceTextUpdate()
    {
        text.UpdateFontAsset();
    }
}
