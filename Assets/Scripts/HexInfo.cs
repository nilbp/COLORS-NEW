 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : MonoBehaviour {


	public int x;
	public int y;

	public bool Nucli;
	public int ColorDensity;
	public bool Clickable;

	public char HexColor;
	public Map map;

	public HexInfo[] neigbours;

    [Header("Optional")]
	public GameObject turret;

	public Color hoverColor;
	private Renderer rend;
	private Color startColor;

    public Vector3 offsetX;

    BuildManager buildManager;

	void Start(){
        offsetX = new Vector3(0, 0.3859f, 0);

		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
	}

    public Vector3 GetBuildPosition()
    {
        return transform.position + offsetX;
    }

	void OnMouseDown(){

        if (!buildManager.CanBuild)
            return;

        if (turret != null) {
		
			Debug.Log ("Can't build");
			return;
		}
        buildManager.BuildTurretOn(this);
	}

	void OnMouseEnter(){

        if (!buildManager.CanBuild)
            return;
		
		rend.material.color =  hoverColor;

	
	}
	void OnMouseExit()
    {
		rend.material.color = startColor;
	}
}



