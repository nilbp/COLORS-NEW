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

	enum tipusTotem{farming, defensa, area};

	private GameObject Totem;

	public Color hoverColor;
	private Renderer rend;
	private Color startColor; 

	BuildManager buildManager;

	void Start(){

		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
	}

	void OnMouseDown(){
	
		if (buildManager.GetTotemToBuild () == null)
			return;
		
		if (Totem != null) {
		
			Debug.Log ("Can't build");
			return;
		}

		GameObject totemToBuild = BuildManager.instance.GetTotemToBuild ();
		Totem = (GameObject)Instantiate (totemToBuild, transform.position, Quaternion.identity);

	}

	void OnMouseEnter(){
	
		if (buildManager.GetTotemToBuild () == null)
			return;
		
		rend.material.color =  hoverColor;

	
	}
	void OnMouseExit(){

		rend.material.color = startColor;

	}

}



