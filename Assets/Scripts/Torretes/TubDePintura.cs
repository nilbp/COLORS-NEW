using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubDePintura : MonoBehaviour {

    [Header("Atributes")]

	public int tubRange = 3;

	public float range = 1.2f;
	public float FireRatio = 1f; //3 = 3s ? 
	public float FireCountdown = 0f;

	[Header("Unity Setup Fields")]

	public HexInfo actualHex;

	public Transform target;
	public string enemyTag = "Enemy";

	public char tubColor;

	public GameObject bulletPrefab;
	public Transform firePoint;

    //per saber el rango cap a endavant en funció el numero de hexes 
    private HexInfo[] ListOfHexesInRange;

    void Start(){

		SetRange ();
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}

	void SetRange(){

		ListOfHexesInRange = new HexInfo[tubRange];
		HexInfo newHex = actualHex;

		for (int i = 0; i < tubRange; i++) {

			if (newHex.neigbours [0] == null)
				return;

			ListOfHexesInRange[i] = newHex.neigbours [0];
			newHex = newHex.neigbours [0];

			Debug.Log (ListOfHexesInRange [i].x);
		}
	}

	bool IsInHexRange(ColorComponents colorComponents){

		for(int i = 0; i< tubRange; i++){
			if (ListOfHexesInRange [i] == colorComponents.actualHex) 
				return true;	
		}
		return false;
	}

	void Update(){

		if (target == null) 
			return;
		if (FireCountdown <= 0f) 
		{
			Shoot ();
			FireCountdown = 1f / FireRatio;
		}

		FireCountdown -= Time.deltaTime;

	}

    void UpdateTarget()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        ColorComponents minion;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                if (enemy.gameObject != null)
                {
                    minion = enemy.GetComponentInParent<ColorComponents>();

                    Debug.Log(IsTheMinionShootable(minion));
                    if (IsTheMinionShootable(minion))
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy;
                    }
                   else
                        nearestEnemy = null; 
                }
            }
        }
        if (nearestEnemy == null)
            target = null;
        else
            target = nearestEnemy.transform;

    }

    public bool IsTheMinionShootable(ColorComponents minion)
    {
        if (!IsInHexRange(minion))
            return false;

        switch (tubColor)
        {
            case 'C':
                if (minion.cyanComponent > 0)              
                    return true;
                break;
            case 'M':
                if (minion.magentaComponent > 0)
                    return true;
                break;
            case 'Y':
                if (minion.yellowComponent > 0)
                    return true;
                break;
        }
        return false;
    }

void Shoot(){

		GameObject bulletGO = (GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet> ();

		if (bullet != null) {

			bullet.chase (target);
            bullet.color = tubColor;

        }
	}
}


