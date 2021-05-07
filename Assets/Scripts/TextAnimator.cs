using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TextAnimator : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float minOutlineThickness;

    [SerializeField]
    [Range(0, 1)]
    private float maxOutlineThickness;

    [SerializeField]
    private float minFontSize;

    [SerializeField]
    private float maxFontSize;

    [SerializeField]
    private float minPosition;

    [SerializeField]
    private float maxPosition;

    [SerializeField]
    private float animationDelay;

    private TextMeshPro textMesh;
    private WaitForSeconds waitTime;
    private Vector3 startPosition;

    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        waitTime = new WaitForSeconds(animationDelay);
        startPosition = gameObject.transform.position;
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText() {

        while (true)
        {
            yield return waitTime;
            textMesh.outlineWidth = Random.Range(minOutlineThickness, maxOutlineThickness);
            textMesh.fontSize = Random.Range(minFontSize, maxFontSize);
            gameObject.transform.position = new Vector3(
                startPosition.x + Mathf.Sin(Random.Range(minPosition, maxPosition)),
                startPosition.y + Mathf.Sin(Random.Range(minPosition, maxPosition)),
                startPosition.z
            );
        }
    }
}
