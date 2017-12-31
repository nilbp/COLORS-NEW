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

	public GameObject defenseTotemPrefab;
	public GameObject AnotherTotemPrefab;

	private GameObject totemToBuild;

	public GameObject GetTotemToBuild()
	{
		return totemToBuild; 
	}

	public void SetTotemToBuild(GameObject totem){

		totemToBuild = totem;

	}

}
