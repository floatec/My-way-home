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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			hitTimeout+=Time.deltaTime;
			if(hitTimeout>0){
				hitTimeout=-hitSpeed;
				target.life-= takeLife;
			}
		}
	}
}
