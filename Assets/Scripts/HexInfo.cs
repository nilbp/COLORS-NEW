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

	public struct Totem{

		tipusTotem TotemType;
		Color[] ColorsDefensa;
		int FireRatio;
		int Area;
		int NumAbsorcions;


	}

}



