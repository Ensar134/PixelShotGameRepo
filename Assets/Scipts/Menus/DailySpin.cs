using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailySpin : MonoBehaviour
{
    public Panel myPanel;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject panelControl;

    private void Awake()
    {
        CanvasManager.instance.changePanel += ChangePanel;
    }

    private void Start()
    {
        backButton.onClick.AddListener(OnClick_Back);
    }

    public void ChangePanel(Panel _panel)
    {
        if (myPanel == _panel)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        panelControl.SetActive(false);
    }

    private void Show()
    {
        panelControl.SetActive(true);
    }

    public void OnClick_Back()
    {
        CanvasManager.instance.ChangeMenu(Panel.MainMenu);
    }
}
