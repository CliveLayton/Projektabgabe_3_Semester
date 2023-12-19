using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public MoverBase owner;
    public string displayName;

    /// <summary>
    /// sets the owner of this Weapon to the new owner with MoverBase Script
    /// </summary>
    /// <param name="owner">Script MoverBase</param>
    public void GetEquipedBy(MoverBase owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// gets the playeraxiscontroller
    /// sets this weapon to the playeraxiscontroller if this is not in the list
    /// </summary>
    private void OnEnable()
    {
        PlayerAxisController controller = GetComponentInParent<PlayerAxisController>();
        if(controller != null)
            controller.AddWeapon(this);
    }

    /// <summary>
    /// gets the playeraxiscontroller
    /// removes this weapon from the playeraxiscontroller if this is in list
    /// </summary>
    private void OnDisable()
    {
        PlayerAxisController controller = GetComponentInParent<PlayerAxisController>();
        if(controller != null)
            controller.RemoveWeapon(this);
    }

    /// <summary>
    /// starts shooting
    /// </summary>
    public virtual void StartShooting()
    {
        
    }
    
    /// <summary>
    /// stops shooting
    /// </summary>
    public virtual void StopShooting()
    {
        
    }

    /// <summary>
    /// reload this weapon
    /// </summary>
    public virtual void Reload()
    {
        
    }
}
