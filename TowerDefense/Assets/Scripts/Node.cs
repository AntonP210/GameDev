using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor, busyColor;
    private Color startColor;
    Transform location;

    [HideInInspector]
    private GameObject turret;

    [HideInInspector]
    public TurretBluePrint turretBluePrint;

    [HideInInspector]
    public int upgradeLevel = 0;
    BuildManager buildManager;
    public Vector3 positionOffset;
    private Renderer rend;



    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //if UI ELEMENT IS BLOCKING
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild) //If all the requirements set for the turret building
        {
            return;
        }
        BuildTurret(buildManager.GetTurretToBuild());

    }

    void OnMouseEnter() //changes the color of the selected node
    {
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        SetColorToNormal();
    }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        location = GetComponent<Transform>();
        buildManager = BuildManager.instance;
        startColor = rend.material.color;
    }
    private void HideNode()//when turret built node element is hidden
    {
        location.gameObject.GetComponent<Renderer>().enabled = false;
    }
    private void RestoreNode()//when turret is sold return the node to its place
    {
        location.gameObject.GetComponent<Renderer>().enabled = true;
    }


    public void BuildTurret(TurretBluePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        PlayerStats.Money -= blueprint.cost;

        GameObject _turret;
        turretBluePrint = blueprint;

        _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), transform.rotation);
        turret = _turret;

        GameObject effect;
        effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), transform.rotation);
        HideNode();
        Destroy(effect, 5f);


    }
    public void UpgradeTurret()
    {
        //upgradeLevel

        if (upgradeLevel < turretBluePrint.upgradePrice.Length)
        {
            if (PlayerStats.Money < turretBluePrint.upgradePrice[upgradeLevel])
            {
                Debug.Log("Not enough money to upgrade that!");
                return;
            }
            else
            {
                PlayerStats.Money -= turretBluePrint.upgradePrice[upgradeLevel];

                //get rid of the old turret
                Destroy(turret);

                // //build a new upgraded turret
                GameObject _turret;
                _turret = (GameObject)Instantiate(turretBluePrint.upgrades[upgradeLevel], GetBuildPosition(), transform.rotation);
                turret = _turret;

                //add visual effect of building
                GameObject effect;
                effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), transform.rotation);
                Destroy(effect, 5f);

                upgradeLevel++;
            }
        }
    }

    public void SellTurret()
    {
        PlayerStats.Money += GetSellAmount();

        GameObject effect;
        effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), transform.rotation);
        Destroy(effect, 5f);

        Destroy(turret);

        turretBluePrint = null;
        RestoreNode();
        Debug.Log("Tower Sold!");
    }

    public int GetSellAmount()
    {
        if (upgradeLevel == 0)
        {
            return turretBluePrint.cost / 2;
        }
        else
        {
            return turretBluePrint.upgradePrice[1] / 2;
        }
    }

    public int GetUpgradeAmount()
    {
        if (upgradeLevel == 0)
        {
            return turretBluePrint.upgradePrice[0];
        }
        if (upgradeLevel == 2)
        {

            Debug.Log("Max upgrade");
            return 0;
        }
        else
        {
            return turretBluePrint.upgradePrice[1];
        }
    }

    public Vector3 GetBuildPosition() //sets the turret in the correct position (centers it out)
    {
        return transform.position + positionOffset;
    }
    public void SetColorToNormal()
    {
        rend.material.color = startColor;
    }


}
