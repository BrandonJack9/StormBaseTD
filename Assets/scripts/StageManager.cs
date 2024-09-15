using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageManager : MonoBehaviour
{
    public int stageNumber;
    public TextMeshProUGUI timer;

    // Update is called once per frame
    void Update()
    {
        // move to next stage if current one is completed
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0 && timer.text == "00:00")
        {
            Debug.Log("You win!!!!!!!!!!: " + stageNumber);
            if (stageNumber < 3)
            {
                int buf = ++stageNumber;
                SceneManager.LoadScene("Stage" + buf.ToString());
            }
            else
            {
                // do something like exit the game
            }
        }
    }
}
