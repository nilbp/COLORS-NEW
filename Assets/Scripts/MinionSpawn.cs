using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawn : MonoBehaviour {

	GameObject spawn1;

	private int counter;

    //MINION PREFABS
    public GameObject minion1;
    public GameObject minion2;
    public GameObject minion3;

    private int firstSpawnPoint = 1;
	private int lastSpawnPoint = 7;

    //VARIABLES PEL CANVI DE COLOR
    int cyanQuantity;
    int magentaQuantity;
    int yellowQuantity;

    [System.Serializable]
    public enum ColorComplexity {basic,medium,advanced,random};

    [System.Serializable]
    public enum Behaviour {move_Forward, mov_S, move_Random};

   //STRUCTS PEL LEVEL DESIGN 
   [System.Serializable]
    public struct Minion
    {

        public int size;

        //0.2 ÉS VELOCITAT RAONABLE
        public float speed;

        //1 = 1 color. 2 = 2 colors, 3 = 3 colors, 4 = random 
        public ColorComplexity colorComplexity;

        //1 = move forward, 2 = move S, 3 = move random, 4 = random
        public Behaviour behaviour;
    }

    [System.Serializable]
    public struct Waves
    {
        public float spawnRatio;
        public float startTime;
        public Minion[] minion;

    }

    //VARIABLES PER EL LEVEL DESIGN
    [Header("LEVEL DESIGN TOOL")]
    public Waves[] waves;


    void Start()
    {
        StartCoroutine(SpawnManager1());
    }

    IEnumerator SpawnManager1()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            yield return new WaitForSeconds(waves[i].startTime);
            for (int j = 0; j < waves[i].minion.Length; j++)
            {
                yield return new WaitForSeconds(waves[i].spawnRatio);

                switch (waves[i].minion[j].behaviour)
                {

                    case (Behaviour)1:
                        SpawnMinionBehaviour1(waves[i].minion[j]);
                        break;
                    case (Behaviour)2:
                        SpawnMinionBehaviour2(waves[i].minion[j]);
                        break;
                    case (Behaviour)3:
                        SpawnMinionBehaviour3(waves[i].minion[j]);
                        break;                
                    default:
                        SpawnMinionBehaviour1(waves[i].minion[j]);
                        break;
                }
            }
        }
    }

    void Update(){

		
	}

	int RandomInt(int from, int to)
	{
		return Random.Range (from, to);
	}

    void BuildMinion(Minion minion)  {

        int acum;
        int counter;
        int aux;
        
        switch (minion.colorComplexity)
        {
            case (ColorComplexity)0:
                acum = RandomInt(1, 3);
                if (acum == 1)
                    cyanQuantity = minion.size;
                else if (acum == 2)
                    magentaQuantity = minion.size;
                else
                    yellowQuantity = minion.size;
                break;
            case (ColorComplexity)1:
                counter = RandomInt(0, minion.size);
                acum = RandomInt(1, 3);
                if (acum == 1)
                {
                    cyanQuantity = counter;
                    magentaQuantity = minion.size - counter;
                }
                else if (acum == 2)
                {
                    yellowQuantity = counter;
                    cyanQuantity = minion.size - counter;
                }
                else
                {
                    magentaQuantity = counter;
                    yellowQuantity = minion.size - counter;
                }
                break;
            case (ColorComplexity)2:
                counter = RandomInt(0, minion.size);
                aux = RandomInt(0, minion.size - counter);

                acum = RandomInt(1, 3);
                if (acum == 1)
                {
                    cyanQuantity = counter;
                    magentaQuantity = aux;
                    yellowQuantity = minion.size - (aux + counter);
                }
                else if (acum == 2)
                {
                    yellowQuantity = counter;
                    cyanQuantity = aux;
                    magentaQuantity = minion.size - (aux + counter);
                }
                else
                {
                    magentaQuantity = counter;
                    yellowQuantity = aux;
                    cyanQuantity = minion.size - (aux + counter);
                }
                break;
            case (ColorComplexity)3:
                minion.colorComplexity = (ColorComplexity)RandomInt(1, 3);
                BuildMinion(minion);
                break;
        }
    }

    

   //FORWARD MOVE
	void SpawnMinionBehaviour1(Minion minion){

        
        spawn1 = GameObject.Find("Hex_0_" + RandomInt(firstSpawnPoint, lastSpawnPoint));

        //PASSO, A L'SCRIPT DEL MINION, "HEXINFO" (NECESSARI PER QUE EL MINION SAPIGA SABER ON ÉS) I VARIABLES DE COLOR, TAMANY I VELOCITAT (PER QUE SAPIGA COM CONSTRUIR-SE)
        HexInfo spawn1Hex = spawn1.GetComponentInChildren<HexInfo>();
        MinionMovement minionScript = minion1.GetComponent<MinionMovement> ();
        ColorComponents colorComponents = minion1.GetComponent<ColorComponents>();

		minionScript.ActualHex = spawn1Hex;

        BuildMinion(minion); 
        
        //POSA LES VARIABLES DE COLOR EN FUNCIÓ DE "MINION.SIZE" I "MINION.COLORCOMPLEXITY"
        colorComponents.cyanComponent = cyanQuantity;
        colorComponents.magentaComponent = magentaQuantity;
        colorComponents.yellowComponent = yellowQuantity;

        //RESET DE LES VARIABLES GLOBALS DE COLOR
        cyanQuantity = 0;
        magentaQuantity = 0;
        yellowQuantity = 0;

        minionScript.speed = minion.speed; 

        Instantiate (minion1, spawn1.transform.position, minion1.transform.rotation);
	}

    //S MOVE
	void SpawnMinionBehaviour2(Minion minion)
    {
        //MATEIXA ESTRUCTURA QUE "FORWARD MOVE" PERO INSTANTIAN UN MINION AMB UN ALTRE COMPORTAMENT 
        spawn1 = GameObject.Find("Hex_0_" + RandomInt(firstSpawnPoint, lastSpawnPoint));

        HexInfo spawn1Hex = spawn1.GetComponentInChildren<HexInfo>();
        MinionMovementS minionScript = minion2.GetComponent<MinionMovementS>();
        ColorComponents colorComponents = minion2.GetComponent<ColorComponents>();

        minionScript.ActualHex = spawn1Hex;

        BuildMinion(minion);
        colorComponents.cyanComponent = cyanQuantity;
        colorComponents.magentaComponent = magentaQuantity;
        colorComponents.yellowComponent = yellowQuantity;

        cyanQuantity = 0;
        magentaQuantity = 0;
        yellowQuantity = 0;

        minionScript.speed = minion.speed;

        Instantiate(minion2, spawn1.transform.position, minion2.transform.rotation);

    }

    //RANDOM MOVE
	void SpawnMinionBehaviour3(Minion minion)
    {

        //MATEIXA ESTRUCTURA QUE "FORWARD MOVE" PERO INSTANTIAN UN MINION AMB UN ALTRE COMPORTAMENT 
        spawn1 = GameObject.Find("Hex_0_" + RandomInt(firstSpawnPoint, lastSpawnPoint));

        HexInfo spawn1Hex = spawn1.GetComponentInChildren<HexInfo>();
        MinionMovementRandom minionScript = minion3.GetComponent<MinionMovementRandom>();
        ColorComponents colorComponents = minion3.GetComponent<ColorComponents>();

        minionScript.ActualHex = spawn1Hex;

        BuildMinion(minion);
        colorComponents.cyanComponent = cyanQuantity;
        colorComponents.magentaComponent = magentaQuantity;
        colorComponents.yellowComponent = yellowQuantity;

        cyanQuantity = 0;
        magentaQuantity = 0;
        yellowQuantity = 0;

        minionScript.speed = minion.speed;

        Instantiate(minion3, spawn1.transform.position, minion3.transform.rotation);

    }


}
