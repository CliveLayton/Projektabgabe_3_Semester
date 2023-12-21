using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float radius = 1;

    /// <summary>
    /// spawn the enemy prefab on the position of the spawner if no other collider hits the spawner
    /// </summary>
    /// <param name="prefab">Script MoverBase</param>
    /// <returns></returns>
    public bool TrySpawnObject(MoverBase prefab)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero);

        if (hit.collider != null)
        {
            return false;
        }

        Instantiate(prefab, transform.position, Quaternion.identity);

        return true;
    }

    //draws a wiresphere in unity to show the size of the spawner
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
