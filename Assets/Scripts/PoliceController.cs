using UnityEngine;
using System.Collections;

public class PoliceController : MonoBehaviour {

	private NavMeshAgent agent;
	public InsidantAreaController iac;
	private Vector3 target;

	// Use this for initialization
	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		var spots = GameObject.FindGameObjectsWithTag ( "UnitSpawn" );
		var pos = spots[Random.Range ( 0, spots.Length )].transform.position;
		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position,target)<=1) {
			iac.policeArived();
			gameObject.SetActive (false);
		}
	}

	public void moveTo(Vector3 pos,InsidantAreaController iac){
		this.iac = iac;
		Debug.Log (pos.ToString());
		agent.SetDestination(pos);
		target = pos;
	}
}
