using UnityEngine;
using System.Collections;

public class VictomController : MonoBehaviour {

	public float life=100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			GetComponent<MeshRenderer>().material.color=Color.gray;		
		}
	}
}
