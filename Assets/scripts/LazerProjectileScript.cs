using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;

public class LazerProjectileScript : MonoBehaviour
{
    public float projectileLifeTime;
    public GameObject crystal;
    public GameObject pyramid;
    public GameObject lifeStealParticles;
    public GameObject lifeStealParticlesContinuous;
    bool stealLife = false;
    public float lifeStealStrength;
    // Start is called before the first frame update
   
    void Start()
    {
        crystal = GameObject.Find("turret");
        pyramid = GameObject.Find("basePyramid");

        if(crystal.GetComponent<TurretScript>().lifeStealObtained == true)
        {
            lifeStealParticles.SetActive(true);
            stealLife = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("lifesteal"))
        {
            Debug.Log("got lifesteal");
            crystal.GetComponent<TurretScript>().lifeStealObtained = true;
            if (crystal.GetComponent<TurretScript>().lifeStealStack < 10)
            {
                crystal.GetComponent<TurretScript>().lifeStealStack++;
            }
        }

        if (other.CompareTag("continuouspowerup")){
            Debug.Log("got continuous beam");
            crystal.GetComponent<TurretScript>().continuousBeamObtained = true;
        }

        if (other.CompareTag("meteor") || other.CompareTag("enemy"))
        {
            pyramid.GetComponent<HealthManager>().Heal(1 * ((crystal.GetComponent<TurretScript>().lifeStealStack * .33f)*lifeStealStrength));
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DeleteLazer(projectileLifeTime));
    }

    IEnumerator DeleteLazer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
