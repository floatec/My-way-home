using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
	public GameObject Preview;


	public void Awake ()
	{
		Preview.SetActive ( false );
	}

	public void OnClickNewGame ()
	{

		GetComponent<Animator> ().Play ( "MainMenuFadeout" );
	}

	public void OnClickCredits ()
	{

	}

	public void OnClickExit ()
	{
		Application.Quit ();
	}

	public void FinishFadeoutMenu ()
	{
		Preview.SetActive ( true );

		StartCoroutine ( WaitGame () );
	}

	IEnumerator WaitGame ()
	{
		yield return new WaitForSeconds ( 5 );

		GetComponent<Animator> ().Play ( "MainMenuOut" );


		yield return new WaitForSeconds ( 1 );
		Application.LoadLevel ( "World" );
	}
}
