using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerShopCanvas : MonoBehaviour
{
    [SerializeField] private StickerItem[] stickerItems;
    [SerializeField] private StickerPrefab stickerPrefab;
    [SerializeField] private Transform content;

  
    private List<StickerPrefab> createdStickers = new List<StickerPrefab>();
 
    private void Start()
    {
        CreateStickerItems();
    }

    private void CreateStickerItems()
    {
        for (int i = 0; i < stickerItems.Length; i++)
        {
            StickerPrefab sticker = Instantiate(stickerPrefab, content);
            int stickerItem = stickerItems[i].stickerId;
            sticker.SetData(stickerItems[i], ChangePlayerMaterial);
            createdStickers.Add(sticker);
        }
    }  

    private void ChangePlayerMaterial(StickerItem item)
    {
        ChangeStickerItem();
        GameObject prefab = Resources.Load("Player") as GameObject;
        prefab.GetComponent<Renderer>().sharedMaterial = item.stickerMaterial;
    }


    private void ChangeStickerItem()
    {
        foreach (var item in createdStickers)
        {
            item.UpdateStickerHolder();
        }
    }
}
