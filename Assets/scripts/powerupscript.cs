using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupscript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("lazer"))
        {
            AudioSource powerupSFX = GetComponent<AudioSource>();
            powerupSFX.Play();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
