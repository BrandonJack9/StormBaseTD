using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Reference to the settings panel GameObject

    // Method to load Stage1 scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // Method to show the settings panel
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);  // Show the settings panel
    }

    // Method to hide the settings panel and go back to the main menu
    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Hide the settings panel
    }
}


//with separate canvas
// public class MainMenu : MonoBehaviour
// {
//     public GameObject mainMenuCanvas;    // Reference to the Main Menu Canvas
//     public GameObject settingsCanvas;    // Reference to the Settings Canvas

//     // Method to start the game
//     public void PlayGame()
//     {
//         // Load your stage (not relevant to the canvas switch, but kept for completeness)
//         SceneManager.LoadScene("Stage1");
//     }

//     // Method to quit the game
//     public void QuitGame()
//     {
//         Debug.Log("Quit Game");
//         Application.Quit();
//     }

//     // Method to switch to the settings canvas
//     public void OpenSettings()
//     {
//         mainMenuCanvas.SetActive(false);  // Disable the Main Menu Canvas
//         settingsCanvas.SetActive(true);   // Enable the Settings Canvas
//     }

//     // Method to switch back to the main menu canvas
//     public void BackToMenu()
//     {
//         settingsCanvas.SetActive(false);  // Disable the Settings Canvas
//         mainMenuCanvas.SetActive(true);   // Enable the Main Menu Canvas
//     }
// }