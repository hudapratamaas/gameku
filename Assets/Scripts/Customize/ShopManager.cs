using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int bintang;
    public int showBintang;
    public ShopItemSO[] shopItemSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] PurchaseBtn;
    public int[] skinUnlocked = new int[8];
    public Text starsText;
    public GameObject skinList;
    public GameObject equipList;
    
    void Start()
    {
        if(PlayerPrefs.GetInt("equip_baju", -1 )!=-1){
            Equip(PlayerPrefs.GetInt("equip_baju"));
        }
        if(PlayerPrefs.GetInt("equip_acc", -1 )!=-1){
            Equip(PlayerPrefs.GetInt("equip_acc"));
        }
        for (int i = 0; i < skinUnlocked.Length; i++)
        {
            skinUnlocked[i] = PlayerPrefs.GetInt("skin_" + i, 0);
            if (skinUnlocked[i]==1)
            {
                skinList.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                skinList.transform.GetChild(i).GetChild(6).gameObject.SetActive(false);
            }
        }
        bintang = PlayerPrefs.GetInt("total_star", 0);
        for (int i = 0; i < shopItemSO.Length; i++)
            shopPanelsGO[i].SetActive(true);
        LoadPanels();
    }

    void Update(){
        showBintang = bintang;
        for (int i = 0; i < skinUnlocked.Length; i++)
        {
            skinUnlocked[i] = PlayerPrefs.GetInt("skin_" + i, 0);
            if (skinUnlocked[i]==1)
            {
                showBintang-=shopItemSO[i].harga;
            }
        }
        starsText.text = showBintang.ToString();
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if(showBintang >= shopItemSO[i].harga)
                PurchaseBtn[i].interactable = true;
            else
                PurchaseBtn[i].interactable = false;
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (showBintang >= shopItemSO[btnNo].harga)
        {
            CheckPurchaseable();
            PlayerPrefs.SetInt("skin_" + btnNo, 1);
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].namaTxt.text = shopItemSO[i].nama;
            shopPanels[i].hargaTxt.text = shopItemSO[i].harga.ToString();
            shopPanels[i].gambar.sprite = shopItemSO[i].artwork;
        }
    }

    public void Equip(int index){
        for (int i = 0; i < equipList.transform.childCount; i++)
        {
            if (i == index)
            {
                equipList.transform.GetChild(index).gameObject.SetActive(true);
                skinList.transform.GetChild(index).GetChild(2).gameObject.SetActive(true);
                skinList.transform.GetChild(index).GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                if((index%2==0 && i%2==0) || (index%2==1 && i%2==1)){
                    Unequip(i);
                }
            }
        }
        if(index%2==0)
            PlayerPrefs.SetInt("equip_baju", index);
        else
            PlayerPrefs.SetInt("equip_acc", index);
    }
    public void Unequip(int index){
        equipList.transform.GetChild(index).gameObject.SetActive(false);
        skinList.transform.GetChild(index).GetChild(2).gameObject.SetActive(false);
        skinList.transform.GetChild(index).GetChild(3).gameObject.SetActive(true);
        if(index%2==0)
            PlayerPrefs.SetInt("equip_baju", -1);
        else
            PlayerPrefs.SetInt("equip_acc", -1);
    }
}
