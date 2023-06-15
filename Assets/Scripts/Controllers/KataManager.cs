using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Memiliki metode convert dari array ke list
using UnityEngine.UI;

public class KataManager : MonoBehaviour
{
    public static KataManager Instance { get; set; }
    [SerializeField] SusunKata wordPrefab;
    [SerializeField] public Transform startSlot, answerSlot;
    [SerializeField] QuizController quizController;
    private int poinKata, poin;

    void Start()
    {
        Instance = this;
        quizController.resultCanvas.SetActive(false);
        quizController.completedPanel.SetActive(false);
        quizController.failedPanel.SetActive(false);
        quizController.Stop = false;
        quizController.timer = Mathf.Clamp(quizController.initialTimer, 0, quizController.initialTimer);;
    }
    public void InitKata(string word){
        Debug.Log("Apakah masuk: " + word);
        char[] letter = word.ToCharArray();
        char[] letterEmpty = new char[letter.Length];
        char[] letterShuffle = new char[letter.Length];

        List<char> letterCopy =  new List<char>();
        letterCopy = letter.ToList(); // membutuhkan System.Linq

        for (int i = 0; i < letterShuffle.Length; i++)
        {
            int randomIndex = Random.Range(0, letterCopy.Count);
            letterShuffle[i] = letterCopy[randomIndex]; // pakai list supaya ketika diisi akan diremove dan tidak dapat terpakai lagi
            letterCopy.RemoveAt(randomIndex); 

            SusunKata temp = Instantiate(wordPrefab, startSlot);
            temp.Initialize(startSlot, letterShuffle[i].ToString(), false, quizController);
        }

        for (int i = 0; i < letter.Length; i++)
        {
            SusunKata temp = Instantiate(wordPrefab, startSlot);

            temp.Initialize(answerSlot, letter[i].ToString(), true, quizController);
        }

        poinKata = letter.Length;
    }

    public void TambahPoin(){
        poin++;

        if (poin == poinKata){
            // 3 bintang
            if(quizController.timer >= 40){
                quizController.resultCanvas.SetActive(true);
                quizController.completedPanel.SetActive(true);
                quizController.threeStar.SetActive(true);
                quizController.twoStar.SetActive(false);
                quizController.oneStar.SetActive(false);
                quizController.Stop = true;
                quizController.timer = -1;
                quizController.Correct();
            
                quizController.gameController.playerProfile.thePlayer.StarPerLevel[quizController.sentenceIndex] = 3;
                quizController.gameController.playerProfile.thePlayer.CountStar();
                GameController.SaveData(quizController.gameController.playerProfile.thePlayer);  
            }

            // 2 bintang
            else if(quizController.timer < 40 && quizController.timer >= 20){
                quizController.resultCanvas.SetActive(true);
                quizController.completedPanel.SetActive(true);
                quizController.threeStar.SetActive(false);
                quizController.twoStar.SetActive(true);
                quizController.oneStar.SetActive(false);
                quizController.Stop = true;
                quizController.timer = -1;
                quizController.Correct();

                if (quizController.gameController.playerProfile.thePlayer.StarPerLevel[quizController.sentenceIndex] <= 2)
                {
                    quizController.gameController.playerProfile.thePlayer.StarPerLevel[quizController.sentenceIndex] = 2;
                }
                
                quizController.gameController.playerProfile.thePlayer.CountStar();
                GameController.SaveData(quizController.gameController.playerProfile.thePlayer);
            }

            //1 bintang
            else if(quizController.timer < 20 && quizController.timer > 0){
                quizController.resultCanvas.SetActive(true);
                quizController.completedPanel.SetActive(true);
                quizController.threeStar.SetActive(false);
                quizController.twoStar.SetActive(false);
                quizController.oneStar.SetActive(true);
                quizController.Stop = true;
                quizController.timer = -1;
                quizController.Correct();

                if (quizController.gameController.playerProfile.thePlayer.StarPerLevel[quizController.sentenceIndex] <= 1)
                {
                    quizController.gameController.playerProfile.thePlayer.StarPerLevel[quizController.sentenceIndex] = 1;
                }
                
                quizController.gameController.playerProfile.thePlayer.CountStar();
                GameController.SaveData(quizController.gameController.playerProfile.thePlayer);
            }
        }
    }
}
