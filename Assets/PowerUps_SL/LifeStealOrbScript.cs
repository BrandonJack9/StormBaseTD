using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealOrbScript : MonoBehaviour
{
    public float speed = 10f;                // Speed of the projectile
    public float rotateSpeed = 200f;        // Optional: Rotation speed for spinning meteor effect
    public GameObject impactEffect;         // Effect to spawn on impact
    public static bool stopAllMeteors = false; // Static boolean to control if all meteors should stop


    void OnTriggerEnter(Collider other)
    {
        // Check if the meteor has hit the target (or any object you want)
        if (other.CompareTag("lazer"))// Ensure the target has the "Target" tag
        {
            if(other.GetComponent<LifeStealProjectile>().level <= 10){
                other.GetComponent<LifeStealProjectile>().parentTurret.GetComponent<UpdatedTurretScript>().lifestealLevel+=1;
                other.GetComponent<LifeStealProjectile>().level+=1;
            }
            Destroy(gameObject);
        }

    }
    void Update()
    {
      
    }

   
  

}