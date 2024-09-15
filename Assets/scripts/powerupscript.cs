using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupscript : MonoBehaviour
{
    // Start is called before the first frame update
    public int rotateSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("lazer"))
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }
}
