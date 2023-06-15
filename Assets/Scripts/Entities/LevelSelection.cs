using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelSelection : MonoBehaviour
{

    [SerializeField] public bool unlocked = false; ////Default value is false;
    public Sprite lockImage;
    public List<GameObject> levels;
    public Sprite starSprite;
    public GameObject parent; 
    PlayerData data;

    private void Start()
    {
        data = GameController.LoadData();
    }

    private void Update()
    {
        UpdateLevelImage();
    }

    private void UpdateLevelImage()
    {
        foreach (GameObject level in levels)
        {
            if(level.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                level.transform.GetChild(1).gameObject.SetActive(false);
                level.transform.GetChild(2).gameObject.SetActive(false);
                level.transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                level.transform.GetChild(1).gameObject.SetActive(true);
                level.transform.GetChild(2).gameObject.SetActive(true);
                level.transform.GetChild(3).gameObject.SetActive(true);
                for (int i = 0; i < data.starPerLevel[levels.IndexOf(level)]; i++)
                {
                    level.transform.GetChild(i + 1).gameObject.SetActive(true);
                    level.transform.GetChild(i + 1).gameObject.GetComponent<Image>().sprite = starSprite;
                }
            }
        }
    }
}
