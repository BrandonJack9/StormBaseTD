using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime;
    void Start()
    {
        StartCoroutine(DeleteSelf());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeleteSelf()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
