using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLazerProjectileScript : MonoBehaviour
{
    public float projectileLifeTime;
 
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("AutoLazer generated");   
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("meteor") || other.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("turret") && !collision.gameObject.CompareTag("turretgun"))
        {
            Debug.Log("Destroy lazer");
            StartCoroutine(CollisionDestroy());
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DeleteLazer(projectileLifeTime));
    }

    IEnumerator CollisionDestroy()
    {
        yield return new WaitForSeconds(.05f);
        Destroy(gameObject);
    }
    IEnumerator DeleteLazer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    public void DestroyLazer()
    {
        Destroy(gameObject);
    }
}
