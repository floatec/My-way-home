using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour
{
	public GameObject PlayerPrefab;

	void Start ()
	{
		var spots = GameObject.FindGameObjectsWithTag ( "PlayerSpawn" );
		var pos = spots[Random.Range ( 0, spots.Length )].transform.position;

		var inst = (GameObject)Object.Instantiate ( PlayerPrefab );
		inst.transform.position = pos;
	}
}
