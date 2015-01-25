using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomDelete : MonoBehaviour
{
	public int Left = 1;

	void Start ()
	{
		List<Transform> list = new List<Transform> ( transform.childCount );
		foreach ( var item in transform )
		{
			list.Add ( (Transform)item );
		}
		while ( list.Count > Left )
		{
			var idx = Random.Range ( 0, list.Count );
			Destroy ( list[idx].gameObject );
			list.RemoveAt ( idx );
		}
	}
}
