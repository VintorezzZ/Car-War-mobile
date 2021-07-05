 using UnityEngine;

 public class EntityCar : BaseCar
 {
     [SerializeField] private int pointsToAdd;
     public override void GetDamage(int damage)
     {
         GameManager.Instance.OnDestroyEnemy(pointsToAdd);
         base.GetDamage(damage);
     }
     
     private void OnCollisionEnter(Collision collision)
     {
         if (collision.transform.CompareTag("Wall"))
         {
             GetDamage(maxHealth);
         }
         else if (collision.transform.TryGetComponent(out EntityCar entityCar))
         {
             entityCar.GetDamage(maxHealth);
         }    
     }
 }
