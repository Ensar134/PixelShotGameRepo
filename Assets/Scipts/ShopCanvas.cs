using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShopCanvas : MonoBehaviour
{
    public TextMeshProUGUI diamondAmount;
    public StickerItem[] stickerItems;
    public TextMeshProUGUI[] priceText;
    public List<int> skinList = new List<int>();

    [Header("Button Objects")]
    public Button[] stickerButtons;

    private void Start()
    {
        skinList.Add(0);

        priceText[PlayerPrefs.GetInt("CurrentSticker")].text = "USING";
        RemoveDiamondImage(PlayerPrefs.GetInt("CurrentSticker"));
        skinList.Add(PlayerPrefs.GetInt("CurrentSticker"));

        ChangeButtonSettings();
    }

    private void Update()
    {
        diamondAmount.text = PlayerPrefs.GetInt("Diamond").ToString();
        priceText[PlayerPrefs.GetInt("UnlockedStickers")].text = "USE";
    }

    private void ChangeButtonSettings()
    {
        for (int i=0; i < stickerButtons.Length; i++)
        {
            int closureIndex = i;
            stickerButtons[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
        }
    }

    public void TaskOnClick(int buttonIndex)
    {
        RemoveDiamondImage(buttonIndex);

        stickerButtons[0].GetComponent<Image>().color = Color.yellow;
        PlayerPrefs.SetInt("ClickedSticker", buttonIndex);
        ChangeTexts();
    }

    private void ChangeTexts()
    {
        GameObject prefab = Resources.Load("Player") as GameObject;
        prefab.GetComponent<Renderer>().sharedMaterial = stickerItems[skinList[skinList.Count - 1]].stickerMaterial;

        PlayerPrefs.SetInt("CurrentSticker", skinList[skinList.Count - 1]);
        skinList.Add(PlayerPrefs.GetInt("CurrentSticker"));
        PlayerPrefs.Save();

        foreach (int values in skinList)
        {
            PlayerPrefs.SetInt("UnlockedStickers" + values, skinList[values]);
            if (skinList[values] == PlayerPrefs.GetInt("CurrentSticker"))
            {     
                priceText[PlayerPrefs.GetInt("CurrentSticker")].text = "USING";
            }
            else
            {
                //priceText[skinList[i]].text = "USE";
                priceText[PlayerPrefs.GetInt("UnlockedStickers")].text = "USE";
            }
            Debug.Log(PlayerPrefs.GetInt("UnlockedStickers"));
        }

        CheckList();
    }

    private void CheckList()
    {
        if (skinList.Contains(PlayerPrefs.GetInt("ClickedSticker")))
        {
            skinList.Remove(PlayerPrefs.GetInt("ClickedSticker"));
            skinList.Add(PlayerPrefs.GetInt("ClickedSticker"));
        }
        else
        {
            skinList.Add(PlayerPrefs.GetInt("ClickedSticker"));
        }
    }

    private void RemoveDiamondImage(int index)
    {
        try
        {
            priceText[index].GetComponentInChildren<Image>().gameObject.SetActive(false);
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine("{0} Exeption cought.", e);
        }
    }
}
