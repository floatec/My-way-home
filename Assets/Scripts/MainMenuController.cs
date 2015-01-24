using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
	public void OnClickNewGame ()
	{
		Application.LoadLevel ( "World" );
	}

	public void OnClickCredits()
	{

	}

	public void OnClickExit ()
	{

	}
}
