using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemieByCheck : MonoBehaviour
{
    [SerializeField] private MoverBase enemyPrefab;
    [SerializeField] private SpawnPoint spawnPoint;

    //if the player enters the trigger, spawn enemy on the spawnpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawnPoint.TrySpawnObject(enemyPrefab);
            Destroy(gameObject);
        }
    }
}
