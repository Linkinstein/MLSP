using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfVisionCone : MonoBehaviour
{
    public Material VisionConeMaterial;
    public float VisionRange = 8;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer; // Layer with objects that obstruct the view
    public int VisionConeResolution = 10; // Higher values make the cone prettier
    private Mesh VisionConeMesh;
    private MeshFilter MeshFilter_;

    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle = 0.75f; // Convert angle to radians
    }

    void Update()
    {
        DrawVisionCone(); // Update the vision cone every frame
    }

    void DrawVisionCone()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] vertices = new Vector3[VisionConeResolution + 1];
        vertices[0] = Vector3.zero; // Cone tip

        float currentAngle = -VisionAngle / 2;
        float angleIncrement = VisionAngle / (VisionConeResolution - 1);
        float sine, cosine;

        // Get the direction based on the parent's scale
        float directionMultiplier = transform.parent.localScale.x < 0 ? -1 : 1;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            sine = Mathf.Sin(currentAngle);
            cosine = Mathf.Cos(currentAngle);
            Vector3 raycastDirection = new Vector3(directionMultiplier, sine, 0); // Pointing right/left based on parent's scale
            Vector3 vertForward = new Vector3(1, sine, 0); // Cone vertex

            // Cast the ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, VisionRange, VisionObstructingLayer);
            if (hit)
            {
                vertices[i + 1] = vertForward * hit.distance; // Adjust vertex position to hit distance
            }
            else
            {
                vertices[i + 1] = vertForward * VisionRange; // Max range if no hit
            }

            currentAngle += angleIncrement;
        }

        // Create triangles
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0; // First vertex (tip of the cone)
            triangles[i + 1] = j + 1; // Current vertex
            triangles[i + 2] = j + 2; // Next vertex
        }

        VisionConeMesh.Clear();
        VisionConeMesh.vertices = vertices;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;
    }
}

