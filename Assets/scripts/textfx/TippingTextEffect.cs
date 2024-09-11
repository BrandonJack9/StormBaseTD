using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TippingTextEffect : MonoBehaviour
{
    public float maxRotationAngle = 10f;   // Maximum angle of the tip (in degrees)
    public float snapSpeed = 5f;           // Speed of snapping between angles
    public float holdTime = 0.1f;          // How long to hold at the max rotation

    private bool tippingLeft = true;       // State to track direction of tipping
    private Quaternion leftRotation;       // Rotation to the left
    private Quaternion rightRotation;      // Rotation to the right
    private Quaternion originalRotation;   // Original rotation of the object
    private bool isSnapping = false;

    void Start()
    {
        // Store the original rotation of the object
        originalRotation = transform.rotation;

        // Define the left and right rotation angles
        leftRotation = Quaternion.Euler(0, 0, maxRotationAngle);   // Rotate to the left
        rightRotation = Quaternion.Euler(0, 0, -maxRotationAngle); // Rotate to the right

        // Start the tipping coroutine
        StartCoroutine(TipText());
    }

    IEnumerator TipText()
    {
        while (true)
        {
            // Toggle between left and right rotations
            if (tippingLeft)
            {
                yield return SnapToRotation(leftRotation);
            }
            else
            {
                yield return SnapToRotation(rightRotation);
            }

            // Wait at the max angle for a brief moment before switching
            yield return new WaitForSeconds(holdTime);

            // Switch tipping direction
            tippingLeft = !tippingLeft;
        }
    }

    IEnumerator SnapToRotation(Quaternion targetRotation)
    {
        isSnapping = true;

        // Lerp (smoothly transition) the rotation towards the target
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * snapSpeed);
            yield return null;  // Wait until the next frame
        }

        // Ensure the final rotation is exactly the target rotation
        transform.rotation = targetRotation;
        isSnapping = false;
    }
}


////pop in and out effect/////////

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PopTextEffect : MonoBehaviour
// {
//     public float minScale = 0.9f;  // Minimum scale (how small it gets)
//     public float maxScale = 1.1f;  // Maximum scale (how large it gets)
//     public float speed = 5f;       // Speed of the pop animation

//     private bool growing = true;
//     private Vector3 targetScale;
//     private Vector3 originalScale;

//     void Start()
//     {
//         // Ensure the rotation is reset to avoid any angles
//         transform.rotation = Quaternion.identity;

//         // Store the original scale of the object
//         originalScale = transform.localScale;
//         targetScale = originalScale * maxScale; // Initially, target the larger scale
//     }

//     void Update()
//     {
//         // Scale towards the target scale uniformly
//         transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);

//         // Check if we've reached the target scale (with a small tolerance)
//         if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
//         {
//             // If we were growing, start shrinking, and vice versa
//             if (growing)
//             {
//                 targetScale = originalScale * minScale;  // Shrink
//             }
//             else
//             {
//                 targetScale = originalScale * maxScale;  // Grow
//             }

//             // Toggle the growing state
//             growing = !growing;
//         }
//     }
// }



