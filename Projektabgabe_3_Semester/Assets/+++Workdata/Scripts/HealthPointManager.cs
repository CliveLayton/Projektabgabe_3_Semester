using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPointManager : MonoBehaviour
{
   [SerializeField] private int maximumHealthPoints = 10;
   [SerializeField] private GameObject splashVFXPrefab;
   private int currentHealthPoints;

   //set currenthealth to the maximumhealth on awake
   private void Awake()
   {
      currentHealthPoints = maximumHealthPoints;
   }

   /// <summary>
   /// subtract the damageamount from the currenthealth, if currenthealth 0 destroy the gameobject
   /// spawn blood particle on the other side of the gameobject
   /// </summary>
   /// <param name="amount">damageamount</param>
   /// <param name="damageDirection">direction where the bullet comes from</param>
   public void DealDamage(int amount, Vector2 damageDirection)
   {
      SpawnSplash(damageDirection);
        
      currentHealthPoints -= amount;
      if (currentHealthPoints <= 0)
      {
         Destroy(gameObject);
      }
   }

   //instantiate  bloodpartcicles on the opposite where the bullet comes from
   private void SpawnSplash(Vector2 direction)
   {
      if (splashVFXPrefab == null)
         return;
        
      Quaternion splashRotation = Quaternion.LookRotation(direction, Vector3.back);
      Instantiate(splashVFXPrefab, transform.position, splashRotation);
   }
}
