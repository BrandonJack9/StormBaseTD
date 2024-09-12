using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    private bool isAiming;
    private bool isFiring;
    private bool isWaiting;
    public ParticleSystem.EmissionModule emissions;
    public int turretRadarRadius;
    public float rotationSpeed;
    public float cooldownTime;
    public float rotationTimeMax;
    public Vector3 turretPosition;
    public Vector3 currentEnemyPosition;
    public GameObject myEnemy;
    public GameObject theProjectilePrefab;
    public TurretStatus status;

    // Start is called before the first frame update
    void Start()
    {
        isAiming = false;
        isFiring = false;
        isWaiting = false;
        turretPosition = gameObject.transform.position;
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        emissions = ps.emission;
        emissions.enabled = false;   
    }

    // Update is called once per frame
    void Update()
    {
        // KEY:
        // Detecting: find the closest enemy projectile
        // Aiming: rotate the turret towards the target
        // Firing: shoot laser and destroy the target
        // Waiting: Do nothing
        switch (status)
        {
            case TurretStatus.Detecting:
                DetectFirstEnemyProjectile();
                break;
            case TurretStatus.Aiming:
                StartCoroutine(AimingSequence());
                break;
            case TurretStatus.Firing:
                StartCoroutine(FiringSequence());
                break;
            case TurretStatus.Waiting:
                StartCoroutine(Cooldown());
                break;
        }
    }

    // get all gameObject projectiles within its radius
    // find closest one
    public void DetectFirstEnemyProjectile()
    { 
        Collider[] allObjects = Physics.OverlapSphere(turretPosition, turretRadarRadius);
        float closest = 99999f;
        GameObject theClosestMeteor;

        foreach (Collider i in allObjects)
        {
            // only get closest enemy projectile
            if (i.gameObject.CompareTag("meteor"))
            {
                if (i.gameObject != null)
                {
                    if (Vector3.Distance(turretPosition, i.gameObject.transform.position) < closest)
                    {
                        closest = Vector3.Distance(turretPosition, i.gameObject.transform.position);
                        theClosestMeteor = i.gameObject;
                        currentEnemyPosition = theClosestMeteor.transform.position;
                        myEnemy = i.gameObject;
                    }

                }
                else
                {
                    Debug.Log("The meteor is null");
                }
            }
        }
        if (closest < 99999f)
        {
            status = TurretStatus.Aiming;
            Debug.Log("Aiming towards enemy");
        }
    }


    // fires a laser at enemy projectile and triggers particle effects
    public void Fire()
    {
        // instatiate the projectile firing towards the enemy
        // projectile will have the script
        if (myEnemy != null)
        {
            emissions.enabled = true;
            GameObject laser = (GameObject)Instantiate(theProjectilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Debug.Log(currentEnemyPosition + " is the current enemy position");
            laser.GetComponent<AutoTurretProjectile>().Fire(gameObject.transform.position, myEnemy.transform.position);
            
        } 
        // projectile = prefab with the aiming data
        // funnel in data regarding the enemy position
        // wait 1 second before firing again
    }

    // turn turret towards projectile
    public IEnumerator AimingSequence()
    {
        if (isAiming)
        {
            yield break;
        }

        if (status != TurretStatus.Aiming)
        {
            yield break;
        }

        isAiming = true;

        Quaternion targetRotation = Quaternion.LookRotation(currentEnemyPosition - turretPosition);
        float rotationTime = 0.0f;
        while (rotationTime < rotationTimeMax)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationTime);
            rotationTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }
        status = TurretStatus.Firing;
        isAiming = false;
    }

    // fire at the enemy
    public IEnumerator FiringSequence()
    {
        if (isFiring)
        {
            yield break;
        }

        if (status != TurretStatus.Firing)
        {
            yield break;
        }

        isFiring = true;

        Fire();
        status = TurretStatus.Waiting;
        isFiring = false;
    }

    // wait before selecting and destroying next target
    public IEnumerator Cooldown()
    {
        if (isWaiting)
        {
            yield break;
        }

        if (status != TurretStatus.Waiting)
        {
            yield break;
        }

        isWaiting = true;

        yield return new WaitForSeconds(cooldownTime);

        status = TurretStatus.Detecting;
        isWaiting = false;

    }
}

public enum TurretStatus
{
    Detecting,
    Aiming,
    Firing,
    Waiting
}
