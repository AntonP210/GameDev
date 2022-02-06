using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public Button upgradeButton;
    public TextMeshProUGUI SellCost, UpgradeCost;
    private Node target;



    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();
        if (target.upgradeLevel == target.turretBluePrint.upgradePrice.Length)
        {
            //upgraded to max
            UpgradeCost.gameObject.SetActive(false);
            SetTransparency(0.4f);

        }
        else
        {

        }
        SellCost.text = target.GetSellAmount().ToString();
        UpgradeCost.text = target.GetUpgradeAmount().ToString();
        ui.SetActive(true);
    }
    public void Hide()
    {
        ui.SetActive(false);
    }


    public void Upgrade()
    {
        target.UpgradeTurret();

        BuildManager.instance.DeselectNode();
    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
    public void SetTransparency(float transparency)
    {
        upgradeButton.image.color = new Color(255f, 0f, 0f, transparency);
    }
}
