using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    private static MoverBase playerMover;
    private static MoverBase emptyMover;
    
    private static PlayerAxisController playerController;

    /// <summary>
    /// sets the playermover to the newMover
    /// </summary>
    /// <param name="newMover">Script MoverBase</param>
    public static void SetPlayerMover(MoverBase newMover)
    {
        playerMover = newMover;
    }

    /// <summary>
    /// sets the playercontroller to the new controller
    /// </summary>
    /// <param name="controller">Script PlayerAxisController</param>
    public static void SetPlayerController(PlayerAxisController controller)
    {
        playerController = controller;
    }

    /// <summary>
    /// get the current player controller type
    /// </summary>
    /// <returns>playercontroller</returns>
    public static PlayerAxisController GetPlayerController()
    {
        return playerController;
    }

    /// <summary>
    /// get the current player mover if there is one or an empty mover
    /// </summary>
    /// <returns>player mover or an empty mover</returns>
    public static MoverBase GetPlayerMover()
    {
        if (playerMover != null)
        {
            return playerMover;
        }

        if (emptyMover == null)
        {
            emptyMover = new GameObject("Empty Mover").AddComponent<MoverBase>();
        }

        return emptyMover;
    }
}
