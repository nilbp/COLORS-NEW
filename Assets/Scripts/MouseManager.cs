﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour {

	int xHexPos;
	int yHexPos;

<<<<<<< HEAD
    private int colorCost=10;

    private float colorCooldown = 0.8f;

    public Texture2D DefaultText;
=======
	int costPigment = 10;

	public Texture2D DefaultText;
>>>>>>> master

	public Texture2D CyanTex;
	public Texture2D MagentaTex;
	public Texture2D YellowTex;

	public static char ColorInHand;

	public GameObject CyanTubPinturaPrefab;
	public GameObject MagentaTubPinturaPrefab;
	public GameObject YellowTubPinturaPrefab;

	public Vector3 SpraiPositionOffset;
	public Vector3 TubOfset;

    //RAYCAST VARIABLES
    GameObject ourHitObject;
    RaycastHit hitInfo;
    Ray ray;

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


    void Start()
    {
        SpraiPositionOffset = new Vector3(0, 0.485f, 0);
    }

	void Update () {

        if (colorCooldown > 0)
            colorCooldown -= Time.deltaTime;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            ourHitObject = hitInfo.collider.transform.gameObject;

            if (Input.GetMouseButtonDown(0))
            {
                HexInfo hexInfoObject = ourHitObject.GetComponentInChildren<HexInfo>();              

                if (MoneyManager.Pigment > 0)
                {
                    if (hexInfoObject!=null)
                    {
                        MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();

<<<<<<< HEAD
                        if (colorCooldown <= 0)
                        {
                            if (ColorInHand == 'C')
                            {

                                hexInfoObject.HexColor = 'C';
                                hexInfoObject.SetColorTo(CyanTex);
                                MoneyManager.Pigment -= colorCost;
                                colorCooldown = 0.5f;
                            }
                            else if (ColorInHand == 'M')
                            {
                                hexInfoObject.HexColor = 'M';
                                hexInfoObject.SetColorTo(MagentaTex);
                                MoneyManager.Pigment -= colorCost;
                                colorCooldown = 0.5f;
                            }
                            else if (ColorInHand == 'Y')
                            {
                                hexInfoObject.HexColor = 'Y';
                                hexInfoObject.SetColorTo(YellowTex);
                                MoneyManager.Pigment -= colorCost;
                                colorCooldown = 0.5f;
                            }
=======
                        if (ColorInHand == 'C')
                        {

                            hexInfoObject.HexColor = 'C';
                            hexInfoObject.SetColorTo(CyanTex);
							MoneyManager.Pigment -= costPigment;
                        }
                        else if (ColorInHand == 'M')
                        {
                            hexInfoObject.HexColor = 'M';
                            hexInfoObject.SetColorTo(MagentaTex);
							MoneyManager.Pigment -= costPigment;
                        }
                        else if (ColorInHand == 'Y')
                        {
                            hexInfoObject.HexColor = 'Y';
                            hexInfoObject.SetColorTo(YellowTex);
							MoneyManager.Pigment -= costPigment;
>>>>>>> master
                        }
                    }
                }
            }
        }
	}

    


}

