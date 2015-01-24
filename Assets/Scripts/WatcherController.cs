using UnityEngine;
using System.Collections;

public class WatcherController : MonoBehaviour {

	private float willhelp;
	// Use this for initialization
	void Start () {
		willhelp = Random.Range (0, 11) / 10;
	}


	public bool askForHelp(){
		willhelp += .3f;
		return willhelp >= 1;
	}
}
