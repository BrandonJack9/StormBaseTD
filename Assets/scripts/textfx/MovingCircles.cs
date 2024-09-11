using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCircles : MonoBehaviour
{
    public GameObject circlePrefab;       // Reference to the circle prefab
    public float spawnRate = 1f;          // Time between circle spawns
    public int numberOfCircles = 8;       // Number of circles to spawn per iteration
    public float speed = 100f;            // Speed at which circles move
    public Vector2 direction = new Vector2(-1, -1);  // Movement direction (down-left by default)
    public float spawnXRange = 800f;      // Range along the X-axis to randomly spawn circles
    public float lifeTime = 5f;           // Time before each circle is destroyed
    public float spawnHeight = 1200f;     // Height above the canvas where circles will spawn

    private RectTransform canvasRectTransform; // Reference to the canvas RectTransform

    void Start()
    {
        // Get the RectTransform of the parent Canvas
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        if (canvasRectTransform == null)
        {
            Debug.LogError("Canvas RectTransform not found!");
            return;
        }

        // Start spawning circles at intervals
        StartCoroutine(SpawnCircles());
    }

    IEnumerator SpawnCircles()
    {
        while (true)
        {
            // Spawn multiple circles
            for (int i = 0; i < numberOfCircles; i++)
            {
                // Spawn the circle higher above the top edge of the canvas
                Vector2 spawnPosition = new Vector2(
                    Random.Range(-spawnXRange, spawnXRange), 
                    canvasRectTransform.rect.height / 2 + spawnHeight // Use spawnHeight to control how high above the canvas the circles spawn
                );
                GameObject newCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity, transform);

                // Tag the newly created circle
                newCircle.tag = "Circle";

                // Set the lifetime of the circle
                Destroy(newCircle, lifeTime);
            }

            yield return new WaitForSeconds(spawnRate); // Wait before spawning the next set of circles
        }
    }

    void Update()
    {
        // Move each circle down at an angle every frame
        foreach (Transform child in transform)
        {
            // Check if the child has the tag "Circle"
            if (child.CompareTag("Circle"))
            {
                // Move the circle based on the direction and speed
                child.Translate(direction.normalized * speed * Time.deltaTime);
            }
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MovingCircles : MonoBehaviour
// {
//     public GameObject circlePrefab;       // Reference to the circle prefab
//     public float spawnRate = 1f;          // Time between circle spawns
//     public float speed = 100f;            // Speed at which circles move
//     public Vector2 direction = new Vector2(-1, -1);  // Movement direction (down-left by default)
//     public float spawnXRange = 800f;      // Range along the X-axis to randomly spawn circles
//     public float lifeTime = 5f;           // Time before each circle is destroyed

//     private RectTransform canvasRectTransform; // Reference to the canvas RectTransform

//     void Start()
//     {
//         // Get the RectTransform of the parent Canvas
//         canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

//         if (canvasRectTransform == null)
//         {
//             Debug.LogError("Canvas RectTransform not found!");
//             return;
//         }

//         // Start spawning circles at intervals
//         StartCoroutine(SpawnCircles());
//     }

//     IEnumerator SpawnCircles()
//     {
//         while (true)
//         {
//             // Spawn a new circle at random X position within the range
//             Vector2 spawnPosition = new Vector2(Random.Range(-spawnXRange, spawnXRange), canvasRectTransform.rect.height / 2);
//             GameObject newCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity, transform);

//             // Tag the newly created circle (make sure the prefab has the same tag in case you skip this line)
//             newCircle.tag = "Circle";

//             // Set the lifetime of the circle
//             Destroy(newCircle, lifeTime);

//             yield return new WaitForSeconds(spawnRate); // Wait before spawning the next circle
//         }
//     }

//     void Update()
//     {
//         // Move each circle down at an angle every frame
//         foreach (Transform child in transform)
//         {
//             // Check if the child has the tag "Circle"
//             if (child.CompareTag("Circle"))
//             {
//                 // Move the circle based on the direction and speed
//                 child.Translate(direction.normalized * speed * Time.deltaTime);
//             }
//         }
//     }
// }
