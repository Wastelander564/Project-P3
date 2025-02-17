using UnityEngine;
using System.Collections.Generic;

public class VisionCone : MonoBehaviour
{
//Dit scr
    public float viewAngle = 30f;  // Hoek van de zichtkegel
    public float viewDistance = 1f; // Hoe ver de vijand kan zien
    public int segments = 10;  // Hoe gedetailleerd de mesh is

    private Mesh mesh;
    private MeshCollider col;
    private GameObject player;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        UpdateMesh();

        col = GetComponent<MeshCollider>();

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Unlit/Color"));
        renderer.material.color = new Color(1f, 1f, 1f, 0.5f);
        player = GameObject.FindWithTag("Player");
    }

    void UpdateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices.Add(Vector3.zero); // Middelpunt van de kegel

        for (int i = 0; i <= segments; i++)
        {
            float angle = -viewAngle / 2 + (viewAngle / segments) * i;
            Vector3 point = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle)) * viewDistance;
            vertices.Add(point);
        }

        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Detector>().isSeen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Detector>().isSeen = false;
        }
    }
}
