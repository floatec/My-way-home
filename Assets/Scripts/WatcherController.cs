using UnityEngine;
using System.Collections;

public class WatcherController : MonoBehaviour
{
	private bool MoveToAndDestroy;

	private float willhelp;
	private NavMeshAgent agent;
	private float cooldown=0;
	private bool stoped=false;
	void Awake ()
	{
		agent = GetComponent<NavMeshAgent> ();
		willhelp = Random.Range ( 0, 11 ) / 10;
	}

	void Update ()
	{
		if ( MoveToAndDestroy )
		{
			if ( !agent.pathPending )
			{
				if ( agent.remainingDistance <= agent.stoppingDistance + 1 )
				{
					if ( !agent.hasPath )
					{
						// Done
						Destroy ( gameObject );
					}
				}
			}
		}
		cooldown += Time.deltaTime;

		if (willhelp >= 1 || cooldown <= 0) {
			agent.Stop ();
			stoped=true;
		} else {
			if(stoped){
				agent.Resume();	
				stoped=false;
			}
		}

	}

	public void Wandern ( Vector3 target )
	{
		MoveToAndDestroy = true;
		agent.SetDestination ( target );
	}

	public bool askForHelp ( float strength )
	{
		willhelp += strength;
		cooldown = -3;
		return willhelp >= 1;
	}
}
