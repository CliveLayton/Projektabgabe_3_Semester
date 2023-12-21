using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private MoverBase enemyPrefab;
    [SerializeField] private List<SpawnPoint> spawnPoints;
    private BoxCollider2D col;

    [Range(0.01f, 1.0f)]
    [SerializeField] private float intervalMultiplier;

    //get the boxcollider on the gameobject
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    //if player enters the boxcollider, disable collider
    //start SpawnEnemy Coroutine
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            col.enabled = false;
            StartCoroutine(SpawnEnemyCoroutine());
        }
    }

    //get a random spawnpoint in the list, if tryspawnobject returns true, wait for the spawninterval
    //multiply it with the intervalmultiplier to decrease it, do the same again and again (while true)
    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            SpawnPoint point = spawnPoints[Random.Range(0, spawnPoints.Count)];
            if (point.TrySpawnObject(enemyPrefab))
            {
                yield return new WaitForSeconds(spawnInterval);
                spawnInterval *= intervalMultiplier;
            }
            yield return null;
        }
    }
}
