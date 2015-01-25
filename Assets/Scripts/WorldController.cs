using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WorldController : MonoBehaviour
{
	public CameraController Camera;
	public GameObject PlayerPrefab;
	public GameObject WandererPrefab;

	private WatcherController[] Peoples;
	public List<GameObject> Polices;

	void Awake ()
	{
		Application.LoadLevelAdditive ( "World_UI" );
	}

	IEnumerator Start ()
	{
		var root = new GameObject ( "Watcher root" ).transform;

		var spots = GameObject.FindGameObjectsWithTag ( "PlayerSpawn" );
		var pos = spots[Random.Range ( 0, spots.Length )].transform.position;

		var inst = (GameObject)Object.Instantiate ( PlayerPrefab );
		inst.transform.position = pos;
		inst.GetComponent<Player> ().world=this;
		Camera.Target = inst.GetComponent<Player> ();
		WorldUIController.Instance.player = inst.GetComponent<Player> ();


		int idx = 0;
		spots = GameObject.FindGameObjectsWithTag ( "UnitSpawn" );

		Peoples = new WatcherController[100];
		for ( int i = 0; i < Peoples.Length; i++ )
		{
			pos = spots[idx].transform.position;
			inst = (GameObject)Object.Instantiate ( WandererPrefab );
			inst.transform.parent = root;
			inst.transform.position = pos;

			pos = spots[Random.Range ( 0, spots.Length )].transform.position;
			Peoples[i] = inst.GetComponent<WatcherController> ();
			Peoples[i].Wandern ( pos );

			idx++;
			if ( idx >= spots.Length ) idx = 0;

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
					inst.transform.parent = root;
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
