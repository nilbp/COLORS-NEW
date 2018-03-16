 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour {

	int xHexPos;
	int yHexPos;

	public Texture2D DefaultText;

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

                        if (ColorInHand == 'C')
                        {

                            hexInfoObject.HexColor = 'C';
                            hexInfoObject.SetColorTo(CyanTex);
                            MoneyManager.Pigment -= 5;
                        }
                        else if (ColorInHand == 'M')
                        {
                            hexInfoObject.HexColor = 'M';
                            hexInfoObject.SetColorTo(MagentaTex);
                            MoneyManager.Pigment -= 5;
                        }
                        else if (ColorInHand == 'Y')
                        {
                            hexInfoObject.HexColor = 'Y';
                            hexInfoObject.SetColorTo(YellowTex);
                            MoneyManager.Pigment -= 5;
                        }
                    }
                }
            }
        }
	}

    


}

