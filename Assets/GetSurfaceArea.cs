using UnityEngine;

public class GetSurfaceArea
{
    public static float CalculateSurface(GameObject Gobj)
    {
        MeshFilter meshFilter = Gobj.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        float surfaceArea = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for(int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            surfaceArea += CalculateTriangleArea(v0, v1, v2);
        }

        return surfaceArea;
    }

    static float CalculateTriangleArea(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        Vector3 side1 = v1 - v0;
        Vector3 side2 = v2 - v1;
        return (float)(Vector3.Cross(side1, side2).magnitude * 0.5);
    }
}
