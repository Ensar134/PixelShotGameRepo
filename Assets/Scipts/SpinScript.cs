using UnityEngine.UI;
using UnityEngine;
using EasyUI.PickerWheelUI;
using TMPro;
using System;

public class SpinScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiSpinButtonText;
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private GameObject rewardedCanvas;
    [SerializeField] private TextMeshProUGUI rewardAmount;
    [SerializeField] private PickerWheel pickerWheel;
    [SerializeField] private GameObject rewardImage;

    private int diamonds;

    private void Start()
    {
        diamonds = PlayerPrefs.GetInt("Diamond");
        Sprite sticker = Resources.Load<Sprite>("Sticker1");
        Sprite gem = Resources.Load<Sprite>("GEM");

        uiSpinButton.onClick.AddListener(() =>
        {
            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Spinning";

            pickerWheel.OnSpinStart(() => { Debug.Log("Spin Started..."); });

            pickerWheel.OnSpinEnd(WheelPiece =>
            {
                Debug.Log("Spin End: Label:" + WheelPiece.Label + " , Amount:" + WheelPiece.Amount);

                if (WheelPiece.Icon.name == "GEM")
                {
                    rewardedCanvas.SetActive(true);
                    rewardImage.GetComponent<Image>().sprite = gem;

                }
                else if (WheelPiece.Icon.name == "Sticker1")
                {
                    rewardedCanvas.SetActive(true);
                    rewardImage.GetComponent<Image>().sprite = sticker;
                }

                diamonds += int.Parse(WheelPiece.Amount);
                PlayerPrefs.SetInt("Diamond", diamonds);
                rewardAmount.text = WheelPiece.Amount;
                uiSpinButton.interactable = false;
                uiSpinButtonText.fontSize = 20;
                uiSpinButtonText.text = "Come Tomorrow";
            });

            pickerWheel.Spin();
        });
    }

    public void CloseCanvas()
    {
        rewardedCanvas.SetActive(false);
    }

    private void Update()
    {
        
    }
}
