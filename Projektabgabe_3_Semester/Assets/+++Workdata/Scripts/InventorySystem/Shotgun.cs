using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shotgun : AmmoBasedWeapon
{
    [Header("Shotgun")]
    [SerializeField] private int bulletsPerShot = 12;
    [SerializeField] private float sprayAngle = 45;
    [SerializeField] private Bullet shotgunBullet;

    /// <summary>
    /// instantiate 12 bullets on the right position
    /// set the spray angle to instantiate the bullets
    /// rotate the bullets to the spray angle
    /// add force to the bullets and shoot them
    /// </summary>
    public override void ShootBullet()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Bullet newBullet = Instantiate(shotgunBullet, owner.GetPosition(), Quaternion.identity);

            Vector2 bulletDirection = owner.GetLookDirection();
            float wantedAngle = Random.Range(-sprayAngle, sprayAngle);

            bulletDirection = Rotate(bulletDirection, wantedAngle / 180);
            newBullet.Shoot(owner, owner.GetPosition() + bulletDirection);
        }

       
    }
    
    /// <summary>
    /// rotation math for the spray angle
    /// </summary>
    /// <param name="v">Vector2</param>
    /// <param name="delta">float</param>
    /// <returns></returns>
    public static Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta));
    }
}
