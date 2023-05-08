using infrastructure.Service;
using Services;

public class ClientCar : BaseCar
{
    private IInputService _input;

    [Inject]
    private void Inject(IInputService inputService)
    {
        _input = inputService;
    }

    protected override void Awake()
    {
        base.Awake();

        //_input = ServicesLocator.Container.Resolve<IInputService>();
    }

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
