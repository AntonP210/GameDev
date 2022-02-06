using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint Balista;
    public TurretBluePrint Cannon;
    public TurretBluePrint Pyro;


    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;

    }
    public void PurchaseBalista()
    {
        buildManager.SetTurretToBuild(Balista);
    }

    public void PurchaseCannon()
    {
        buildManager.SetTurretToBuild(Cannon);
    }

    public void PurchasePyro()
    {
        buildManager.SetTurretToBuild(Pyro);
    }

    
    
}
