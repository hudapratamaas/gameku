using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {
 
    [SerializeField] GameController gameController;
    public AudioClip NewMusic;
    public GameObject audioManager;
    private void Awake(){ 
        UbahMusik();
    }
    private void Update(){
        if(gameController.currentScene.music==null || gameController.currentScene.music==NewMusic){
            return;
        }
        else{
            UbahMusik();
        }
    }
    public void UbahMusik(){
        audioManager = GameObject.Find("AudioManager");
        NewMusic = gameController.currentScene.music;
        if(NewMusic == null || audioManager == null)
            return;
        else
        {
            audioManager.GetComponent<AudioSource>().clip = NewMusic;
            audioManager.GetComponent<AudioSource>().Play();
        }
    }
}