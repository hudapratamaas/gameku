using System.Collections.Generic;
using UnityEngine;

// Membuat scriptable object untuk quiz
[CreateAssetMenu(fileName = "NewQuizScene", menuName ="Data/New Quiz Scene")]
[System.Serializable]
public class Quiz : ScriptableObject
{
    // Setiap quiz memiliki jenis quiz, pertanyaan, dan jawaban yang benar
    public Quizzes quizzes;
    [System.Serializable]
    public struct Quizzes
    {
        public string Name;
        public QuizTypes.QuizType quizType;
        [TextArea(3,5)]
        public string question;
        public Sprite questionImage;
        public List<string> pilihanPilgan;
        [TextArea(3,5)]
        public string hint;
        public string answer;
    }
}
