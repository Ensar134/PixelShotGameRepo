using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    public Panel mainMenuPanel;
    public TextMeshProUGUI highScore;
    [SerializeField] private Button playButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button dailySpinButton;
    [SerializeField] private Text diamondText;
    [SerializeField] private GameObject panelControl;

    private void Awake()
    {
        CanvasManager.instance.changePanel += ChangePanel;
        playButton.onClick.AddListener(OnClick_Play);
        dailySpinButton.onClick.AddListener(OnClick_SpinCanvas);
        shopButton.onClick.AddListener(OnClick_Shop);
    }

    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void ChangePanel(Panel _panel)
    {
        
        if (mainMenuPanel == _panel)
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

    public void OnClick_Play()
    {
        Spawner.Instance.NewSpawnRequest();
        FindObjectOfType<Database>().GetLevel();
        CanvasManager.instance.ChangeMenu(Panel.InGame);
    }

    private void OnClick_SpinCanvas()
    {
        CanvasManager.instance.ChangeMenu(Panel.DailySpin);
    }

    private void OnClick_Shop()
    {
        CanvasManager.instance.ChangeMenu(Panel.Shop);
    }

    private void Update()
    {
        diamondText.text = PlayerPrefs.GetInt("Diamond").ToString();
    }
}
