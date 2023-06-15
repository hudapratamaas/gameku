using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [SerializeField] GameObject questionPanelPilgan;
    [SerializeField] GameObject questionPanelSusunKata;
    [SerializeField] GameObject questionPanelMengisi;
    [SerializeField] public GameObject resultCanvas;
    [SerializeField] public GameObject completedPanel;
    [SerializeField] public GameObject failedPanel;
    [SerializeField] public GameObject hintPanel;
    [SerializeField] public PlayerProfile playerProfile;
    [SerializeField] public GameObject oneStar; 
    [SerializeField] public GameObject twoStar;
    [SerializeField] public GameObject threeStar;
    [SerializeField] GameObject minusImage;
    [SerializeField] GameObject wrongImage;
    [SerializeField] Button nextButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button hintButton;
    [SerializeField] GameObject questionImage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip wrongClip;
    [SerializeField] AudioClip correctClip;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI hintText;
    public int sentenceIndex;
    public float timer;
    public float initialTimer = 60f;
    public GameController gameController;
    public KataManager kataManager;
    public Quiz[] quizCollections;
    public Quiz currentQuiz;
    public QuizTypes quizTypes;
    public bool Stop;
    private bool isAnimating = false;

    private void Start(){
        resultCanvas.SetActive(false);
        completedPanel.SetActive(false);
        failedPanel.SetActive(false);
        hintPanel.SetActive(false);
        questionImage.SetActive(false);
        questionPanelPilgan.SetActive(false);
        questionPanelSusunKata.SetActive(false);
        questionPanelMengisi.SetActive(false);
        minusImage.SetActive(false);
        wrongImage.SetActive(false);

        timer = Mathf.Clamp(60, 0, 60);
        Stop = true;
        Time.timeScale = 1;
        TimeManager();
        gameController.pauseRestart.onClick.AddListener(()=>{
            if(currentQuiz.quizzes.quizType==QuizTypes.QuizType.Menyusun){
                for(int i = 0; i<kataManager.startSlot.childCount; i++){
                    Destroy(kataManager.startSlot.GetChild(i).gameObject);
                }
                for(int i = 0; i<kataManager.answerSlot.childCount; i++){
                    Destroy(kataManager.answerSlot.GetChild(i).gameObject);
                }
            }
            if(currentQuiz.quizzes.quizType==QuizTypes.QuizType.Mengisi){
                quizTypes.jawabanMengisi.text = "";
            }
        });
    }

    // Memulai countdown timer quiz
    private void Update()
    {
        // if(timerText==null || gameOverPanel==null)
        if(timerText==null)
            return;
        
        if(Stop==false){
            if(timer>0)
                timer -= Time.deltaTime;
            else
                timer = 0;
        }
        
        timerText.text = timer.ToString("0");
        if(timer==0){
            timer=-1;
            Stop=true;
            resultCanvas.SetActive(true);
            failedPanel.SetActive(true);
            if(currentQuiz.quizzes.quizType==QuizTypes.QuizType.Menyusun){
                for(int i = 0; i<kataManager.startSlot.childCount; i++){
                    Destroy(kataManager.startSlot.GetChild(i).gameObject);
                }
                for(int i = 0; i<kataManager.answerSlot.childCount; i++){
                    Destroy(kataManager.answerSlot.GetChild(i).gameObject);
                }
            }
            if(currentQuiz.quizzes.quizType==QuizTypes.QuizType.Mengisi){
                quizTypes.jawabanMengisi.text = "";
            }
        }
    }
    // Mengatur countdown timer quiz
    public void TimeManager(){
        if(timerText==null)
            return;

        timerText.text = "";
        timer=60;
        
        questionText.enabled = true;
        timerText.enabled = true;
    }
    // 2.b.i Menampilkan quiz dan mengatur button failed dan sukses
    public void PlayQuiz(int index)
    {
        Stop = false;
        sentenceIndex = index/3-1;
        currentQuiz = (Quiz)quizCollections[sentenceIndex];
        questionText.text = currentQuiz.quizzes.question;
        hintText.text = currentQuiz.quizzes.hint;

        if(currentQuiz.quizzes.questionImage != null){
            questionImage.SetActive(true);
            questionImage.GetComponent<Image>().sprite = currentQuiz.quizzes.questionImage;
        }
        else{
            questionImage.SetActive(false);
        }
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(()=>{
            resultCanvas.SetActive(false);
            gameController.quizCanvas.SetActive(false);
            failedPanel.SetActive(false);
            gameController.QuizTime = false;
            gameController.currentScene = gameController.sceneCollections[index-3];
            gameController.backgroundController.SwitchImage(gameController.currentScene);
            gameController.bottomBar.PlayScene(gameController.currentScene);
        });
        gameController.pauseRestart.onClick.AddListener(()=>{
            Stop = true;
            Time.timeScale=1;
            gameController.QuizTime = false;
            resultCanvas.SetActive(false);
            gameController.quizCanvas.SetActive(false);
            failedPanel.SetActive(false);
            gameController.QuizTime = false;
            gameController.PausePanel.SetActive(false);
            gameController.currentScene = gameController.sceneCollections[index-3];
            gameController.backgroundController.SwitchImage(gameController.currentScene);
            gameController.bottomBar.PlayScene(gameController.currentScene);
        });
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(()=>{
            resultCanvas.SetActive(false);
            gameController.quizCanvas.SetActive(false);
            failedPanel.SetActive(false);
            gameController.QuizTime = false;
            gameController.currentScene = gameController.sceneCollections[index];
            gameController.backgroundController.SwitchImage(gameController.currentScene);
            gameController.bottomBar.PlayScene(gameController.currentScene);
        });
        ChooseAnswer();
    }

    // 2.b.ii Mengaktifkan canvas sesuai jenis quiz
    public void ChooseAnswer()
    {
        var questionType = currentQuiz.quizzes.quizType;
        if(questionType.ToString()==QuizType.Pilgan.ToString()){
            questionPanelPilgan.SetActive(true);
            questionPanelSusunKata.SetActive(false);
            questionPanelMengisi.SetActive(false);
        }
        else if(questionType.ToString()==QuizType.Menyusun.ToString()){
            questionPanelPilgan.SetActive(false);
            questionPanelSusunKata.SetActive(true);
            questionPanelMengisi.SetActive(false);
        }
        else{
            questionPanelPilgan.SetActive(false);
            questionPanelSusunKata.SetActive(false);
            questionPanelMengisi.SetActive(true);
        }
        quizTypes.checkQuizType((Quiz)currentQuiz);
    }

    public void Correct(){
        audioSource.PlayOneShot(correctClip);
    }

    public void Wrong(){
        if(isAnimating==false)
            StartCoroutine(WrongAnimationIE());
    }

    private IEnumerator WrongAnimationIE(){
        isAnimating=true;
        minusImage.SetActive(true);
        wrongImage.SetActive(true);
        audioSource.PlayOneShot(wrongClip);
        var minusTime = minusImage.GetComponent<Animator>();
        var wrong = wrongImage.GetComponent<Animator>();
        minusTime.Play("Base Layer.MinusTime");
        wrong.Play("Base Layer.Wrong");
        yield return new WaitForSecondsRealtime(1);
        minusImage.SetActive(false);
        wrongImage.SetActive(false);
        isAnimating=false;
    }
}
