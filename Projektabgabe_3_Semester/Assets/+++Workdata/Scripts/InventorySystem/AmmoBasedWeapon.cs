using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBasedWeapon : Weapon
{
    [Header("Ammo Based Weapon")]
    public int bulletsPerMagazine;
    public int currentBullets;

    public float timeBetweenBullets = 0.2f;
    public float reloadTime;

    private bool isShooting = false;

    private float timeSinceLastBullet;
   
    // wait for a time before can shoot another bullet
    // set the time since last bullet to zero after shooted a bullet
    private void Update()
    {
        timeSinceLastBullet += Time.deltaTime;
      
        if (!isShooting)
            return;

        if (timeSinceLastBullet < timeBetweenBullets)
            return;

        timeSinceLastBullet = 0;
        ShootBullet();
    }

    /// <summary>
    /// shoot a bullet
    /// </summary>
    public virtual void ShootBullet()
    {
      
    }

    /// <summary>
    /// shooting is true
    /// </summary>
    public override void StartShooting()
    {
        isShooting = true;
    }

    /// <summary>
    /// shooting is false
    /// </summary>
    public override void StopShooting()
    {
        isShooting = false;
    }

    /// <summary>
    /// reload weapon
    /// </summary>
    public override void Reload()
    {
      
    }
}
