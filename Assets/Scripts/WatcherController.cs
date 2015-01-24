using UnityEngine;
using System.Collections;

public class WatcherController : MonoBehaviour
{
	private bool MoveToAndDestroy;

	private float willhelp;
	private NavMeshAgent agent;

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
	}

	public void Wandern ( Vector3 target )
	{
		MoveToAndDestroy = true;
		agent.SetDestination ( target );
	}

	public bool askForHelp ()
	{
		willhelp += .3f;
		return willhelp >= 1;
	}
}
