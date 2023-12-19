using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float despawnTime = 5;
    [SerializeField] private int damage = 1;

    public MoverBase shooter;

    private Vector2 direction;

    /// <summary>
    /// sets the shooter to the new shooter
    /// sets the direction of the bullet to the targetedPosition subtract with the position of the shooter
    /// Ignore the collision with the shooter
    /// adding force to the bullet to move it
    /// starts coroutine to despawn the bullet after few seconds
    /// </summary>
    /// <param name="shooter">Script MoverBase</param>
    /// <param name="targetedPosition">Vecotr2</param>
    public void Shoot(MoverBase shooter, Vector2 targetedPosition)
    {
        this.shooter = shooter;
        
        direction = targetedPosition - shooter.GetPosition();
        direction.Normalize();
        
        Physics2D.IgnoreCollision(shooter.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        GetComponent<Rigidbody2D>().AddForce(direction * speed , ForceMode2D.Impulse);
        StartCoroutine(DespawnAfterTimeCoroutine());
    }

    /// <summary>
    /// ignores the collison of the bullet with the same shooter
    /// if the gameobject has HealthpointManager the bullet deals Damage to it and the bullet get destroyed
    /// </summary>
    /// <param name="other">Collision2D</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        Bullet otherBullet = other.gameObject.GetComponent<Bullet>();
        if (otherBullet != null && otherBullet.shooter == shooter)
        {
            Physics2D.IgnoreCollision(shooter.GetComponent<Collider2D>(), other.collider);
            return;
        }

        HealthPointManager target = other.collider.GetComponent<HealthPointManager>();
        if (target != null)
        {
            target.DealDamage(damage, direction);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// waits for the despawntime and then destroys the gameobject
    /// </summary>
    /// <returns>waits for the despawntime in seconds</returns>
    private IEnumerator DespawnAfterTimeCoroutine()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
