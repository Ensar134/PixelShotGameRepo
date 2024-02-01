using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Panel myPanel;

    [SerializeField] private Button backButton;
    [SerializeField] private GameObject panelControl;
    [SerializeField] private TextMeshProUGUI diamondText;

    private void Awake()
    {
        backButton.onClick.AddListener(OnClick_Back);
    }

    private void Start()
    {
        CanvasManager.instance.changePanel += ChangePanel;
    }

    private void Update()
    {
        diamondText.text = PlayerPrefs.GetInt("Diamond").ToString();
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
