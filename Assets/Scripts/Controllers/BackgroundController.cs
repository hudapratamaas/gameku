using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public GameController gameController;
    
    // 1.d Mengubah background image cerita
    public void SwitchImage(StoryScene sceneIndex){
        this.gameObject.GetComponent<Image>().sprite = gameController.currentScene.background;
    }
}
