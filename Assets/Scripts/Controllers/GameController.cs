using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public BottomBarController bottomBar;
    public BackgroundController backgroundController;
    public GameObject storyCanvas;
    public StoryScene currentScene;
    public StoryScene[] sceneCollections;
    public QuizController currentQuiz;
    public GameObject quizTimePanel;
    public Button quizTimeButton;
    public GameObject quizCanvas;
    private bool quizTime = false;
    public bool QuizTime { get => quizTime; set => quizTime = value; }
    private int indexScene = 0;   
    public int IndexScene { get => indexScene; set => indexScene = value; }
    public GameObject ceritaSelesaiPanel;
    public GameObject PausePanel; //pausepanel
    public Button PauseButton; //pausebutton
    public Button pauseRestart;
    public Button closeHintButton;
    public GameObject hintPanel; //pausepanel
    public Button HintButton; //pausebutton
    public PlayerProfile playerProfile;
    public bool GamePause;
    public LevelSelector levelSelector;
   // public IklanManager iklanManager;
    
    // 1. Memutar scene
    void Start()
    {
        StopAllCoroutines();
        QuizTime = false;
        storyCanvas.SetActive(true);
        quizTimePanel.SetActive(false);
        quizCanvas.SetActive(false);
        hintPanel.SetActive(false);
        PausePanel.SetActive(false);
        ceritaSelesaiPanel.SetActive(false);
        if(GameObject.FindGameObjectWithTag("LevelSelector")==null){
            currentScene=sceneCollections[0];
        }
        else{
            levelSelector = GameObject.FindGameObjectWithTag("LevelSelector").GetComponent<LevelSelector>();
            currentScene = sceneCollections[levelSelector.levelSelected];
        }
        bottomBar.PlayScene(currentScene);
        backgroundController.SwitchImage(currentScene);
        PlayerData tempData = LoadData();
        playerProfile.thePlayer = new PlayerProfile(currentQuiz.quizCollections.Length);
        if (tempData.starPerLevel != null)
        {             
            playerProfile.thePlayer.HintCount = tempData.totalHint;
            playerProfile.thePlayer.StarPerLevel = tempData.starPerLevel;
            playerProfile.thePlayer.CountStar();
        }
        Debug.Log(playerProfile.thePlayer.HintCount);

        HintButton.onClick.AddListener(()=>
        {
            Hint();
        });

        closeHintButton.onClick.AddListener(()=>
        {
            closeHint();
        });
        Debug.Log("Bintang Tersimpan: " + playerProfile.thePlayer.CountStar());
    }

    // 2. Menampilkan scene berikut & memeriksa apakah sudah 3 scene, maka Quiz Time
    void Update()
    {
        if(QuizTime==false && bottomBar.IsCompleted() && bottomBar.IsLastSentence())
        {
            if(currentScene.nextScene == null){
                ceritaSelesaiPanel.SetActive(true);
                return;
            }

            currentScene = currentScene.nextScene;
            bottomBar.PlayScene(currentScene);
            var cekScene = System.Array.IndexOf(sceneCollections, currentScene);
            
            if(cekScene!=-1 && ((cekScene+1)%3==0)){
                QuizTime = true;
                StartCoroutine(ShowQuizTime());
                quizTimeButton.onClick.RemoveAllListeners();
                quizTimeButton.onClick.AddListener(()=>{
                    quizTimePanel.SetActive(false);
                    currentQuiz.timer = Mathf.Clamp(60, 0, 60);
                    ShowQuiz(cekScene+1);
                });
            }
            backgroundController.SwitchImage(currentScene);
        }
        else if(QuizTime==false && bottomBar.IsCompleted())
            bottomBar.PlayNextSentence();
    }

    // 2.a Menunggu scene kelipatan 3 selesai ditampilkan dan klik lanjut dari bottombar.IsCompleted()
    private IEnumerator ShowQuizTime(){
        yield return new WaitUntil(()=>bottomBar.IsCompleted());
        quizTimePanel.SetActive(true);
        var cekSceneSekarang = System.Array.IndexOf(sceneCollections, currentScene)+1;
        indexScene = System.Array.IndexOf(sceneCollections, currentScene)+1;
    }

    // 2.b Menampilkan quiz
    private void ShowQuiz(int cekSceneSekarang)
    {
        quizCanvas.SetActive(true);
        currentQuiz.PlayQuiz(cekSceneSekarang);
    }
    // 3. Pausecontrol
    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
        }
    }

    public void Restart()
    {
        var cekScene = System.Array.IndexOf(sceneCollections, currentScene);
        currentScene = sceneCollections[cekScene-1];
        currentQuiz.resultCanvas.SetActive(false);
        quizCanvas.SetActive(false);
        PausePanel.SetActive(false);
        currentQuiz.failedPanel.SetActive(false);
        QuizTime = false;
        backgroundController.SwitchImage(currentScene);
        bottomBar.PlayScene(currentScene);
        Time.timeScale = 1;
    }

    // hint
    public void Hint()
    {
        // playerProfile.thePlayer.HintCount = 3;
        if (playerProfile.thePlayer.HintCount > 0)
        {
            hintPanel.SetActive(true);
            Time.timeScale = 0;
            playerProfile.thePlayer.HintCount -= 1;
            SaveData(playerProfile.thePlayer);
            LoadData();
            Debug.Log("jumlah hint sekarang: " + playerProfile.thePlayer.HintCount);
        }
        else
        {
            Debug.Log("hint habis");
          //  iklanManager.ShowRewarded();
            Time.timeScale = 0;
            playerProfile.thePlayer.HintCount += 1;
            SaveData(playerProfile.thePlayer);
            LoadData();
            Debug.Log("jumlah hint sekarang: " + playerProfile.thePlayer.HintCount);
        }
    }

    public void closeHint()
    {
        hintPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public static void SaveData(PlayerProfile playerProfile) {
        

        for (int i = 0; i < playerProfile.StarPerLevel.Count; i++)
        {
            PlayerPrefs.SetInt("star_"+ i, playerProfile.StarPerLevel[i]);
        }
        PlayerPrefs.SetInt("total_hint", playerProfile.HintCount);
        PlayerPrefs.SetInt("total_star", playerProfile.StarCount);
        PlayerPrefs.Save();
    }

    public static PlayerData LoadData(){
        var tempData = new PlayerData();
        List<int> temp = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            temp.Add(PlayerPrefs.GetInt("star_"+ i, 0));
        }
        tempData.totalHint = PlayerPrefs.GetInt("total_hint", 3);
        tempData.totalStar = PlayerPrefs.GetInt("total_star", 0);

        tempData.starPerLevel = temp;
        return tempData;
    }
}
