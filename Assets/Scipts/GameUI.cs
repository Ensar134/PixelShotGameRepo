using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject map;

    private void Update()
    {
        if(PlayerPrefs.GetInt("Life") == 0)
        {
            OpenWinCanvas();
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
