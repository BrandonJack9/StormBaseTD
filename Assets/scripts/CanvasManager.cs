using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    [SerializeField] GameObject powerupPanel, bossHealthBar, crossHair, timer;
    public EnemySpawner enemySpawner;
    public GameObject pausButt;
    public GameObject bar;
    public GameObject deathScreen;

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
    //     timer.SetActive(false);
    // }
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreen.SetActive(true);
        crossHair.SetActive(false);
        // timerText.SetActive(false);
        timer.SetActive(false);
        pausButt.SetActive(false);
        bar.SetActive(false);
    }

    public void RestartLevel()
    {
        enemySpawner.StartSpawning();
    }
}
