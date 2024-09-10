using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Simple code attached to a UI button to change scenes / levels
 */

public class ChangeScene : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
