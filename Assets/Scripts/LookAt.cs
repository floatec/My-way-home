using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
	void LateUpdate ()
	{
		transform.LookAt ( Camera.main.transform );
		transform.localRotation *= Quaternion.AngleAxis ( 180, Vector3.up );
	}
}
