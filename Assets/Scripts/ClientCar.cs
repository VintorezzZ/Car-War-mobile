using System;
using UnityEngine;

public class ClientCar : BaseCar
{
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     godMode = true;
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     var rcc = GetComponent<RCC_CarControllerV3>();
        //     // rcc.repairNow = true;
        //     // rcc.repaired = false;
        //     // rcc.Repair();
        // }
    }

    protected override void OnDamageTaken(float damage)
    {
        if (HealthComponent.Health == 0)
        {
            GameManager.Instance.OnGameOver();
        }

        base.OnDamageTaken(damage);
    }
}
