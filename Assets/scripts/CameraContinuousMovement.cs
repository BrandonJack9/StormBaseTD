using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

[ExecuteInEditMode]
public class CameraContinuousMovement : MonoBehaviour
{
    public Transform orbitCenter;
    public float orbitRadius;
    public float orbitHeight;
    public float orbitSpeed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            transform.position = orbitCenter.position + new Vector3(0, orbitHeight, -orbitRadius);
            transform.LookAt(orbitCenter);
        } else
        {
            transform.position = Quaternion.AngleAxis(orbitSpeed * (-Input.GetAxis("Horizontal")) * Time.deltaTime, orbitCenter.up) * transform.position;
            transform.LookAt(orbitCenter);
        }

    }
}