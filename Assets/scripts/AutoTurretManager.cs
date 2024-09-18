using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class AutoTurretManager : MonoBehaviour
{
    public static AutoTurretManager instance {get; private set;}
    public int currentTurretsAmount;

    public int maxTurretsAmount; 
    private void Awake(){
        if (instance != null && instance != this){
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        currentTurretsAmount = 0;
    }

    public int GetMaxTurretAmount(){
        return maxTurretsAmount;
    }

    public int GetCurrentTurretsAmount(){
        return currentTurretsAmount;
    }
    public void IncrementCurrentTurretAmount(){
        currentTurretsAmount++;
    }

}
