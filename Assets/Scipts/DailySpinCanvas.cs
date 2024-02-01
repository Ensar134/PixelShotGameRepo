using UnityEngine;

public class DailySpinCanvas : MonoBehaviour
{
    public Panel myPanel;

    private void Start()
    {
        CanvasManager.instance.changePanel += ChangePanel;
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
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    public void OpenMainMenu()
    {
        CanvasManager.instance.ChangeMenu(Panel.MainMenu);
    }
}
