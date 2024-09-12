using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurretProjectile : MonoBehaviour
{
    // renders line that acts as a laser
    public LineRenderer lineRenderer;
    public float laserDuration = 1.0f;

    // Fires the laser at a projectile
    public void Fire(Vector3 turret, Vector3 target)
    {
        Vector3 spaceToTarget = target - turret;
        float dist = spaceToTarget.magnitude;
        Vector3 dirToTarget = spaceToTarget / dist;

        Ray theRay = new Ray(turret, dirToTarget);
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, turret);
        Debug.Log("FIRE!!!");
        // if the ray hits an enemy projectile
        if (Physics.Raycast(theRay, out RaycastHit hit))
        {
            if (hit.collider.gameObject != null)
            {
                if (hit.collider.gameObject.CompareTag("meteor"))
                {
                    lineRenderer.SetPosition(1, hit.collider.gameObject.transform.position);
                    Debug.Log("The line renderers endpoint is: " + hit.collider.gameObject.transform.position);
                    Destroy(hit.collider.gameObject);
                }
                
            } 
        }
        StartCoroutine(LaserSequence()); 
    }

    // process of visually rendering the laser with particle effects
    public IEnumerator LaserSequence()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        lineRenderer.enabled = false;
        AutoTurret atur = GameObject.Find("AutoTurret").GetComponent<AutoTurret>();
        // end particle effects when done
        atur.emissions.enabled = false;
        Destroy(gameObject);
    }
}
