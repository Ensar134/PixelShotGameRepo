using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public Panel inGamePanel;
    public Text currentLevelText;
    public Text nextLevelText;
    public Text coinText;
    public Text diamondText;

    public GameObject map;
    public GameObject hand;
    public GameObject bossLevel;

    private int cLevel;
    private int nLevel;
    private bool controlHand = true;

    [SerializeField] private GameObject panelControl;

    private void Awake()
    {
        CanvasManager.instance.changePanel += ChangePanel;
    }

    public void ChangePanel(Panel _panel)
    {
        if (inGamePanel == _panel)
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

    private void Update()
    {
        diamondText.text = PlayerPrefs.GetInt("Diamond").ToString();
        cLevel = PlayerPrefs.GetInt("Level") + 1;
        nLevel = cLevel + 1;
        currentLevelText.text = cLevel.ToString();
        nextLevelText.text = nLevel.ToString();

        coinText.text = PlayerPrefs.GetInt("Coin").ToString();

        if (Input.GetMouseButtonDown(0))
        {
            hand.SetActive(false);
            controlHand = false;
        }
    }

    public void OpenWinCanvas()
    {
        CanvasManager.instance.ChangeMenu(Panel.Win);
    }

    public void OpenLoseCanvas()
    {
        CanvasManager.instance.ChangeMenu(Panel.Lose);
    }
}
