using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateBackground : MonoBehaviour
{
    public float rotationSpeed = 30f;  // Speed of rotation in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the RectTransform on the Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
