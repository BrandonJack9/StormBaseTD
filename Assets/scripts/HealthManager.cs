using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("meteor")){
            TakeDamage(5);
        }

        if (other.gameObject.CompareTag("enemyProjectile"))
        {
            TakeDamage(1);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
        if(healthAmount <= 0)
        {
            CanvasManager.instance.GameOver();
        }
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount;
    }
}
