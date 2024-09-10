using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class CameraContinuousMovement : MonoBehaviour
{
    public Transform orbitCenter;
    public float orbitRadius;
    public float orbitHeight;
    public float orbitSpeed;

    void Start()
    {
        transform.position = orbitCenter.position + new Vector3(0, orbitHeight, -orbitRadius);
        transform.LookAt(orbitCenter);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Quaternion.AngleAxis(orbitSpeed * (-Input.GetAxis("Horizontal")) * Time.deltaTime, orbitCenter.up) * transform.position;
        transform.LookAt(orbitCenter);
    }

    void OnDrawGizmos()
    {
        if (orbitCenter != null)
        {
            var startPos = orbitCenter.position + new Vector3(0, orbitHeight, -orbitRadius);

            // Draw the start position
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(startPos, 1f);

            // Draw a line between the orbit center and the start position
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(orbitCenter.position, startPos);

        }
    }
}