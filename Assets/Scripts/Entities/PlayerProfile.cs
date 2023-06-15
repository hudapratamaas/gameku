using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    private int hintCount;
    private int starCount;
    private List<int> starPerLevel;
    public PlayerProfile thePlayer;
    public int HintCount { get => hintCount; set => hintCount = value; }
    public int StarCount { get => starCount; set => starCount = value; }
    public List<int> StarPerLevel { get => starPerLevel; set => starPerLevel = value; }

    public PlayerProfile(int length)
    {
        hintCount = 3;
        starCount = 0;
        starPerLevel = new List<int>();
        for (int i = 0; i < length; i++)
        {
            starPerLevel.Add(0);
        }
    }

    public int CountStar()
    {
        int tempStar = 0;
        foreach (var starLevel in StarPerLevel)
            {
                tempStar += starLevel;
            }

        StarCount = tempStar;
        return StarCount;
    }
}

