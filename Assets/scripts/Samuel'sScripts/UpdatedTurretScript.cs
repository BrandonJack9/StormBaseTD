using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class UpdatedTurretScript : MonoBehaviour
{
    public Transform gunBarrel, aimLookAt, barrelPoint, cameraPoint;
    public GameObject lazerPrefab;
    public GameObject Turret;
    public GameObject PyramidBase; // The object to heal for life steal
    public LayerMask layersToHit;
    public float lazerSpeed, fireRate, lazerLifeSpan;
    public float lifestealLevel,enemyControlLevel,ShieldLevel,autoTurretLevel;
    bool canFire = true;
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
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, layersToHit))
        {
            barrelPoint.transform.LookAt(hit.point);
            gunBarrel.transform.LookAt(hit.point);
            Debug.DrawRay(barrelPoint.transform.position, barrelPoint.forward * 500, Color.red);

            if (canFire)
            {
                currentLazer = Instantiate(lazerPrefab);
                currentLazer.transform.position = barrelPoint.transform.position;
                currentLazer.transform.up = barrelPoint.transform.forward;
                currentLazer.GetComponent<Rigidbody>().AddForce(currentLazer.transform.up * lazerSpeed, 0);
                StartCoroutine(fireCoolDown(fireRate));
                Debug.Log("LifeSteal Obtained: "+lifestealLevel);
                if (currentLazer != null)
        {
            //setting up life steal stacks
            currentLazer.GetComponent<LifeStealProjectile>().parentTurret = Turret;
            currentLazer.GetComponent<LifeStealProjectile>().level = lifestealLevel;
            currentLazer.GetComponent<LifeStealProjectile>().healTarget = PyramidBase; // Pass the heal target to the laser
        }
            }
        }
        else
        {
            barrelPoint.transform.LookAt(aimLookAt);
            gunBarrel.transform.LookAt(aimLookAt);
        }
        

    }
    
  
    IEnumerator fireCoolDown(float rate)
    {
        canFire = false;
        yield return new WaitForSeconds(rate);
        canFire = true;
    }

}
