using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    [SerializeField] GameObject powerupPanel, deathScreen, bossHealthBar, crossHair, timerText;
    public RandomMeteorSpawn meteorSpawner;
    public GameObject pausButt;

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
    // public void GameOver()
    // {
    //     Cursor.lockState = CursorLockMode.None;
    //     deathScreen.SetActive(true);
    //     crossHair.SetActive(false);
    //     timerText.SetActive(false);
    // }
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
        crossHair.SetActive(false);
        timerText.SetActive(false);
        pausButt.SetActive(false);
    }

    public void RestartLevel()
    {
        meteorSpawner.StartSpawning();
    }
}
