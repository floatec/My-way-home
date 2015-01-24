using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	[HideInInspector]
	public Player Target;

	private Vector3 offset;

	void Start ()
	{
		offset = transform.position;
	}

	void Update ()
	{
		transform.position = Target.transform.position + offset;
	}
}
