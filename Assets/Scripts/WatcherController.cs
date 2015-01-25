using UnityEngine;
using System.Collections;

public class WatcherController : MonoBehaviour
{
	private bool MoveToAndDestroy;

	private float willhelp;
	private NavMeshAgent agent;
	private float cooldown = 0;
	private bool stoped = false;

	void Awake ()
	{
		agent = GetComponent<NavMeshAgent> ();
		agent.enabled = false;
		willhelp = Random.Range ( 0, 11 ) / 10;
	}

	void Update ()
	{
		if ( MoveToAndDestroy )
		{
			if ( !agent.pathPending )
			{
				if ( agent.remainingDistance <= agent.stoppingDistance )
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

		if ( willhelp >= 1 || cooldown <= 0 )
		{
			if ( agent.enabled ) agent.Stop ();
			stoped = true;
		}
		else
		{
			if ( stoped )
			{
				if ( agent.enabled ) agent.Resume ();
				stoped = false;
			}
		}

	}

	public void Wandern ( Vector3 target )
	{
		MoveToAndDestroy = true;
		agent.enabled = true;
		agent.SetDestination ( target );
	}

	public bool askForHelp ( float strength )
	{
		willhelp += strength;
		cooldown = -3;
		return willhelp >= 1;
	}
}
