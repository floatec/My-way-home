using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public LayerMask terrainLayer;
	public LayerMask interUnitLayer;
	public InsidantAreaController iac;
	private float strongnes = 1;
	public float karma;
	private Vector3 moveTarget;

	void Start ()
	{
		moveTarget = transform.position;
	}

	void Update ()
	{
		var ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
		RaycastHit hit;
		if ( Input.GetMouseButtonUp ( 0 ) && Physics.Raycast ( ray, out hit, 1000, interUnitLayer | terrainLayer ) )
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
			if ( 1 << hit.collider.gameObject.layer == terrainLayer )
			{
				moveTarget = hit.point;
			}
		}

		//transform.position = Vector3.MoveTowards ( transform.position, moveTarget, 10 * Time.deltaTime );

		var agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination ( moveTarget );
	}
}
