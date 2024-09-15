using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
   AudioSource powerUpSFX;
   GameObject lazerSFXHolder;

   GameObject gameOSTHolder;
   AudioSource[] lazerSFX;
   
   AudioSource[] ost;

    public static AudioManager instance {get; private set;}
    private void Awake() {
        if (instance != null && instance != this){
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded+= OnSceneLoaded;
    }
    void Start()
    {
        powerUpSFX = GetComponent<AudioSource>();
        lazerSFXHolder = gameObject.transform.GetChild(0).gameObject;
        lazerSFX = lazerSFXHolder.GetComponents<AudioSource>();
        
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        gameOSTHolder = gameObject.transform.GetChild(1).gameObject;
        ost = gameOSTHolder.GetComponents<AudioSource>();
        HandleOST();
    }

    // Update is called once per frame
    
    public void PlayPowerUpSFX(){
        powerUpSFX.Play();
    }

    public void PlayLazerSFX(){
        foreach (AudioSource source in lazerSFX){
            if (!source.isPlaying){
                source.Play();
            }
        }
    }

    private void HandleOST(){
        if (SceneManager.GetActiveScene().name == "MainMenu"){
            ost[2].Play();
            ost[0].Stop();
            ost[1].Stop();
        }
        if (SceneManager.GetActiveScene().name != "Stage3" && SceneManager.GetActiveScene().name != "MainMenu"){
            ost[0].Play();
            ost[1].Stop();
            ost[2].Stop();
        } else if (SceneManager.GetActiveScene().name == "Stage3"){
            ost[1].Play();
            ost[2].Stop();
            ost[0].Stop();
        }
    }
}
