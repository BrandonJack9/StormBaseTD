using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPowerup : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public GameObject impactEffect;
    public GameObject autoTurretPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("lazer"))
        {
            GameObject tur = Instantiate(autoTurretPrefab);
            tur.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
