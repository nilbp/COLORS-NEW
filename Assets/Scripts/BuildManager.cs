using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;
    

	void Awake(){

		if (instance != null) {

			Debug.Log ("More than 1 build manager in scene");
			return;
		}
		instance = this;
	}

	private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }

    public void BuildTurretOn(HexInfo hex)
    {
        if (MoneyManager.Pigment < turretToBuild.cost)
        {
            Debug.Log("not enough money to build");
            return;
        }

        MoneyManager.Pigment -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, hex.GetBuildPosition(), turretToBuild.prefab.transform.rotation);
        hex.turret = turret;

        if (turret.GetComponent<TubDePintura>() != null)
        {
            turret.GetComponent<TubDePintura>().actualHex = hex;
        }

        Debug.Log(turretToBuild.cost + " " +MoneyManager.Pigment);
        turretToBuild = null;
    }

	public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        
    }

}
