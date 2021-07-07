using System;
using UnityEngine;

public class ClientCar : BaseCar
{
    public override void GetDamage(BaseCar player, Collision collision)
    {
        base.GetDamage(player, collision);
        
        if (Health == 0)
        {
            GameManager.Instance.OnGameOver();   
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            if(godMode)
                return;
            
            GetDamage(this, collision);
        }
        else if (collision.transform.TryGetComponent(out EntityCar entityCar))
        {
            
            
            //entityCar.GetDamage(this, collision);
            
            if(godMode)
                return;
            
            GetDamage(this, collision);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            godMode = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var rcc = GetComponent<RCC_CarControllerV3>();
            // rcc.repairNow = true;
            // rcc.repaired = false;
            // rcc.Repair();
        }
    }
}
