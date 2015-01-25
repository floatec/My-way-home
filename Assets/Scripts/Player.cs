using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
	public LayerMask terrainLayer;
	public LayerMask interUnitLayer;
	public LayerMask WatcherLayer;
	public InsidantAreaController iac;
	public float karma;
	public WatcherController[] watchers;
	public WorldController world;

	public float helpCallRange = 10;

	public GameObject SpeechbubblePrefab;
	public GameObject PolicePrefab;

	private float strongnes = 1;
	private Vector3 moveTarget;
	private NavMeshAgent agent;

	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		moveTarget = transform.position;
	}

	void Update ()
	{
		bool mouseOverUI = EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null;

		if ( mouseOverUI )
			return;

		var ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
		RaycastHit hit;

		if ( Input.GetMouseButton ( 0 ) && Physics.Raycast ( ray, out hit, 1000, terrainLayer ) )
		{
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
					iac.runaway ();
				}
				else
				{
					Application.LoadLevel ( "EndHospital" );
				}
			}
			if ( 1 << hit.collider.gameObject.layer == WatcherLayer.value )
			{
				AskForHelp ( hit.collider.gameObject.GetComponent<WatcherController> (), 0.3f );
			}
			if ( 1 << hit.collider.gameObject.layer == terrainLayer )
			{
				moveTarget = hit.point;
			}
		}
		agent.SetDestination ( moveTarget );
	}

	private void AskForHelp ( WatcherController ctrl, float strength )
	{
		GameObject inst;
		if ( ctrl.askForHelp ( strength ) )
		{
			karma += 15;
			strongnes++;

			inst = (GameObject)Object.Instantiate ( SpeechbubblePrefab );
			inst.transform.SetParent ( ctrl.transform, false );
			Destroy ( inst, 5 );
		}
		else
		{
			inst = (GameObject)Object.Instantiate ( SpeechbubblePrefab );
			inst.transform.SetParent ( ctrl.transform, false );
			Destroy ( inst, 5 );
		}

		inst = (GameObject)Object.Instantiate ( SpeechbubblePrefab );
		inst.transform.SetParent ( transform, false );
		Destroy ( inst, 5 );
	}

	public void leaveArea ()
	{
		iac = null;
		strongnes = 1;

	}

	public void CallHelp ()
	{
		var units = Physics.OverlapSphere ( transform.position, helpCallRange, interUnitLayer );
		foreach ( var item in units )
		{
			var watch = item.GetComponent<WatcherController> ();
			if ( watch != null )
			{
				AskForHelp ( watch, Random.value > 0.5f ? 0.15f : 0.1f );
			}
		}
	}

	public void callPolice ()
	{
		var pol = (GameObject)Object.Instantiate ( PolicePrefab );
		pol.GetComponent<PoliceController> ().moveTo ( transform.position, this.iac );
		world.Polices.Add ( pol );
	}

}
