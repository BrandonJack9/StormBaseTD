using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    [SerializeField] GameObject powerupPanel, deathScreen, bossHealthBar;
    
    public void Awake()
    {
        if (CanvasManager.instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void Start()
    {
        deathScreen.SetActive(false);
    }
    public void PowerUp(string s)
    {
        powerupPanel.SetActive(true);
        //powerupPanel.GetComponent<PowerupPanel>().descriptionText.text = s;
    }

    public void bossHealth(bool active)
    {
        if (active)
        {
            bossHealthBar.SetActive(true);
        }
        else
        {
            bossHealthBar.SetActive(false);
        }
    }
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
    }


}
