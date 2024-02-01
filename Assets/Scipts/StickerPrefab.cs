using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public enum StickerCase
{
    purchaseble,
    selected,
    unselected
}
public class StickerPrefab : MonoBehaviour
{
    private int stickerId;
    private string itemKey = "item";
    private string lastSelectedKey = "lastSelectedItem";

    [Header("Text Header")]
    [SerializeField] private TextMeshProUGUI buyHolderTxt;
    [SerializeField] private TextMeshProUGUI useHolderTxt;

    [Header("Gameobject Header")]
    [SerializeField] private GameObject buyHolder;
    [SerializeField] private GameObject useHolder;

    [SerializeField] private Image stickerImage;
    [SerializeField] private Button buttonHolder;

    private Action<StickerItem> _itemSelected;
    private StickerItem _itemData;

    public void SetData(StickerItem data, Action<StickerItem> itemSelected)
    {
        _itemData = data;


        buyHolderTxt.text = _itemData.stickerCost.ToString();
        stickerImage.sprite = _itemData.stickerSprite;
        stickerId = _itemData.stickerId;

        buttonHolder.onClick.AddListener(TappedButton);
        UpdateStickerHolder();

        _itemSelected = itemSelected;
    }

    public void TappedButton()
    {
        if (CheckBuyItem(stickerId))
        {
            Select();
        }
        else
        {
            Buy();
        }
    }   

    public void UpdateStickerHolder()
    {
        if (CheckBuyItem(stickerId))
        {
            if (CheckSelectedItem(stickerId))
            {
                buyHolder.SetActive(false);
                useHolder.SetActive(true);
                useHolderTxt.text = "USING";
                var color = buttonHolder.GetComponent<Button>().colors;
                color.pressedColor = Color.green;
                color.selectedColor = Color.green;
                buttonHolder.GetComponent<Button>().colors = color;
            }
            else
            {
                buyHolder.SetActive(false);
                useHolder.SetActive(true);
                useHolderTxt.text = "USE";
                var color = buttonHolder.GetComponent<Button>().colors;
                color.pressedColor = Color.yellow;
                color.selectedColor = Color.green;
                buttonHolder.GetComponent<Button>().colors = color;
            }
        }
        else
        {
            buyHolder.SetActive(true);
            useHolder.SetActive(false);
        }
    }

    public void Buy()
    {
        if (PlayerPrefs.GetInt("Diamond") > _itemData.stickerCost)
        {
            PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") - _itemData.stickerCost);
            SetBuyItem(stickerId);
            Select();
        }
    }

    public void Select()
    {
        SetLastSelectedItem(stickerId);
        _itemSelected?.Invoke(_itemData);
    }

    public void SetBuyItem(int itemId)
    {
        PlayerPrefs.SetInt(itemKey + itemId, 1);

    }
    public bool CheckBuyItem(int itemId)
    {
        return PlayerPrefs.GetInt(itemKey + itemId) == 1;
    }

    public bool CheckSelectedItem(int itemId)
    {
        return PlayerPrefs.GetInt(lastSelectedKey, -1) == itemId;
    }

    public void SetLastSelectedItem(int itemId)
    {
        PlayerPrefs.SetInt(lastSelectedKey , itemId);
    }
}
