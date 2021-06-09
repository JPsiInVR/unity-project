using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Billboard : MonoBehaviour
{
    private List<MeshFilter> meshFilters;
    private List<MeshRenderer> meshRenderers;
    private List<TextMeshPro> textMeshes;
    private int fullVertexCount;
    private int visibleVertexCount;
    private float vertexPercentageVisible;

    private void Start()
    {        
        meshFilters = new List<MeshFilter>(GetComponentsInChildren<MeshFilter>());
        meshRenderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
        textMeshes = new List<TextMeshPro>(GetComponentsInChildren<TextMeshPro>());

        foreach (TextMeshPro textMesh in textMeshes)
        {
            textMesh.ForceMeshUpdate();
        }

        foreach (MeshFilter meshFilter in meshFilters)
        {
            fullVertexCount += meshFilter.mesh.vertexCount;
        }
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        
        visibleVertexCount = 0;

        foreach(MeshFilter meshFilter in meshFilters)
        {
            visibleVertexCount += meshFilter.GetVisibleVerticeCount(Camera.main);
        }

        vertexPercentageVisible = (float)visibleVertexCount / fullVertexCount;

        SetAlphaForAllRenderers();
    }

    private void SetAlphaForAllRenderers()
    {
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.SetFloat("Alpha", 1 - ((1 - vertexPercentageVisible) * 4));
        }

        foreach (TextMeshPro textMesh in textMeshes)
        { 
            textMesh.faceColor = new Color(textMesh.faceColor.r, textMesh.faceColor.g, textMesh.faceColor.b, 1 - ((1 - vertexPercentageVisible) * 4));
        }
    }
}
