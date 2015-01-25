using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public LayerMask terrainLayer;
	public LayerMask interUnitLayer;
	public LayerMask WatcherLayer;
	public InsidantAreaController iac;
	public float karma;
	public WatcherController[] watchers;
	public WorldController world;

	public GameObject SpeechbubblePrefab;
	public GameObject PolicePrefab;

	private float strongnes = 1;
	private Vector3 moveTarget;

	void Start ()
	{
		moveTarget = transform.position;
	}

	void Update ()
	{
		var ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
		RaycastHit hit;

		if (Input.GetMouseButton (0) && Physics.Raycast (ray, out hit, 1000, terrainLayer )) {
			if ( 1 << hit.collider.gameObject.layer == terrainLayer )
			{
				moveTarget = hit.point;
			}
		}
		if ( Input.GetMouseButtonUp ( 0 ) && Physics.Raycast ( ray, out hit, 1000, interUnitLayer | WatcherLayer ) )
		{
			if ( 1 << hit.collider.gameObject.layer == interUnitLayer.value )
			{
				if ( iac.isStronger ( this.strongnes ) )
				{
					karma += 50;
					iac.runaway();
				}
				else
				{
					Application.LoadLevel ( "EndHospital" );
				}
			}
			if ( 1 << hit.collider.gameObject.layer == WatcherLayer.value )
			{
				GameObject inst;

				if ( hit.collider.gameObject.GetComponent<WatcherController> ().askForHelp () )
				{
					karma += 15;
					strongnes++;

					inst = (GameObject)Object.Instantiate ( SpeechbubblePrefab );
					inst.transform.SetParent ( hit.collider.transform, false );
					Destroy ( inst, 5 );
				}
				else
				{
					inst = (GameObject)Object.Instantiate ( SpeechbubblePrefab );
					inst.transform.SetParent ( hit.collider.transform, false );
					Destroy ( inst, 5 );
				}

				inst = (GameObject)Object.Instantiate ( SpeechbubblePrefab );
				inst.transform.SetParent ( transform, false );
				Destroy ( inst, 5 );
			}

		}

		var agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination ( moveTarget );
	}
	public void leaveArea ()
	{
		iac = null;
		strongnes = 1;

	}

	public void callPolice(){
		var pol = (GameObject)Object.Instantiate (PolicePrefab);
		pol.GetComponent<PoliceController>().moveTo (transform.position,this.iac);
		world.Polices.Add(pol);
	}

}
