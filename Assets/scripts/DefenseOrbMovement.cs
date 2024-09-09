using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefenseOrbMovement : MonoBehaviour
{
    Camera cam ;
    Collider planeCollider;
    Collider domeCollider;
    RaycastHit hit;
    Ray ray;

    public Rigidbody rb;
    public float orbSpeed;
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        planeCollider = GameObject.Find("ground").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        //transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point + new Vector3(0, hit.point.y + 20, 0), Time.deltaTime * orbSpeed);
        }

        
        if(Physics.Raycast(ray, out hit) && !Input.GetMouseButton(0))
        {
            if (hit.collider == planeCollider)
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * orbSpeed);
            }
        }
    }
}
