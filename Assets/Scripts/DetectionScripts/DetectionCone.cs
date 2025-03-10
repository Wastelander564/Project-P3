using UnityEngine;
using System.Collections.Generic;

public class VisionCone : MonoBehaviour
{
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

        // Voeg MeshRenderer toe en zorg dat het materiaal zichtbaar is
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            renderer = gameObject.AddComponent<MeshRenderer>();
        }
        renderer.material = new Material(Shader.Find("Sprites/Default"));
        renderer.material.color = new Color(1f, 1f, 1f, 0.1f); // Halfdoorzichtig

        col = GetComponent<MeshCollider>();

        UpdateMesh(); // Roep deze functie aan om de mesh te maken
        player = GameObject.FindWithTag("Player");
    }

    void UpdateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Voeg het middelpunt van de kegel toe
        vertices.Add(Vector3.zero);

        // Maak de punten aan de rand van de kegel op basis van de hoek en afstand
        for (int i = 0; i <= segments; i++)
        {
            float angle = -viewAngle / 2 + (viewAngle / segments) * i;
            Vector3 point = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle) * viewDistance, Mathf.Cos(Mathf.Deg2Rad * angle) * viewDistance, 0f);  // Zorg voor 2D-zichtkegel
            vertices.Add(point);
        }

        // Maak de driehoeken die de mesh verbinden
        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triangles.Add(0);  // Middelpunt
            triangles.Add(i);  // Punt 1
            triangles.Add(i + 1);  // Punt 2
        }

        // Werk de mesh bij
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals(); // Herbereken de normale vectoren voor de mesh
    }

    // Als de speler de zichtkegel binnenkomt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Detector>().isSeen = true;
        }
    }

    // Als de speler de zichtkegel verlaat
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Detector>().isSeen = false;
        }
    }
}
