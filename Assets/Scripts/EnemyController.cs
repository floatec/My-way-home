using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float dangourLevel;
	public bool canHit;
	public VictomController target;
	public bool isActive = false;
	public float hitSpeed = 2;
	private float hitTimeout;
	public float takeLife = 5;
	public InsidantAreaController iac;
	private NavMeshAgent agent;
	private Vector3 disappire=new Vector3(-1111111111111111,-1111111111111111,-1111111111111111);
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			hitTimeout+=Time.deltaTime;
			if(hitTimeout>0){
				hitTimeout=-hitSpeed;
				target.life-=takeLife;
			}
		}
		//Debug.Log (Vector3.Distance (transform.position, disappire));
		if (Vector3.Distance(transform.position,disappire)<=1) {
			 gameObject.SetActive (false);
		}
		if (isActive&&target.life<=0) {
			isActive=false;
			iac.world.player.karma-=500;
			runaway();
		}
	}

	public void runaway(){
		isActive = false;
		iac.onTheRun = true;
		var spots = GameObject.FindGameObjectsWithTag ( "UnitSpawn" );
		var pos = spots[Random.Range ( 0, spots.Length )].transform.position;

		agent.SetDestination ( pos );
		disappire = pos;
	}
}
