using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Lose : MonoBehaviour
{
    public Panel myPanel;
    public Database db;
    public TextMeshProUGUI levelText;
    [SerializeField] private GameObject panelControl;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        CanvasManager.instance.changePanel += ChangePanel;
        levelText.text = "Level " + (db.currentLevel + 1);
        restartButton.onClick.AddListener(() => OnClick_Restart());
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

    public void OnClick_Restart()
    {
        FindObjectOfType<Database>().GetLevel();
        PlayerPrefs.SetInt("Score", 0);
        CanvasManager.instance.ChangeMenu(Panel.InGame);
    }

    public void OnClick_BackToMenu()
    {
        PlayerPrefs.SetInt("Score", 0);
        CanvasManager.instance.ChangeMenu(Panel.MainMenu);
    }
}
