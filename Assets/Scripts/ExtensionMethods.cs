using UnityEngine;

public static class ExtensionMethods
{
    public static int GetVisibleVerticeCount(this MeshFilter meshFilter, Camera camera)
    {
        int visibleVertices = meshFilter.mesh.vertexCount;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        foreach (Plane plane in planes)
        {
            foreach (Vector3 vertice in meshFilter.mesh.vertices)
            {
                if (plane.GetDistanceToPoint(meshFilter.transform.TransformPoint(vertice)) < 0)
                {
                    visibleVertices--;
                }
            }
        }

        return Mathf.Max(visibleVertices, 0);
    }
}
