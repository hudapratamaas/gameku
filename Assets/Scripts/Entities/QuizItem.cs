using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizItem : MonoBehaviour
{
    public string question;
    public QuizType questionType;
    public string answer;
}
// Jenis Kuis
public enum QuizType
{
    Pilgan,
    Menyusun,
    Mengisi,
}
