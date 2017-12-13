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


				
				HexInfo hexInfo = Hex_go.GetComponentInChildren<HexInfo> ();
				hexInfo.x = x;
				hexInfo.y = y;
				hexInfo.Nucli = false;
				hexInfo.ColorDensity = 0;
				hexInfo.Clickable = false;
                hexInfo.map = this;

				if(hexInfo.x==10 && hexInfo.y==2){

					hexInfo.Nucli= true;
				}

				SpawnEnemies (Hex_go, x, y);
				SpawnEnemies2 (Hex_go, x, y);
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

	void SpawnEnemies(GameObject hexInfo, int x, int y){
	
		if(x == 0 && y == 2){

			spawner.HexSpawn1 = hexInfo;

		}


			}
	void SpawnEnemies2(GameObject hexInfo, int x, int y){

		if(x == 0 && y == 4){

			spawner.HexSpawn2 = hexInfo;

		}
			

	}






}
