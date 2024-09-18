using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoTurret : MonoBehaviour
{
    private bool isAiming;
    private bool isFiring;
    private bool isWaiting;
    private bool isHovering;
    private Vector3 hoverTarget;
    public int turretRadarRadius;
    public int hoverHeight;
    public float hoverSpeed;
    public float rotationSpeed;
    public float cooldownTime;
    public float rotationTimeMax;
    public float lazerSpeed;
    public Vector3 turretPosition;
    public Vector3 currentEnemyPosition;
    public GameObject myEnemy;
    public GameObject theProjectilePrefab;
    public GameObject gun;
    public TurretStatus status;



    public int maxTurretsAmount;

    // Start is called before the first frame update
    void Start()
    {
        
        if (AutoTurretManager.instance.GetCurrentTurretsAmount() > AutoTurretManager.instance.GetMaxTurretAmount()){
            Destroy(gameObject);
        } 
        AutoTurretManager.instance.IncrementCurrentTurretAmount();
        gun = gameObject.transform.Find("gun").gameObject;
        isAiming = false;
        isFiring = false;
        isWaiting = false;
        isHovering = false;
        turretPosition = gun.transform.position;
        hoverTarget = new (turretPosition.x, turretPosition.y + hoverHeight, turretPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        AutoTurretCases(status);
    }

    private void AutoTurretCases(TurretStatus status){
        switch (status)
        {
            case TurretStatus.Hovering:
                StartCoroutine(HoverSequence());
                break;
            case TurretStatus.Detecting:
                DetectFirstEnemyProjectile();
                break;
            case TurretStatus.Aiming:
                // StartCoroutine(AimingSequence());
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
        Debug.Log("Turret operational");
        Collider[] allObjects = Physics.OverlapSphere(turretPosition, turretRadarRadius);
        float closest = 99999f;
        GameObject theClosestMeteor;

        foreach (Collider i in allObjects)
        {
            // only get closest enemy projectile
            if (i.gameObject.CompareTag("meteor") || i.gameObject.CompareTag("enemy"))
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
            status = TurretStatus.Firing;
            // status = TurretStatus.Aiming;
            Debug.Log("Aiming towards enemy");
        }
    }


    // fires a laser at enemy projectile and triggers particle effects
    public void Fire()
    {
        Quaternion targetRotation = Quaternion.LookRotation(currentEnemyPosition - gun.transform.position);
        gun.transform.rotation = targetRotation;
        // instatiate the projectile firing towards the enemy
        // projectile will have the script
        if (myEnemy != null)
        {
            GameObject laser = Instantiate(theProjectilePrefab);
            laser.transform.position = gun.transform.position;
            laser.transform.up = gun.transform.forward;
            Debug.Log(currentEnemyPosition + " is the current enemy position");
            laser.GetComponent<Rigidbody>().AddForce(laser.transform.up * lazerSpeed, 0);
        } 
        // projectile = prefab with the aiming data
        // funnel in data regarding the enemy position
        // wait 1 second before firing again
    }

    public bool HoverToTop()
    {
        return hoverTarget != (transform.position = Vector3.MoveTowards(transform.position, hoverTarget, hoverSpeed * Time.deltaTime));
    }

    public IEnumerator HoverSequence()
    {
        if (isHovering)
        {
            yield break;
        }

        if (status != TurretStatus.Hovering)
        {
            yield break;
        }

        isHovering = true;

        while (HoverToTop())
        {
            yield return null;
        }

        isHovering = false;
        status = TurretStatus.Detecting;
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

        Debug.Log("my enemy is in: " + currentEnemyPosition);
        Quaternion targetRotation = Quaternion.LookRotation(currentEnemyPosition - gun.transform.position);
        float rotationTime = 0.0f;
        while (rotationTime < rotationTimeMax)
        {
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, targetRotation, rotationTime);
            rotationTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }
        Debug.Log("Preparing to fire...");
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
    Hovering,
    Detecting,
    Aiming,
    Firing,
    Waiting
}
