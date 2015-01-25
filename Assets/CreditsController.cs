using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour
{
	void Update ()
	{
		if ( Input.anyKeyDown )
		{
			Application.LoadLevel ( "MainMenu_Subway" );
		}
	}
}
