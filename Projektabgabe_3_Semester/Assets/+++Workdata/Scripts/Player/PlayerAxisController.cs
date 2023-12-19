using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAxisController : MonoBehaviour
{
    [SerializeField] private PlayerMover mover;
    [SerializeField] private Bullet bulletPrefab;
    
    private int equippedWeaponIndex = 0;
    /// <summary>
    /// List of equipped Weapons
    /// </summary>
    [SerializeField] private List<Weapon> inventoryList = new List<Weapon>();
    
    private InputHandler input;
    [SerializeField] private InputHandler emptyInput;
    [SerializeField] private InputHandler gameplayInput;

    //events to handle weapon added, weapon removed and weapon switched
    public event Action<Weapon> onWeaponAdded;
    public event Action<Weapon> onWeaponRemoved;
    public event Action<Weapon> onActiveWeaponSwitched;

    private Vector2 inputDirection;
    
    /// <summary>
    /// get the current inventorylist
    /// </summary>
    /// <returns>List of weapons</returns>
    public List<Weapon> GetInventory()
    {
        return inventoryList;
    }

    /// <summary>
    /// get the equipped weapon, if there is no item in the list returns null
    /// </summary>
    /// <returns>List of Weapons</returns>
    public Weapon GetEquippedWeapon()
    {
        if (inventoryList.Count == 0)
            return null;
        
        return inventoryList[equippedWeaponIndex];
    }
    
    /// <summary>
    /// set the player mover and controller, gets the current gamestate from gameloopmanager
    /// </summary>
    private void Awake()
    {
        PlayerManager.SetPlayerMover(mover);
        PlayerManager.SetPlayerController(this);
        GameLoopManager.onGameStateChange += OnGameStateChanged;
        OnGameStateChanged(GameLoopManager.GetGameState());
    }

    /// <summary>
    /// remove the function ongamestatechanged to be fired
    /// </summary>
    private void OnDestroy()
    {
        GameLoopManager.onGameStateChange -= OnGameStateChanged;
    }

    /// <summary>
    /// switch between empty input and the gameplayinput if the gamestate is on playing state or not
    /// </summary>
    /// <param name="newState">gamestate</param>
    private void OnGameStateChanged(GameLoopManager.GameState newState)
    {
        if (newState == GameLoopManager.GameState.Playing)
        {
            input = gameplayInput;
        }
        else
        {
            input = emptyInput;
        }
    }

    /// <summary>
    /// equip weapon if the weapon is not in the list
    /// if you have a weapon equipped, switch to first slot to keep the weapon, if you collect another one
    /// </summary>
    /// <param name="newWeapon">Script Weapon</param>
    public void AddWeapon(Weapon newWeapon)
    {
        if (inventoryList.Contains(newWeapon))
            return;
        inventoryList.Add(newWeapon);
        newWeapon.owner = mover;
        newWeapon.transform.SetParent(this.transform);
        
        if (onWeaponAdded != null)
            onWeaponAdded(newWeapon);

        if (inventoryList.Count == 1)
        {
            if (onActiveWeaponSwitched != null)
                onActiveWeaponSwitched(inventoryList[0]);
        }
    }

    /// <summary>
    /// if there is a weapon, remove the active weapon in the list
    /// if the inventory list has another weapon, switch to the weapon after remove the other one
    /// </summary>
    /// <param name="weapon">Script Weapon</param>
    public void RemoveWeapon(Weapon weapon)
    {
        if (!inventoryList.Contains(weapon))
            return;

        var removedWeaponIndex = inventoryList.IndexOf(weapon);
        if (equippedWeaponIndex >= removedWeaponIndex)
        {
            equippedWeaponIndex--;
            if (equippedWeaponIndex < 0)
                equippedWeaponIndex = 0;
        }
        inventoryList.Remove(weapon);
        
        if (onWeaponRemoved != null)
            onWeaponRemoved(weapon);
        
        if (inventoryList.Count > 0 && onActiveWeaponSwitched != null)
            onActiveWeaponSwitched(inventoryList[equippedWeaponIndex]);
    }

    /// <summary>
    /// if the player gets destroyed, set gamestate to gameover
    /// handles movement and weapons
    /// </summary>
    private void Update()
    {
        if (mover == null)
        {
            GameLoopManager.SetGameState(GameLoopManager.GameState.GameOver);
            enabled = false;
            return;
        }

        UpdateMovement();
        UpdateChangeWeapon();
        UpdateShootWeapon();
        UpdateMoverDirection();
    }
    
    //get the input for movement
    private void UpdateMovement()
    {
        inputDirection = input.GetMoveDirection();
    }

    /// <summary>
    /// if mouse scroll up change the weapon to previous
    /// if mouse scroll down change the weapon to next
    /// if there is no weapon do nothing
    /// </summary>
    private void UpdateChangeWeapon()
    {
        int scrollDirection = 0;
        if (input.GetSwitchToPreviousWeapon())
            scrollDirection = -1;
        if (input.GetSwitchToNextWeapon())
            scrollDirection = 1;
        
        if (scrollDirection != 0)
        {
            equippedWeaponIndex += Mathf.RoundToInt(scrollDirection);
            if (equippedWeaponIndex < 0)
                equippedWeaponIndex = inventoryList.Count - 1;
            if (equippedWeaponIndex >= inventoryList.Count)
                equippedWeaponIndex = 0;
            if (inventoryList.Count == 0)
            {
                return;
            }
            if (onActiveWeaponSwitched != null)
                onActiveWeaponSwitched(inventoryList[equippedWeaponIndex]);
        }
    }

    /// <summary>
    /// get the active weapon in the list
    /// if there is one, start shoot if button was pressed
    /// stop shoot 
    /// </summary>
    private void UpdateShootWeapon()
    {
        if (equippedWeaponIndex >= inventoryList.Count)
            return;
        if (equippedWeaponIndex < 0)
        {
            return;
        }
        Weapon equippedWeapon = inventoryList[equippedWeaponIndex];
        if (equippedWeapon != null && input.GetIsShooting())
        {
            equippedWeapon.StartShooting();
        }
        else if (equippedWeapon != null && !input.GetIsShooting())
        {
            equippedWeapon.StopShooting();
        }
    }

    //sets the look direction the player looks with the shoot direction
    private void UpdateMoverDirection()
    {
        mover.SetLookDirection(input.GetShootDirection());
    }

    //calculate the movement for the player
    private void FixedUpdate()
    {
        if (mover != null)
        {
            mover.MoveInDirection(inputDirection);
        }
    }
}
