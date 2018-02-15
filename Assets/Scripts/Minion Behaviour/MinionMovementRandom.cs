using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovementRandom : MonoBehaviour {

	public HexInfo ActualHex;
	public HexInfo NextHex;
		
	//EVITA QUE DETECTI COLISIÓ DE COLOR DESPRÉS DE PASSAR PEL HEX
	private float maxDist = 0.7f;
	private float minDist = 0.3f;
	private bool neutralHex = false;

	public HexInfo Nucli;
	public Texture DefaultTexture;
    private MeshRenderer minionRenderer;
    private ColorComponents ownColor;

    Transform target;

	private bool facingNordEast;
	private bool facingSouthEast;
	private float Size;

    //QUANTITAT TOTAL DEL MINION
    private Color totalColor;
    public int minionColorQuantity;

    //COMPONTENTS DE COLORS PRIMARIS
    public int cyanQuantity = 0;
    public int magentaQuantity = 0;
    public int yellowQuantity = 0;

    // CARACTERISTIQUES
    public int Life;
	public char ColorIdentifier;
	public float speed = 0.2f;

    //SIZE VARIABLE
    float sizeIncreaseVariable = 0.15f;
    
    //COMPROVAR QUE NO SURTIN DEL MAPA
    private int lastSpawnPoint = 7;
	private int firstSpawnPoint = 0;

	//MÉS GRAN L'ENTER = MENYS POSSIBILITATS QUE CANVII.
	public int chanceToChangeDirection = 10;

	//Valor del mínon en funció de la dificultat de matar-lo
	public int minionValue = 0;

    //AQUESTA FUNCIÓ ES CRIDA DES DE L'SPAWN MANAGER DIENT LA QUANTITAT DE COLOR QUE TE EL MINION EX:(3,4,0) 3 CYANS I 4 MAGENTES

    void ConvineColors(int cyanQuantity, int magentaQuantity, int yellowQuantity)
    {
        int totalSize = cyanQuantity + magentaQuantity + yellowQuantity;
        Color[] aColors = new Color[totalSize];

        bool exitLoop = false;
        int posInArray = 0;
        do
        {
            if (cyanQuantity > 0)
            {
                aColors[posInArray] += Color.cyan;
                cyanQuantity--;
                posInArray++;
            }
            else if (magentaQuantity > 0)
            {
                aColors[posInArray] += Color.magenta;
                magentaQuantity--;
                posInArray++;
            }
            else if (yellowQuantity > 0)
            {
                aColors[posInArray] += Color.yellow;
                yellowQuantity--;
                posInArray++;
            }
            else
            {
                exitLoop = true;
            }

        } while (!exitLoop);

        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;

        minionColorQuantity = totalSize;

        totalColor = result;

    }

    void Start () {

        minionRenderer = GetComponentInChildren<MeshRenderer>();
        ownColor = GetComponent<ColorComponents>();
        //INICIALITZAR ELS PROPIS COLORS
        cyanQuantity = ownColor.cyanComponent;
        magentaQuantity = ownColor.magentaComponent;
        yellowQuantity = ownColor.yellowComponent;

        ConvineColors(cyanQuantity, magentaQuantity, yellowQuantity);

        NextHex = ActualHex.neigbours[3];
		target = NextHex.gameObject.transform;
        minionValue = minionColorQuantity * 10;

    }

	void Update(){

        if (minionColorQuantity <= 0)
        {
            MoneyManager.Pigment += minionValue;
            Destroy(gameObject);
            return;
        }

		if (ActualHex.HexColor == 'W' || !neutralHex) 
			MovementRandom ();
		else 
			Colision ();

		ColorManager();

        //FER UPDATE DE LES VARIABLES DE L'SCRIPT "COLOR COMPONENTS"
        cyanQuantity = ownColor.cyanComponent;
        magentaQuantity = ownColor.magentaComponent;
        yellowQuantity = ownColor.yellowComponent;

    }

	void ColorManager(){

        ConvineColors(cyanQuantity, magentaQuantity, yellowQuantity);

        //CANVIA EL COLOR
        minionRenderer.materials[0].color = totalColor;
        minionRenderer.materials[1].color = totalColor;

        Size = 1 + minionColorQuantity * sizeIncreaseVariable;
        transform.localScale = new Vector3(Size,Size,Size);


	}
	int RandomInt(int from, int to){
		int Rand = Random.Range(from, to);
		return Rand;
	}


	void MovementRandom(){
		Vector3 dir= target.position - transform.position;
		float distanceThisFrame = speed * Time.fixedDeltaTime;
			
		//EVITA QUE DETECTI COLISIÓ DE COLOR DESPRÉS DE PASSAR PEL HEX
		if (dir.magnitude < maxDist && dir.magnitude > minDist)
			neutralHex = false;
		else if (dir.magnitude < minDist) {
			neutralHex = true;
			ActualHex = NextHex;
		}
			
		if (dir.magnitude <= distanceThisFrame)
		{

			if (RandomInt (0, chanceToChangeDirection) == 0 && ActualHex.y > firstSpawnPoint) {

				NextHex = ActualHex.neigbours [4];
					
				if (facingNordEast == true) {
					transform.Rotate (1, 120, 1);
					facingSouthEast = true;
						
				}
				else if (facingSouthEast == false) {

					transform.Rotate (1, 60, 1);
					facingSouthEast = true;
				} 
				facingNordEast = false;
			} 
			else if (RandomInt (0, chanceToChangeDirection) == 1 && ActualHex.y < lastSpawnPoint) {
				
				NextHex = ActualHex.neigbours [2];

				if (facingSouthEast == true) {

					transform.Rotate (1, -120, 1);
					facingNordEast = true;
						
				} 
				else if (facingNordEast == false) {
					transform.Rotate (1, -60, 1);
					facingNordEast = true;
				}
				facingSouthEast = false;
			}
			else {

				NextHex = ActualHex.neigbours [3];
					
				if (facingNordEast == true) {
					transform.Rotate (1, 60, 1);
					facingNordEast = false;
				} 
				else if (facingSouthEast == true) {
					transform.Rotate (1, -60, 1);
					facingSouthEast = false;
				}
			}

			if (NextHex == null) {
				Destroy (gameObject);
				return;
			}
			target = NextHex.gameObject.transform;
		}
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);


	}

	void ResetHexagonColorValues(HexInfo ActualHex){

		ActualHex.HexColor = 'W';
		ActualHex.transform.localScale = new Vector3 (1, 1, 1);
		ActualHex.GetComponent<Renderer>().material.mainTexture = DefaultTexture;
		ActualHex.ColorDensity = 0;
	}

	void Colision(){

        if (ActualHex.HexColor == 'C')
        {
            if (cyanQuantity > 0)
                cyanQuantity--;

            else if (cyanQuantity <= 0)
            {
                cyanQuantity++;
            }
            ResetHexagonColorValues(ActualHex);
        }
        else if (ActualHex.HexColor == 'M')
        {
            if (magentaQuantity > 0)
                magentaQuantity--;

            else if (magentaQuantity <= 0)
            {
                magentaQuantity++;
            }
            ResetHexagonColorValues(ActualHex);
        }
        else if (ActualHex.HexColor == 'Y')
        {
            if (yellowQuantity > 0)
                yellowQuantity--;

            else if (yellowQuantity <= 0)
            {
                yellowQuantity++;
            }
            ResetHexagonColorValues(ActualHex);
        }
    }


}


