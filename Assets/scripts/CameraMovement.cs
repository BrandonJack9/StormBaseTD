using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class CameraMovement : MonoBehaviour
{
    public GameObject CameraPointN, CameraPointW, CameraPointS, CameraPointE;
    public Transform currentPoint;

    
    public Transform target;

    public float speed = 1.0f;

    private float distanceLeft, distanceRight;
    private bool canSwitch = true;
    void Start()
    {

        transform.forward = CameraPointN.transform.forward;
        distanceLeft = Vector3.Distance(transform.position, CameraPointE.transform.position);
        distanceRight = Vector3.Distance(transform.position, CameraPointW.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);

        if (currentPoint == CameraPointN.transform && canSwitch)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                //transform.position = Vector3.MoveTowards(transform.position, CameraPointW.transform.position, distanceRight * (Time.deltaTime * speed));
                transform.position = CameraPointW.transform.position;
                //transform.forward = CameraPointW.transform.forward;
                currentPoint = CameraPointW.transform;
                canSwitch = false;
                StartCoroutine(switchCoolDown());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                //transform.position = Vector3.MoveTowards(transform.position, CameraPointW.transform.position, distanceRight * (Time.deltaTime * speed));
                transform.position = CameraPointE.transform.position;
                //transform.forward = CameraPointE.transform.forward;
                currentPoint = CameraPointE.transform;
                canSwitch = false;
                StartCoroutine(switchCoolDown());
            }
        }

        if (currentPoint == CameraPointS.transform && canSwitch)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                //transform.position = Vector3.MoveTowards(transform.position, CameraPointW.transform.position, distanceRight * (Time.deltaTime * speed));
                transform.position = CameraPointE.transform.position;
                //transform.forward = CameraPointW.transform.forward;
                currentPoint = CameraPointE.transform;
                canSwitch = false;
                StartCoroutine(switchCoolDown());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                //transform.position = Vector3.MoveTowards(transform.position, CameraPointW.transform.position, distanceRight * (Time.deltaTime * speed));
                transform.position = CameraPointW.transform.position;
                //transform.forward = CameraPointE.transform.forward;
                currentPoint = CameraPointW.transform;
                canSwitch = false;
                StartCoroutine(switchCoolDown());
            }
        }

        if (currentPoint == CameraPointE.transform && canSwitch)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                //transform.position = Vector3.MoveTowards(transform.position, CameraPointW.transform.position, distanceRight * (Time.deltaTime * speed));
                transform.position = CameraPointN.transform.position;
                transform.forward = CameraPointN.transform.forward;
                currentPoint = CameraPointN.transform;
                canSwitch = false;
                StartCoroutine(switchCoolDown());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                //transform.position = Vector3.MoveTowards(transform.position, CameraPointW.transform.position, distanceRight * (Time.deltaTime * speed));
                transform.position = CameraPointS.transform.position;
                transform.forward = CameraPointS.transform.forward;
                currentPoint = CameraPointS.transform;
                canSwitch = false;
                StartCoroutine(switchCoolDown());
            }
        }

        if (currentPoint == CameraPointW.transform && canSwitch)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                //transform.position = Vector3.MoveTowards(transform.position, CameraPointW.transform.position, distanceRight * (Time.deltaTime * speed));
                transform.position = CameraPointS.transform.position;
                transform.forward = CameraPointS.transform.forward;
                currentPoint = CameraPointS.transform;
                canSwitch = false;
                StartCoroutine(switchCoolDown());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                //transform.position = Vector3.MoveTowards(transform.position, CameraPointW.transform.position, distanceRight * (Time.deltaTime * speed));
                transform.position = CameraPointN.transform.position;
                transform.forward = CameraPointN.transform.forward;
                currentPoint = CameraPointN.transform;
                canSwitch = false;
                StartCoroutine(switchCoolDown());
            }
        }
    }

    IEnumerator switchCoolDown()
    {
        yield return new WaitForSeconds(2);
        canSwitch = true;
    }
}