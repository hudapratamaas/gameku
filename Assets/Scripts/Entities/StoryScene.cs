using System.Collections.Generic;
using UnityEngine;

// Membuat scriptable object untuk menampung speaker dan scenes
[CreateAssetMenu(fileName = "NewStoryScene", menuName ="Data/New Story Scene")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    // Setiap scene memiliki background dan list kalimat beserta nama speaker
    public List<Sentence> sentences;
    public Sprite background;
    public AudioClip music;
    public StoryScene nextScene;

    [System.Serializable]
    public struct Sentence
    {
        public Speaker speaker;
        [TextArea(3,5)]
        public string text;
    }
}
