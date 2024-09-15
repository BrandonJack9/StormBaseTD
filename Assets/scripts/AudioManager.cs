using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   AudioSource powerUpSFX;
   public GameObject lazerSFXHolder;
   AudioSource[] lazerSFX;

    public static AudioManager instance {get; private set;}
    private void Awake() {
        if (instance != null && instance != this){
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        powerUpSFX = GetComponent<AudioSource>();
        lazerSFXHolder = gameObject.transform.GetChild(0).gameObject;
        lazerSFX = lazerSFXHolder.GetComponents<AudioSource>();
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
}
