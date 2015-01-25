using UnityEngine;
using System.Collections;

public class InsidantAreaController : MonoBehaviour
{
	private bool triggered = false;
	public VictomController[] Victims;
	public EnemyController[] Enemies;
	public bool onTheRun =false;
	public WorldController world;

	void OnTriggerEnter ( Collider other )
	{
		if ( other.CompareTag ( "Player" ) )
		{
			if ( !triggered )
			{
				triggered = true;
				foreach ( var enemy in Enemies )
				{
					enemy.isActive = true;
				}
			}
			other.gameObject.GetComponent<Player> ().iac = this;
		}

	}
	void OnTriggerExit ( Collider other )
	{
		if ( other.CompareTag ( "Player" ) )
		{
			other.gameObject.GetComponent<Player> ().leaveArea();
		}

	}


	void Update ()
	{
		if ( triggered )
		{

		}
	}

	public bool policeArived(){
		bool AllAlive = true;
		foreach ( var Victim in Victims )
		{
			if(Victim.life<=0){
				AllAlive=false;
			}
	
		}
		bool condition = !onTheRun && AllAlive && world.player.iac == this;
			if(condition){
				world.player.karma+=350;
			}

		runaway ();
		return condition;
		
	}

	public bool isStronger ( float strongnes )
	{
		float count = 0;
		foreach ( var enemy in Enemies )
		{
			count += enemy.dangourLevel;
		}
		return strongnes >= count;
	}

	public void runaway ()
	{
		onTheRun = true;
		foreach ( var enemy in Enemies )
		{
			enemy.runaway();
		}
	}

}
