using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TurretScript : MonoBehaviour
{
    public Transform gunBarrel, aimLookAt, barrelPoint, cameraPoint;
    public GameObject lazerPrefab;
    public LayerMask layersToHit;
    public float lazerSpeed, fireRate, lazerLifeSpan;
    bool canFire = true;
    public bool lifeStealObtained = false;
    public bool continuousBeamObtained = false;
    public int lifeStealStack = 0;
    public int beamStack = 0;
    bool once = true;
    bool twice = false;
    //public float projectileLifeTime, shootForce;
    // Start is called before the first frame update
    GameObject currentLazer;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cameraPoint.transform.position, cameraPoint.forward);
        Ray ray2 = new Ray(barrelPoint.transform.position, barrelPoint.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, layersToHit))
        {
            barrelPoint.transform.LookAt(hit.point);
            gunBarrel.transform.LookAt(hit.point);
            //Debug.DrawRay(barrelPoint.transform.position, barrelPoint.forward * 500, Color.red);

            if (canFire && !continuousBeamObtained)
            {
                currentLazer = Instantiate(lazerPrefab);
                currentLazer.transform.position = barrelPoint.transform.position;
                currentLazer.transform.up = barrelPoint.transform.forward;
                currentLazer.GetComponent<Rigidbody>().AddForce(currentLazer.transform.up * lazerSpeed, 0);
                StartCoroutine(fireCoolDown(fireRate));
            }
            
        }
        else
        {
            barrelPoint.transform.LookAt(aimLookAt);
            gunBarrel.transform.LookAt(aimLookAt);
        }
        
        //continuous beam functionality
        if (Physics.Raycast(ray2, out hit, 500, layersToHit))
        {
            float beamDistance = (hit.distance);
            Debug.DrawRay(barrelPoint.transform.position, barrelPoint.forward * beamDistance, Color.red);
            if (canFire && continuousBeamObtained)
            {
                if (once)
                {
                    continuousBeamInstance();
                    once = false;
                }
                    currentLazer.transform.position = barrelPoint.transform.position;
                    currentLazer.transform.up = barrelPoint.transform.forward;
                    currentLazer.transform.localScale = new Vector3(.1f * beamStack, beamDistance * .5f, .1f * beamStack); //this line tells the beam how long it should be
                    currentLazer.transform.Translate(0, currentLazer.transform.localScale.y, 0); //this line moves the beam forward so that its situated properly
                if(lifeStealObtained)
                {
                    currentLazer.GetComponent<LazerProjectileScript>().lifeStealParticlesContinuous.SetActive(true);
                }
            }
        }
        else
        {
            // currentLazer.SetActive(false);
            once = true;
        }

    }
    
  
    IEnumerator fireCoolDown(float rate)
    {
        canFire = false;
        yield return new WaitForSeconds(rate);
        canFire = true;
    }

    void continuousBeamInstance()
    {
        currentLazer = Instantiate(lazerPrefab);
        currentLazer.GetComponent<LazerProjectileScript>().projectileLifeTime = (120f);
    }
}
