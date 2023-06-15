using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text starsText;
    public TextMeshProUGUI hintsText;

    private void start(){
        if(starsText==null || hintsText==null){
            return;
        }
    }
    private void Update()
    {
        if(starsText!=null){
            UpdateStarsUI();
            return;
        }
        if(hintsText!=null){
            UpdateHintsUI();
            return;
        }
    }

    public void UpdateStarsUI()
    {
        starsText.text = PlayerPrefs.GetInt("total_star", 0).ToString();
    }
    public void UpdateHintsUI()
    {
        hintsText.text = PlayerPrefs.GetInt("total_hint", 3).ToString()+"x";
    }
}
