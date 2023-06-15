using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Object/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string id;
    public string nama;
    public int harga;
    public Sprite artwork;
}
