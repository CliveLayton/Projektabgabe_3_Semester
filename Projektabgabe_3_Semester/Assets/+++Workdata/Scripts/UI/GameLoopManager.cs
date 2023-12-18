using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public static class GameLoopManager
{
    /// <summary>
    /// enum with different states for the game to switch between
    /// </summary>
    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver,
        InStory
    }

    //variable for the gamestate
    private static GameState state = GameState.Playing;
    //Action Event to trigger switching between states;
    public static event Action<GameState> onGameStateChange;

    //function to get the current gamestate
    public static GameState GetGameState()
    {
        return state;
    }

    /// <summary>
    /// public function to switch between the state from other scripts
    /// </summary>
    /// <param name="newState">the state you want to switch to</param>
    public static void SetGameState(GameState newState)
    {
        state = newState;

        if (newState == GameState.Playing)
        {
            Time.timeScale = 1f;
        }
        else if(newState == GameState.GameOver)
        {
            Time.timeScale = 0f;
        }
      
        if (onGameStateChange != null)
            onGameStateChange(state);
    }

    /// <summary>
    /// loads scene 1 (game scene) and set gamestate to playing
    /// </summary>
    public static void StartNewGame()
    {
        SceneManager.LoadScene(1);
        SetGameState(GameState.Playing);
    }

    /// <summary>
    /// loads scene 0 (maine menu) and set gamestate to main menu
    /// </summary>
    public static void EnterMainMenu()
    {
        SceneManager.LoadScene(0);
        SetGameState(GameState.MainMenu);
    }
}
