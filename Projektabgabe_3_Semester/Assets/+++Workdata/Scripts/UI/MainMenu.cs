using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        GameLoopManager.SetGameState(GameLoopManager.GameState.MainMenu);
    }

    public void StartGame()
    {
        GameLoopManager.StartNewGame();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
