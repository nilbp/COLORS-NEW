using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : MonoBehaviour {


	public int x;
	public int y;

	public bool Nucli=true;
	public int ColorDensity;
	public bool Clickable;

	public Color HexColor;

    public Map map;

	void Start(){
	
	
	}

	void Update()
	{
	
		IsClickable ();

	}

	void IsClickable(){

		for (int i = (int)NeighbourPosition.Left; i < (int)NeighbourPosition.NumPositions; i++) {
			HexInfo neighbour = GetNeighbourByPosition ((NeighbourPosition)i);

			//Debug.Log(x + " " + y + " " + neighbour);

			if (neighbour != null) {

				if (neighbour.Nucli == true || neighbour.ColorDensity > 1) {
					Clickable = true;

				}
			}
		}
	}

    enum NeighbourPosition
    {
        Left,
		UpLeft,
		UpRight,
        Right,
		DownRight,
		DownLeft,
        NumPositions,
    }

    HexInfo GetNeighbourByPosition(NeighbourPosition position)
    {
        HexInfo retVal = null;

        switch (position)
        {
		case NeighbourPosition.Left:
			retVal = GetLeftNeighbour ();
                break;
		case NeighbourPosition.UpLeft:
			retVal = GetUpLeftNeighbour ();
                break;
			case NeighbourPosition.UpRight:
			retVal = GetUpRightNeighbour();
                break;
		case NeighbourPosition.Right:
			retVal = GetRightNeighbour ();
                break;
            case NeighbourPosition.DownRight:
			retVal = GetDownRightNeighbour();
                break;
		case NeighbourPosition.DownLeft:
			retVal = GetDownLeftNeighbour ();
                break;
            default:
                break;
        }

        return retVal;
    }

	//Map.heith o Map.widht -1 ???? 

    HexInfo GetLeftNeighbour()
    {
        HexInfo retVal = null;

        if (x > 0)
        {
           // retVal = map.hexLines[x - 1].columns[y];
			retVal = map.hexLines[y].columns[x-1];
        }

        return retVal;
    }

	HexInfo GetUpLeftNeighbour(){
	
		HexInfo retVal = null;
	
		if (y % 2 == 0 && x > 0 ) {
				//retVal = map.hexLines [x - 1].columns [y + 1];
				retVal = map.hexLines [y+1].columns [x - 1];
		
			} 
		else if (y % 2 == 1 && y < Map.height -1) {
				//retVal = map.hexLines [x].columns [y + 1];
				retVal = map.hexLines [y+1].columns [x];
			}
			return retVal;
		}
		
	HexInfo GetUpRightNeighbour(){
	
		HexInfo retVal = null;

		if (y % 2 == 0) {

			//retVal = map.hexLines [x].columns [y + 1]; 
			retVal = map.hexLines [y + 1].columns [x]; 

		} else if (y % 2 == 1 && x < Map.width - 1 && y < Map.height - 1) {

			//retVal = map.hexLines [x + 1].columns [y + 1];
			retVal = map.hexLines [y + 1].columns [x + 1];
		}
		return retVal;
	}

	HexInfo GetRightNeighbour(){

		HexInfo retVal = null;
	
		if (x < Map.width-1) {
		
			//retVal = map.hexLines [x + 1].columns [y];
			retVal = map.hexLines [y].columns [x+1];
		
		}

		return retVal;
	}

	HexInfo GetDownRightNeighbour(){
	
		HexInfo retVal=null;

			if (y % 2 == 0 && y > 0) {
				//retVal = map.hexLines [x].columns [y - 1];
				retVal = map.hexLines [y-1].columns [x];
			}
			else if (y % 2 == 1 && x < Map.width-1 ) {
			
				//retVal = map.hexLines[x + 1].columns[y - 1];
				retVal = map.hexLines[y - 1].columns[x + 1];
			}

	
		return retVal;
	}
	HexInfo GetDownLeftNeighbour(){
	
		HexInfo retVal=null;



		if (y % 2 == 0 && x > 0 && y > 0) {
				//retVal = map.hexLines [x - 1].columns [y - 1];
				retVal = map.hexLines [y - 1].columns [x - 1];
			} 
			else if (y % 2 == 1) {
				//retVal = map.hexLines [x].columns [y - 1];
				retVal = map.hexLines [y-1].columns [x];
			}


		return retVal;
	
	}

    //Falta crear totes les funcions de GetNeigbour(left, down left...)
    //HexInfo GetRightNeighbour()
    //HexInfo GetRightNeighbour()


    /*public GameObject[] HexNeighbours(){

		GameObject[] Veins = new GameObject[6];

		//left and right neighbours
		Veins[0] = GameObject.Find ("Hex_" + (x - 1) + "_" + y);
		Veins[3] = GameObject.Find ("Hex_" + (x + 1) + "_" + y);

		//even row
		if (y % 2 == 0) {

			Veins[1] = GameObject.Find ("Hex_" + (x - 1) + "_" + (y + 1));
			Veins[2] = GameObject.Find ("Hex_" + x + "_" + (y + 1));
			Veins[5] = GameObject.Find ("Hex_" + (x - 1) + "_" + (y - 1));
			Veins[4] = GameObject.Find ("Hex_" + x + "_" + (y - 1));
		} 

		//odd row
		else if (y % 2 == 1) {

			Veins[1] = GameObject.Find ("Hex_" + x + "_" + (y + 1));
			Veins[2] = GameObject.Find ("Hex_" + (x + 1) + "_" + (y + 1));
			Veins[5] = GameObject.Find ("Hex_" + x + "_" + (y - 1));
			Veins[4] = GameObject.Find ("Hex_" + (x + 1) + "_" + (y - 1));

		}

		return Veins;
	}*/
}
