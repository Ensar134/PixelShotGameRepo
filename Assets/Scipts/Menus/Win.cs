using UnityEngine;
using TMPro;
using System.Collections;

public class Win : MonoBehaviour
{
    public Panel myPanel;
    public Database db;
    public GameObject map;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private int score;

    [SerializeField] private GameObject panelControl;

    private void Start()
    {
        CanvasManager.instance.changePanel += ChangePanel;
        score = PlayerPrefs.GetInt("Coin");
        levelText.text = "Level " + (db.currentLevel + 1);
        scoreText.text = "SCORE\n" + score.ToString();
        highScoreText.text = PlayerPrefs.GetInt("Score").ToString();
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

    public void NextLevel()
    {
        ChangeLevel();
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Spawner.Instance.NewSpawnRequest();
        CanvasManager.instance.ChangeMenu(Panel.InGame);
    }

    private void ChangeLevel()
    {
        db.currentLevel += 1;
        PlayerPrefs.SetInt("Level", db.currentLevel);
        levelText.text = "Level " + (db.currentLevel + 1);
        db.GetLevel();
    }

    public void OnClick_BackToMenu()
    {
        CanvasManager.instance.ChangeMenu(Panel.MainMenu);
    }

    public void OnClick_SpinCanvas()
    {
        CanvasManager.instance.ChangeMenu(Panel.DailySpin);
    }
}
