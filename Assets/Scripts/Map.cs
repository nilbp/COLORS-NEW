using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	//http://catlikecoding.com/unity/tutorials/hex-map/part-1/

	//public HexInfo HexI;

	public GameObject hexprefab;
	//public Texture2D SandTexture;
	//size of the map

	//Ha de ser parell (width i heith) SEMPRE!!
	public static int width = 20;
	public static int height = 6;

	float xOffset = 0.882f;
	float zOffset = 0.764f;

    [System.Serializable]
    public class HexLine
    {
        public HexInfo[] columns;
    }

    public HexLine[] hexLines;

	void Start () {

		createMap ();

	}

	void createMap(){
		
        hexLines = new HexLine[height];
        for (int i = 0; i < height; i ++)
        {
            hexLines[i] = new HexLine();
            hexLines[i].columns = new HexInfo[width];
        }

		for (int x = 0; x < width; x++) {

			for (int y = 0; y < height; y++) {

				float xPos = x * xOffset;

				if (y % 2 == 1) 
				{
					xPos += xOffset/2;
				}

				//Instantiate hex
				GameObject Hex_go = (GameObject)Instantiate (hexprefab, new Vector3(xPos,0,y*zOffset), Quaternion.identity); 

				HexInfo hexInfo = Hex_go.GetComponent<HexInfo> ();
				hexInfo.x = x;
				hexInfo.y = y;
				hexInfo.Nucli = false;
				hexInfo.ColorDensity = 0;
				hexInfo.Clickable = false;
                hexInfo.map = this;
				             
                hexLines[y].columns[x] = hexInfo;

                //Aray out of range!!!!	
                //Neighbours (Hex_go);

                //Rename hexes with coordenate names
                Hex_go.name = "Hex_" + x + "_" + y;

				//Group hexes in a GameObject parent called "Hex"
				Hex_go.transform.SetParent (this.transform);

				Hex_go.isStatic = true;
			}
		}


	}

	/*void Neighbours(GameObject Hex){

		int x = Hex.GetComponent<HexInfo> ().x;
		int y = Hex.GetComponent<HexInfo> ().y;

		Hex.GetComponent<HexInfo>().HexNeighbours[0] =  GameObject.Find ("Hex_" + (x - 1) + "_" + y);
		Hex.GetComponent<HexInfo>().HexNeighbours[3] = GameObject.Find ("Hex_" + (x + 1) + "_" + y);

		//even row
		if (y % 2 == 0) {

			Hex.GetComponent<HexInfo>().HexNeighbours[1] =  GameObject.Find ("Hex_" + (x - 1) + "_" + (y + 1));
			Hex.GetComponent<HexInfo>().HexNeighbours[2] =  GameObject.Find ("Hex_" + x + "_" + (y + 1));
			Hex.GetComponent<HexInfo>().HexNeighbours[5] =  GameObject.Find ("Hex_" + (x - 1) + "_" + (y - 1));
			Hex.GetComponent<HexInfo>().HexNeighbours[4] =  GameObject.Find ("Hex_" + x + "_" + (y - 1));
		} 

		//odd row
		else if (y % 2 == 1) {

			Hex.GetComponent<HexInfo>().HexNeighbours[1] =  GameObject.Find ("Hex_" + x + "_" + (y + 1));
			Hex.GetComponent<HexInfo>().HexNeighbours[2] =  GameObject.Find ("Hex_" + (x + 1) + "_" + (y + 1));
			Hex.GetComponent<HexInfo>().HexNeighbours[5] = GameObject.Find ("Hex_" + x + "_" + (y - 1));
			Hex.GetComponent<HexInfo>().HexNeighbours[4] =  GameObject.Find ("Hex_" + (x + 1) + "_" + (y - 1));
		}
	}*/

}
