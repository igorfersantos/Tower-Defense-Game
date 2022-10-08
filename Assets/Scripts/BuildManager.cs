using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TurretBlueprint turretToBuild;

    [Header("Effects")]
    public GameObject buildEffect;

    private static BuildManager _instance;
    public static BuildManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("BuildManager is null!");
            }

            return _instance;
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasEnoughMoneyToBuild { get { return PlayerStats.Money >= turretToBuild.cost; } }


    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public TurretBlueprint GetSelectedTurretToBuild()
    {
        return turretToBuild;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        GameManager.Instance.removeMoneyFromPlayer(turretToBuild.cost);
        BuildTurret(node);

        Debug.Log($"Turret build! Money left:{PlayerStats.Money}");
    }

    private void BuildTurret(Node node)
    {
        Vector3 position = node.GetBuildPosition();

        if(turretToBuild.prefab.name == "LaserBeamer");
            position.y -= 0.5f;

        GameObject turret = Instantiate(turretToBuild.prefab, position, Quaternion.identity);
        node.turret = turret;

        GameObject buildEffectObject = Instantiate(buildEffect, position, Quaternion.identity);
        Destroy(buildEffectObject, 5f);
    }
}
