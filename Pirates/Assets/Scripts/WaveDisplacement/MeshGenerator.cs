using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    [Header("Mesh Settings")]
    [SerializeField] int meshWidth = 10;
    [SerializeField] int meshLength = 10;

    [Header("Graphics")]
    [SerializeField] Material material;

    [Header("Ocean Controller")]
    [SerializeField] OceanController controller;

    GerstnerWave[] waves;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;


        CreateMeshShape();
        UpdateMesh();
    }

    void Update()
    {
        waves = controller.waves;
        UpdateVertexPosition();
    }

    void CreateMeshShape()
    {
        int vertexCount = meshWidth * meshLength;
        vertices = new Vector3[vertexCount];
        uvs = new Vector2[vertexCount];

        int triangleCount = (meshWidth - 1) * (meshLength - 1) * 6;
        triangles = new int[triangleCount];

        int v = 0;
        for (int z = 0; z < meshLength; z++)
        {
            for (int x = 0; x < meshWidth; x++)
            {
                float y = 0f;
                vertices[v] = new Vector3(x, y, z);
                uvs[v] = new Vector2(
                    (float)x / (meshWidth - 1),
                    (float)z / (meshLength - 1)
                );
                v++;
            }
        }

        int t = 0;
        int vertIndex = 0;
        for (int z = 0; z < meshLength - 1; z++)
        {
            for (int x = 0; x < meshWidth - 1; x++)
            {
                int bottomLeft = vertIndex;
                int bottomRight = vertIndex + 1;
                int topLeft = vertIndex + meshWidth;
                int topRight = vertIndex + meshWidth + 1;

                triangles[t + 0] = bottomLeft;
                triangles[t + 1] = topLeft;
                triangles[t + 2] = bottomRight;

                triangles[t + 3] = bottomRight;
                triangles[t + 4] = topLeft;
                triangles[t + 5] = topRight;

                t += 6;
                vertIndex++;
            }
            vertIndex++;
        }
    }

    void UpdateVertexPosition()
    {
        int v = 0;
        for (int z = 0; z < meshLength; z++)
        {
            for (int x = 0; x < meshWidth; x++)
            {
                vertices[v] = GerstnerDisplacement.getDisplacement(x, z, waves);
                v++;
            }
        }

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }
}