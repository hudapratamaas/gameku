using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public int levelSelected;

    void Start()
    {
        levelSelected = 0;
        PlayerData data = GameController.LoadData();
    }

    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
