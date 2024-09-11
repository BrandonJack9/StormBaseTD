using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class LazerAim : MonoBehaviour
{
    public Transform gunBarrel, aimLookAt, barrelPoint, cameraPoint;
    public GameObject lazerPrefab;
    public float lazerSpeed = 0.5f;

    public float lazerDelay = 5;
    private float lastUsedTime;
    public LayerMask layersToHit;
    //public float projectileLifeTime, shootForce;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cameraPoint.transform.position, cameraPoint.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 500, layersToHit))
        {
            HandeLazerFire(hit);
        }
       
        
        
    }

    private void HandeLazerFire(RaycastHit hit){
        GameObject hitObject = hit.collider.gameObject;
            if (hitObject != null){
                 if (lastUsedTime + lazerDelay < Time.time){
                    GameObject currentLazer = Instantiate(lazerPrefab);
                    currentLazer.transform.position = barrelPoint.transform.position;
                    currentLazer.transform.up = barrelPoint.transform.forward;
                    while(currentLazer.transform.position != hitObject.transform.position){
                        currentLazer.transform.position = Vector3.MoveTowards(currentLazer.transform.position, hitObject.transform.position, lazerSpeed);
                    }
                    lastUsedTime = Time.time;
                 }
                barrelPoint.transform.LookAt(hit.point);
                gunBarrel.transform.LookAt(hit.point);
                Debug.DrawRay(barrelPoint.transform.position, barrelPoint.forward * 500, Color.red);
            }
    }
}
