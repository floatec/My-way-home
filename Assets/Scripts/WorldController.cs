using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour
{
	public CameraController Camera;
	public GameObject PlayerPrefab;
	public GameObject WandererPrefab;

	private WatcherController[] Peoples;

	IEnumerator Start ()
	{
		var spots = GameObject.FindGameObjectsWithTag ( "PlayerSpawn" );
		var pos = spots[Random.Range ( 0, spots.Length )].transform.position;

		var inst = (GameObject)Object.Instantiate ( PlayerPrefab );
		inst.transform.position = pos;

		Camera.Target = inst.GetComponent<Player> ();



		spots = GameObject.FindGameObjectsWithTag ( "UnitSpawn" );

		Peoples = new WatcherController[100];
		for ( int i = 0; i < Peoples.Length; i++ )
		{
			pos = spots[Random.Range ( 0, spots.Length )].transform.position;
			inst = (GameObject)Object.Instantiate ( WandererPrefab );
			inst.transform.position = pos;

			pos = spots[Random.Range ( 0, spots.Length )].transform.position;
			Peoples[i] = inst.GetComponent<WatcherController> ();
			Peoples[i].Wandern ( pos );

			if ( Random.value > 0.5f )
				yield return new WaitForSeconds ( 0.1f );
		}

		while ( true )
		{
			for ( int i = 0; i < Peoples.Length; i++ )
			{
				if ( Peoples[i] == null )
				{
					pos = spots[Random.Range ( 0, spots.Length )].transform.position;
					inst = (GameObject)Object.Instantiate ( WandererPrefab );
					inst.transform.position = pos;

					pos = spots[Random.Range ( 0, spots.Length )].transform.position;
					Peoples[i] = inst.GetComponent<WatcherController> ();
					Peoples[i].Wandern ( pos );
				}
			} 

			yield return null;
		}
	}
}
