 using UnityEngine;

 public class EntityCar : BaseCar
 {
     [SerializeField] private int pointsToAdd;

     protected override void OnDamageTaken(float damage)
     {
         if (HealthComponent.Health == 0)
         {
             GameManager.Instance.OnDestroyEnemy(pointsToAdd);
         }

         base.OnDamageTaken(damage);
     }
 }
