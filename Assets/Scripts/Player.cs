using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public LayerMask terrainLayer;
	public LayerMask interUnitLayer;
	public LayerMask WatcherLayer;
	public InsidantAreaController iac;
	private float strongnes = 1;
	public float karma;
	private Vector3 moveTarget;
	public WatcherController[] watchers;

	public GameObject SpeechbubblePrefab;


	void Start ()
	{
		moveTarget = transform.position;
	}

	void Update ()
	{
		var ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
		RaycastHit hit;
		if ( Input.GetMouseButtonUp ( 0 ) && Physics.Raycast ( ray, out hit, 1000, interUnitLayer | terrainLayer | WatcherLayer ) )
		{
			if ( 1 << hit.collider.gameObject.layer == interUnitLayer.value )
			{
				if ( iac.isStronger ( this.strongnes ) )
				{
					karma += 50;
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
			if ( 1 << hit.collider.gameObject.layer == terrainLayer )
			{
				moveTarget = hit.point;
			}
		}

		//transform.position = Vector3.MoveTowards ( transform.position, moveTarget, 10 * Time.deltaTime );

		var agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination ( moveTarget );
	}
	public void leaveArea ()
	{
		iac = null;
		strongnes = 1;

	}
}
