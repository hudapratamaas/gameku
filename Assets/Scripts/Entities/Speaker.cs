using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    // Setiap speaker memiliki warna yang berbeda
    public string speakerName;
    public Color textColor;
}
