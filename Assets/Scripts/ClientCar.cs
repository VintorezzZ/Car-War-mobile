using UnityEngine;

public class ClientCar : BaseCar
{
    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        GameManager.instance.SetDeathUI();
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
            GetDamage(maxHealth);
        }    
    }
}
