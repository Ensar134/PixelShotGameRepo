using System;
using UnityEngine;

public enum Panel
{
    MainMenu,
    InGame,
    Win,
    Lose,
    DailySpin,
    Shop
}

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    public Panel _menu;

    public Action<Panel> changePanel;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeMenu(Panel panel)
    {
        changePanel?.Invoke(panel);
    }
}
