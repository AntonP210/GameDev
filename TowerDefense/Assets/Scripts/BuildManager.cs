using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    public GameObject buildEffect;

    private TurretBluePrint turretToBuild;

    private Transform tempLocation;
    private Node selectedNode;

    public NodeUI nodeUI;

    public Vector3 positionOffset;




    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one buildmanager in scene!");
            return;
        }
        instance = this;
    }
    public void SetTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
   
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
}
