using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;                // Reference to the TextMeshPro component
    public float startTime = 60f;                    // Start time in seconds (1 minute)
    public EnemySpawner enemySpawner;          // Reference to the RandomMeteorSpawn script

    private float timeRemaining;
    private bool timerIsRunning = false;

    void Start()
    {
        timeRemaining = startTime;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                timerText.text = "00:00";

                // Stop spawning meteors when the timer ends
                enemySpawner.StopSpawning();

                OnTimerEnd();
            }
        }
    }

    private void OnTimerEnd()
    {
        Debug.Log("Timer has ended!");
    }
}
