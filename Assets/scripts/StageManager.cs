using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public int stageNumber;
    public TextMeshProUGUI timer;
    public GameObject stageCompleteUI; // Reference to the Stage Complete UI Canvas
    public TextMeshProUGUI stageCompleteText; // Reference to the "Stage Complete" text
    public Button nextStageButton; // Reference to the "Enter Next Stage" button
    [SerializeField] GameObject timerthing;
    public GameObject pausButt;
    public GameObject bar;
    public int stageAmount;
    public GameObject crossHair;

    private bool stageCompleted = false;

    void Start()
    {
        // Ensure the Stage Complete UI is hidden at the start
        stageCompleteUI.SetActive(false);

        // Add listener to the button
        nextStageButton.onClick.AddListener(OnNextStageButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if stage is completed
        // if (!stageCompleted && GameObject.FindGameObjectsWithTag("enemy").Length == 0 && timer.text == "00:00")
        if (!stageCompleted && timer.text == "00:00")
        {
            Debug.Log("Stage Complete!: " + stageNumber);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crossHair.SetActive(false);
            // // timerText.SetActive(false);
            timerthing.SetActive(false);
            pausButt.SetActive(false);
            bar.SetActive(false);
            ShowStageCompleteUI();
        }
    }

    // Show the "Stage Complete" UI
    void ShowStageCompleteUI()
    {
        stageCompleted = true;

        // Update the stage complete text
        stageCompleteText.text = "Stage " + stageNumber + " Complete!";
        
        // Show the UI
        stageCompleteUI.SetActive(true);
    }

    // Called when the "Enter Next Stage" button is clicked
    void OnNextStageButtonClicked()
    {
        // Load the next stage if it's within range
        if (stageNumber < stageAmount)
        {
            stageNumber++;
            SceneManager.LoadScene("Stage" + stageNumber.ToString());
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("All stages completed! Exiting game...");
            SceneManager.LoadScene("WinScreen");
            // You can add code to quit the game or show a final screen
            //Application.Quit();
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using TMPro;

// public class StageManager : MonoBehaviour
// {
//     public int stageNumber;
//     public TextMeshProUGUI timer;

//     // Update is called once per frame
//     void Update()
//     {
//         // move to next stage if current one is completed
//         if (GameObject.FindGameObjectsWithTag("enemy").Length == 0 && timer.text == "00:00")
//         {
//             Debug.Log("You win!!!!!!!!!!: " + stageNumber);
//             if (stageNumber < 3)
//             {
//                 int buf = ++stageNumber;
//                 SceneManager.LoadScene("Stage" + buf.ToString());
//             }
//             else
//             {
//                 // do something like exit the game
//             }
//         }
//     }
// }
