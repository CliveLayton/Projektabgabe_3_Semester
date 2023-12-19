using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouseAndKeyboard : InputHandler
{
    /// <summary>
    /// overrides the default moveDirection from InputHandler to current input
    /// </summary>
    /// <returns>Vector2</returns>
    public override Vector2 GetMoveDirection()
    {
        Vector2 movementDirection;
        movementDirection.x = Input.GetAxis("Horizontal");
        movementDirection.y = Input.GetAxis("Vertical");
        movementDirection.Normalize();
        return movementDirection;
    }

    /// <summary>
    /// overrides the default bool if the mousescroller was scrolled to switch to previous weapon
    /// </summary>
    /// <returns>boolean</returns>
    public override bool GetSwitchToPreviousWeapon()
    {
        return Input.mouseScrollDelta.y > 0;
    }

    /// <summary>
    /// overrides the default bool if the mousescroller was scrolled to switch to next weapon
    /// </summary>
    /// <returns>boolean</returns>
    public override bool GetSwitchToNextWeapon()
    {
        return Input.mouseScrollDelta.y < 0;
    }

    /// <summary>
    /// overrides the default bool if the left mouse button is clicked to shoot or not
    /// </summary>
    /// <returns>boolean</returns>
    public override bool GetIsShooting()
    {
        return Input.GetMouseButton(0);
    }

    /// <summary>
    /// sets the shootDirection to the position of the mouse
    /// gets the mover from the playermanager
    /// </summary>
    /// <returns>Vector2</returns>
    public override Vector2 GetShootDirection()
    {
        MoverBase mover = PlayerManager.GetPlayerMover();
        
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint((Input.mousePosition));
        Vector2 moverToMouse = worldMousePosition - mover.GetPosition();
        moverToMouse.Normalize();
        return moverToMouse;
    }
}
