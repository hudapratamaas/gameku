using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizTypes : MonoBehaviour
{
    [SerializeField] Button[] buttonPilihanPilgans;
    [SerializeField] public TMP_InputField jawabanMengisi;
    public QuizController quizController;
    private Quiz theQuiz;
    // Jenis Kuis
    public enum QuizType
    {
        Pilgan,
        Menyusun,
        Mengisi,
    }
    [SerializeField] private float baseDamage = 5f;
    private float damage;
    public float Damage { get => damage; set => damage = value; }
    
    private void OnEnable(){
        damage = baseDamage;
    }

    // 2.b.iii Menampilkan opsi jawaban sesuai jenis soal
    public void checkQuizType(Quiz quiz){
        theQuiz = quiz;
        if(quiz.quizzes.quizType==QuizType.Pilgan){
            // Debug.Log(quiz.quizzes.quizType==QuizType.Pilgan);
            for (int i=0; i<buttonPilihanPilgans.Length; i++){
                buttonPilihanPilgans[i].GetComponentInChildren<TextMeshProUGUI>().text = quiz.quizzes.pilihanPilgan[i];
            }
            foreach (var buttonPilihanPilgan in buttonPilihanPilgans){
                buttonPilihanPilgan.onClick.RemoveAllListeners();
                buttonPilihanPilgan.onClick.AddListener(()=>{
                    
                    // 3 bintang
                    if(quizController.timer >= 40 && Equals(quiz.quizzes.answer, buttonPilihanPilgan.GetComponentInChildren<TextMeshProUGUI>().text)){
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
                    else if(quizController.timer < 40 && quizController.timer >= 20 && Equals(quiz.quizzes.answer, buttonPilihanPilgan.GetComponentInChildren<TextMeshProUGUI>().text)){
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
                    else if(quizController.timer < 20 && quizController.timer > 0 && Equals(quiz.quizzes.answer, buttonPilihanPilgan.GetComponentInChildren<TextMeshProUGUI>().text)){
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
                    else if(Equals(quiz.quizzes.answer, buttonPilihanPilgan.GetComponentInChildren<TextMeshProUGUI>().text)==false){
                        quizController.timer -= damage;
                        quizController.Wrong();
                        return;
                    }
                });
            }
        }
        else if(quiz.quizzes.quizType==QuizType.Menyusun){
            KataManager.Instance.InitKata(quiz.quizzes.answer);
        }
    }
    public void ClearButton(){      
        jawabanMengisi.text = "";
    }
    public void SubmitButton(){
        if(theQuiz != null){
            if(theQuiz.quizzes.answer.ToString().ToLower() == jawabanMengisi.text.ToString().ToLower()){
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
            else{
                quizController.timer -= damage;
                quizController.Wrong();
                return;
            }
        }
    }
}
