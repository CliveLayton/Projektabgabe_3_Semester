using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    /// <summary>
    /// get the movedirection/ return Vector2.zero on default
    /// </summary>
    /// <returns>Vector2</returns>
    public virtual Vector2 GetMoveDirection()
    {
        return Vector2.zero;
    }

    /// <summary>
    /// get the input to switch to previous weapon/ return false on default
    /// </summary>
    /// <returns>boolean</returns>
    public virtual bool GetSwitchToPreviousWeapon()
    {
        return false;
    }

    /// <summary>
    /// get the input to switch tp next weapon/ return false on default
    /// </summary>
    /// <returns>boolean</returns>
    public virtual bool GetSwitchToNextWeapon()
    {
        return false;
    }

    /// <summary>
    /// get the input if a gameobject shoots/ return false on default
    /// </summary>
    /// <returns>boolean</returns>
    public virtual bool GetIsShooting()
    {
        return false;
    }

    /// <summary>
    /// get the shoot direction of the input/ return Vector2.up on default
    /// </summary>
    /// <returns>Vector2</returns>
    public virtual Vector2 GetShootDirection()
    {
        return Vector2.up;
    }
}
