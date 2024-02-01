using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "NewShopItem/NewSticker")]
public class StickerItem : ScriptableObject
{
    public int stickerId;
    public Material stickerMaterial;
    public int stickerCost;
    public Sprite stickerSprite;
}
