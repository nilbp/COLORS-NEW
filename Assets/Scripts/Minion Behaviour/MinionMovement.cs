using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour {

	public HexInfo ActualHex;
	public HexInfo NextHex;
	public HexInfo Nucli;

	public Texture DefaultTexture;
    private Renderer minionRenderer;

	private float Size;

    //QUANTITAT TOTAL DEL MINION
    private Color totalColor;
    public int minionColorQuantity;

    //COMPONTENTS DE COLORS PRIMARIS
    public int cyanQuantity=0;
    public int magentaQuantity=0;
    public int yellowQuantity=0;
    
    Transform target;

	public float speed = 1;

    //SIZE VARIABLE
    float sizeIncreaseVariable = 0.15f;

    //VELOCITAT DEL MINION
    public float realSpeed = 0.3f;

	private bool rotated;

	//EVITA QUE DETECTI COLISIÓ DE COLOR DESPRÉS DE PASSAR PEL HEX
	private float maxDist = 0.7f;
	private float minDist = 0.3f;
	private bool neutralHex = false;

	//Valor del mínon en funció de la dificultat de matar-lo
	public int minionValue = 0;

    //AQUESTA FUNCIÓ ES CRIDA DES DE L'SPAWN MANAGER DIENT LA QUANTITAT DE COLOR QUE TE EL MINION EX:(3,4,0) 3 CYANS I 4 MAGENTES

    void ConvineColors(int cyanQuantity , int magentaQuantity, int yellowQuantity)
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

        //CANVIA EL COLOR DEL MINION
        totalColor = result;

    }

    void Start () {

        minionRenderer = GetComponentInChildren<Renderer>();

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

        if (ActualHex.HexColor == 'W' || !neutralHex) {
			MovementRecte ();
		} 
		else {
			Colision ();
		}

		ColorManager ();

	}

	void ColorManager(){

        ConvineColors(cyanQuantity,magentaQuantity,yellowQuantity);

        //CANVIA EL COLOR
        minionRenderer.material.color = totalColor;

        Size = 1+minionColorQuantity*sizeIncreaseVariable;
		transform.localScale = new Vector3(Size,Size,Size);
	}

    void MovementRecte()
	{
		Vector3 dir= target.position - transform.position;
		float distanceThisFrame = speed * Time.fixedDeltaTime;

		//EVITA QUE DETECTI COLISIÓ DE COLOR DESPRÉS DE PASSAR PEL HEX
		if (dir.magnitude < maxDist && dir.magnitude > minDist)
			neutralHex = false;
		else if (dir.magnitude < minDist) {
			neutralHex = true;
			ActualHex = NextHex;
		}

		if (dir.magnitude <= distanceThisFrame) {

			NextHex = ActualHex.neigbours [3];

			if (NextHex == null) {
				Destroy (gameObject);
				return;
			}

			target = NextHex.gameObject.transform;
		}

		transform.Translate (dir.normalized * distanceThisFrame * realSpeed, Space.World);
	
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
