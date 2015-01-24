using UnityEngine;
using System.Collections;

public class InsidantAreaController : MonoBehaviour
{
	private bool triggered = false;
	public VictomController[] Victims;
	public EnemyController[] Enemies;
	void OnTriggerEnter ( Collider other )
	{
		if ( other.CompareTag ( "Player" ) )
		{
			if ( !triggered )
			{
				Debug.Log ( "fight !!!" );
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
		foreach ( var enemy in Enemies )
		{
			enemy.runaway();
		}
	}

}
