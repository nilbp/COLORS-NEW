using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour {

	public HexInfo ActualHex;
	public HexInfo NextHex;

	public Texture DefaultTexture;
    private MeshRenderer minionRenderer;
    private ColorComponents ownColor;

    ParticualsColor particuals;
    public ParticleSystem particlesDead;
    private Vector3 particlesOffset = new Vector3(0, 0.5f, 0);

    private float Size;

    //QUANTITAT TOTAL DEL MINION
    private Color totalColor;
    public int minionColorQuantity;
    private Color lastColor;

    //COMPONTENTS DE COLORS PRIMARIS
    public int cyanQuantity=0;
    public int magentaQuantity=0;
    public int yellowQuantity=0;
    
    Transform target;

	public float speed = 0.2f;

    //SIZE VARIABLE
    float sizeIncreaseVariable = 0.15f;

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
        if (cyanQuantity < 0 || magentaQuantity < 0 || yellowQuantity < 0)
            return;

        minionColorQuantity = cyanQuantity + magentaQuantity + yellowQuantity;
        Color[] aColors = new Color[minionColorQuantity];

        for(int i = 0; i < minionColorQuantity; i++) { 
            if (cyanQuantity > 0)
            {
                aColors[i] += Color.cyan;
                cyanQuantity--;
            }
            else if (magentaQuantity > 0)
            {
                aColors[i] += Color.magenta;
                magentaQuantity--;
            }
            else if (yellowQuantity > 0)
            {
                aColors[i] += Color.yellow;
                yellowQuantity--;
            }
        }

        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;

        totalColor = result;
    }

    void Start () {

        minionRenderer = GetComponentInChildren<MeshRenderer>();
        ownColor = GetComponent<ColorComponents>();

        cyanQuantity = ownColor.cyanComponent;
        magentaQuantity = ownColor.magentaComponent;
        yellowQuantity = ownColor.yellowComponent;

        ConvineColors(cyanQuantity, magentaQuantity, yellowQuantity);

		NextHex = ActualHex.neigbours[3];
		target = NextHex.gameObject.transform;
		minionValue = minionColorQuantity * 10;

	}

	void Update(){


        if (minionColorQuantity == 1)
            lastColor = totalColor;

        if (minionColorQuantity <= 0)
        {
           
            MoneyManager.Pigment += minionValue;
            Destroy(gameObject);

            if (ownColor.lastMinionInWave)
            {
                Debug.Log("lastminion dead");
                TutorialManager.lastMinion = true;
                return;
            }
            InstantiateParticles();
            return;
        }

        if (ActualHex != null && ActualHex.HexColor == 'W' || ActualHex!= null && !neutralHex) {
			MovementRecte ();
		} 
		else {
			Colision ();
		}

		ColorManager ();

        if (ownColor == null)
            return;


        //FER UPDATE DE LES VARIABLES DE L'SCRIPT "COLOR COMPONENTS"
        cyanQuantity = ownColor.cyanComponent;
        magentaQuantity = ownColor.magentaComponent;
        yellowQuantity = ownColor.yellowComponent;
        ownColor.actualHex = ActualHex;
        

    }

    void InstantiateParticles()
    {
            Instantiate(particlesDead, transform.position + particlesOffset, particlesDead.transform.rotation);     
    }

    void ColorManager(){

        if (minionRenderer == null)
            return;

        ConvineColors(cyanQuantity,magentaQuantity,yellowQuantity);

        //CANVIA EL COLOR
        minionRenderer.materials[0].color = totalColor;
        minionRenderer.materials[1].color = totalColor;


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

		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	
	}


	void ResetHexagonColorValues(HexInfo ActualHex){

		ActualHex.HexColor = 'W';
		ActualHex.transform.localScale = new Vector3 (1, 1, 1);
		ActualHex.GetComponent<Renderer>().material.mainTexture = DefaultTexture;
		ActualHex.ColorDensity = 0;
	}

	void Colision(){

        if (ActualHex == null)
            return;

        if (ActualHex.HexColor == 'C')
        {
			if (cyanQuantity > 0) {
				ownColor.cyanComponent--;


			
			}
			else if (cyanQuantity <= 0)
            {
                ownColor.cyanComponent++;



            }

            ResetHexagonColorValues(ActualHex);
        }
        else if (ActualHex.HexColor == 'M')
        {
			if (magentaQuantity > 0) {
				ownColor.magentaComponent--;
			

			}
            else if (magentaQuantity <= 0)
            {
                ownColor.magentaComponent++;
			

            }
            ResetHexagonColorValues(ActualHex);
        }
        else if (ActualHex.HexColor == 'Y')
        {
			if (yellowQuantity > 0) {
				ownColor.yellowComponent--;
			

			}
            else if (yellowQuantity <= 0)
            {
                ownColor.yellowComponent++;
			

            }
            ResetHexagonColorValues(ActualHex);
        }
	}

}
