using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret_HUD : MonoBehaviour {

    public TurretBlueprint sprai;
    public TurretBlueprint tub;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectSprai()
    {
        buildManager.SelectTurretToBuild(sprai);
    }

    public void SelectTub()
    {
        buildManager.SelectTurretToBuild(tub);
    }

}
