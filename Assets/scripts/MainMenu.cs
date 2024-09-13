using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For detecting the active scene
using UnityEngine.EventSystems;     // For detecting if mouse is over UI

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;  // Reference to the settings panel GameObject
    public GameObject pauseMenuUI;    // Reference to the pause menu UI GameObject
    public GameObject timerObject;    // Reference to the timer object you want to hide when paused
    public GameObject crosshair;
    public GameObject Pauseicon;
    public GameObject stageFailText;

    private CanvasManager canvasManager;

    // public GameObject pausButt;

    // public static MainMenu instance;
    // [SerializeField] GameObject deathScreen, crossHair, timerText;
    private bool isPaused = false;    // Tracks whether the game is paused or not

    void Start()
    {
        canvasManager = CanvasManager.instance;
        // Only lock the cursor if not in the Main Menu scene
        if (!IsInMainMenu())
        {
            LockCursor();  // Lock the cursor initially when the scene starts (if not MainMenu)
        }
        else
        {
            UnlockCursor();  // Unlock the cursor in the Main Menu scene
        }
    }

    // Method to load Stage1 scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        // Time.timeScale = 1;
        // SceneManager.LoadScene("MainMenu");
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

    // Method to toggle the pause menu and game state
    public void TogglePause()
    {
        if (canvasManager.deathScreen.activeSelf)
        {
            Debug.Log("Cannot pause the game when the death screen is active.");
            return;
        }
        
        if (pauseMenuUI == null)
        {
            Debug.LogWarning("No pause menu assigned. Ignoring pause functionality.");
            return;
        }

        if (isPaused)
        {
            ResumeGame(); // Unpause if already paused
        }
        else
        {
            PauseGame(); // Pause the game if not already paused
        }
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0;                // Stops the game time
        pauseMenuUI.SetActive(true);       // Show the pause menu
        if (timerObject != null)
        {
            timerObject.SetActive(false);  // Hide the timer object when paused
        }
        if (crosshair != null)
        {
            crosshair.SetActive(false);    // Hide the crosshair when paused
        }
        if (Pauseicon != null)
        {
            Pauseicon.SetActive(false);    // Hide the pause icon when paused
        }
        // if (stageFailText != null)
        // {
        //     stageFailText.SetActive(false);    // Hide the pause icon when paused
        // }
        isPaused = true;                   // Set the game as paused

        UnlockCursor(); // Unlock and show the cursor for UI interaction
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1;                // Resumes the game time
        pauseMenuUI.SetActive(false);      // Hide the pause menu
        settingsPanel.SetActive(false);
        if (timerObject != null)
        {
            timerObject.SetActive(true);   // Show the timer object when resuming
        }
        if (crosshair != null)
        {
            crosshair.SetActive(true);     // Show the crosshair when resuming
        }
        if (Pauseicon != null)
        {
            Pauseicon.SetActive(true);     // Show the pause icon when resuming
        }
        // if (stageFailText != null)
        // {
        //     stageFailText.SetActive(true);    // Hide the pause icon when paused
        // }
        isPaused = false;                  // Set the game as unpaused

        LockCursor(); // Lock the cursor again when the game resumes (only if not in Main Menu)
    }

    // Method to lock the cursor
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Method to unlock the cursor
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Check if the user presses the "Escape" key to pause or resume the game
    void Update()
    {
        if (pauseMenuUI != null && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); // Pause or resume the game using the Escape key
        }

        // Lock the cursor if the mouse is clicked, unless over UI and not in Main Menu
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI() && !IsInMainMenu())
        {
            LockCursor();
        }
    }

    // Helper method to check if the cursor is over a UI element
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();  // Detects if the mouse is over a UI element
    }

    // Helper method to check if the current scene is the Main Menu
    private bool IsInMainMenu()
    {
        // Replace "MainMenu" with the exact name of your Main Menu scene
        return SceneManager.GetActiveScene().name == "MainMenu";
    }

    // public void GameOver()
    // {
    //     Cursor.lockState = CursorLockMode.None;
    //     deathScreen.SetActive(true);
    //     crossHair.SetActive(false);
    //     timerText.SetActive(false);
    //     pausButt.SetActive(false);
    // }
}




// public class MainMenu : MonoBehaviour
// {
//     public GameObject settingsPanel; // Reference to the settings panel GameObject

//     // Method to load Stage1 scene
//     public void PlayGame()
//     {
//         SceneManager.LoadScene("Stage1");
//     }

//     // Method to quit the game
//     public void QuitGame()
//     {
//         Debug.Log("Quit Game");
//         Application.Quit();
//     }

//     // Method to show the settings panel
//     public void OpenSettings()
//     {
//         settingsPanel.SetActive(true);  // Show the settings panel
//     }

//     // Method to hide the settings panel and go back to the main menu
//     public void CloseSettings()
//     {
//         settingsPanel.SetActive(false); // Hide the settings panel
//     }
// }













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