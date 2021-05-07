using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Highlighter : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float minOutlineThickness;

    [SerializeField]
    [Range(0, 1)]
    private float maxOutlineThickness;

    [SerializeField]
    private Color outlineColor;

    [SerializeField]
    private float animationDelay;

    [SerializeField]
    private Animator animator;

    private WaitForSeconds waitTime;
    private new Renderer renderer;
    private const string materialPropertyName = "Size";
    private const string animatorParameterName = "IsHovering";
    private GameObject outline;
    private bool isHovering;

    private void Start()
    {
        waitTime = new WaitForSeconds(animationDelay);
        outline = gameObject.transform.GetChild(0).gameObject;
        renderer = outline.GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        isHovering = false;
    }

    public void Hovering(bool enable)
    {
        isHovering = enable;
        outline.SetActive(isHovering);
        animator.SetBool(animatorParameterName, enable);

        if (isHovering)
        {
            StartCoroutine(Highlight());
        }
    }

    public void Selecting(bool enable)
    {
        if(enable)
        {
            Debug.Log("Selected");
        }
        else
        {
            Debug.Log("Deselected");
        }
    }

    private IEnumerator Highlight()
    {
        while (isHovering)
        {
            yield return waitTime;

            if (renderer.material.HasFloat(materialPropertyName))
            {
                renderer.material.SetFloat(materialPropertyName, Random.Range(minOutlineThickness, maxOutlineThickness));
                renderer.material.color = outlineColor;
            }
        }
    }
}