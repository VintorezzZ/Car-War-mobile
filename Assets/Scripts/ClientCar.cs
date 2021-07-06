using System;
using UnityEngine;

public class ClientCar : BaseCar
{
    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        GameManager.Instance.OnGameOver();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            if(godMode)
                return;
            
            GetDamage(maxHealth);
        }
        else if (collision.transform.TryGetComponent(out EntityCar entityCar))
        {
            entityCar.GetDamage(maxHealth);
            
            if(godMode)
                return;
            
            GetDamage(maxHealth);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            godMode = true;
        }
    }
}
