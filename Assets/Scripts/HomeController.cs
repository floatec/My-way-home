using UnityEngine;
using System.Collections;

public class HomeController : MonoBehaviour
{
	void OnTriggerEnter ( Collider other )
	{
		if ( other.CompareTag ( "Player" ) )
		{
			Debug.Log ( "home!" );
			Application.LoadLevel ( "EndHouse" );
		}
	}
}
