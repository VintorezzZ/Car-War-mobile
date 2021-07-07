 using UnityEngine;

 public class EntityCar : BaseCar
 {
     [SerializeField] private int pointsToAdd;
     public override void GetDamage(BaseCar player, Collision collision)
     {
         base.GetDamage(player, collision);
     }
     
     private void OnCollisionEnter(Collision collision)
     {
         if (collision.transform.CompareTag("Wall"))
         {
             GetDamage(this, collision);
         }
         else if (collision.transform.TryGetComponent(out EntityCar entityCar))
         {
             GetDamage(this, collision);
             //entityCar.GetDamage();
             
             // if (entityCar.Health == 0)
             // {
             //     GameManager.Instance.OnDestroyEnemy(pointsToAdd);
             // }
         }    
     }
 }
