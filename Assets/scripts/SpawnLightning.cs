using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightning : MonoBehaviour
{
    public GameObject lightningParticleSystem;
    public float minInterval;
    public float maxInterval;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lightningCoroutine());
    }

    IEnumerator lightningCoroutine()
    {
        while (true)
        {
            Instantiate(lightningParticleSystem, new Vector3(transform.position.x + Random.Range(-500, 500), transform.position.y, transform.position.z + Random.Range(-500, 500)), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }
}
