using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveTextEffect : MonoBehaviour
{
    public TMP_Text textComponent;   // Reference to the TextMeshPro component
    public float waveSpeed = 2f;     // Speed of the wave animation
    public float waveHeight = 5f;    // Height of the wave effect

    private TMP_TextInfo textInfo;
    private Vector3[] originalVertices;

    void Start()
    {
        // Automatically assign the TMP_Text component if not manually assigned
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }

        // Check if textComponent was found or assigned
        if (textComponent == null)
        {
            Debug.LogError("No TMP_Text component found on the GameObject!");
            return;
        }

        // Force an initial update of the text component so that the mesh data is available
        textComponent.ForceMeshUpdate();

        // Get the text info from TextMeshPro
        textInfo = textComponent.textInfo;

        // Ensure that there is at least one character and the mesh has been generated
        if (textInfo.characterCount == 0 || textInfo.meshInfo.Length == 0 || textInfo.meshInfo[0].vertices == null)
        {
            Debug.LogError("TextMeshPro vertices are not available! Make sure the text has been set.");
            return;
        }

        // Store the original vertex positions
        originalVertices = new Vector3[textInfo.meshInfo[0].vertices.Length];
        System.Array.Copy(textInfo.meshInfo[0].vertices, originalVertices, originalVertices.Length);
    }

    void Update()
    {
        AnimateText();
    }

    void AnimateText()
    {
        // Force the text component to update its internal structure
        textComponent.ForceMeshUpdate();

        // Get updated text info
        textInfo = textComponent.textInfo;

        // Loop through each character in the text
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            // Get the index of the material used by this character
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            // Get the index of the first vertex of this character
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            // Get the vertices of the character
            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            // Offset for the wave (each character moves with a phase shift)
            float offset = i * 0.1f;

            // Calculate the vertical movement using a sine wave with unscaled time
            float wave = Mathf.Sin(Time.unscaledTime * waveSpeed + offset) * waveHeight;

            // Apply the wave movement to each vertex of the character
            vertices[vertexIndex + 0] = originalVertices[vertexIndex + 0] + new Vector3(0, wave, 0);
            vertices[vertexIndex + 1] = originalVertices[vertexIndex + 1] + new Vector3(0, wave, 0);
            vertices[vertexIndex + 2] = originalVertices[vertexIndex + 2] + new Vector3(0, wave, 0);
            vertices[vertexIndex + 3] = originalVertices[vertexIndex + 3] + new Vector3(0, wave, 0);
        }

        // Update the mesh with the modified vertices
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
