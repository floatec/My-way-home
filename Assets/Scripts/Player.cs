using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public LayerMask terrainLayer;

	private Vector3 moveTarget;

	void Update ()
	{
		var ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
		RaycastHit hit;
		if ( Input.GetMouseButtonUp ( 0 ) && Physics.Raycast ( ray, out hit, 1000, terrainLayer ) )
		{
			Debug.Log ( "hit! " + hit.point );

			moveTarget = hit.point;
		}

		transform.position = Vector3.MoveTowards ( transform.position, moveTarget, 10 * Time.deltaTime );
	}
}
