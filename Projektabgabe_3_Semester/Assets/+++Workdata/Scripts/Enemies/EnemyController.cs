using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Mover mover;
    [SerializeField] private EnemyAnimator animator;
    [SerializeField] private float preferredDistanceToPlayer = 4;
    [SerializeField] private float targetingTime = 1;

    [SerializeField] private Bullet bulletPrefab;

    private bool isTargetingPlayer = false;

    //activate ongamestatechanged function
    private void Awake()
    {
        GameLoopManager.onGameStateChange += OnGameStateChanged;
    }

    //sets the gameobject off if the gamestate is not playing
    private void OnGameStateChanged(GameLoopManager.GameState newState)
    {
        if (newState == GameLoopManager.GameState.Playing)
            enabled = true;
        else
            enabled = false;
    }

    //if the enemy is near to the player stops movement and start targeting player
    //if the distance is to big, sets the movement target to player
    private void Update()
    {
        if (isTargetingPlayer)
        {
            return;
        }
        
        float distanceToPlayer = Vector2.Distance(mover.GetPosition(), PlayerManager.GetPlayerMover().GetPosition());

        if (distanceToPlayer <= preferredDistanceToPlayer)
        {
            StartCoroutine(TargetAndShootPlayerCoroutine());
            mover.CancelMovement();
        }
        else
        {
            mover.SetMovementTarget(PlayerManager.GetPlayerMover().GetPosition());
        }
    }

    //waits for targeting time and shoots a bullet in direction of the player
    private IEnumerator TargetAndShootPlayerCoroutine()
    {
        isTargetingPlayer = true;
        animator.PlayTargetingAnimation(targetingTime);
        
        yield return new WaitForSeconds(targetingTime);
        
        Bullet newBullet = Instantiate(bulletPrefab, mover.GetPosition(), Quaternion.identity);
        newBullet.Shoot(mover, PlayerManager.GetPlayerMover().GetPosition());

        isTargetingPlayer = false;
    }

    //if Enemy gets destroyed increase the score and deactivate ongamestatechange function
    private void OnDestroy()
    {
        UIController.instance.IncreaseScore(1);
        GameLoopManager.onGameStateChange -= OnGameStateChanged;
    }
}
