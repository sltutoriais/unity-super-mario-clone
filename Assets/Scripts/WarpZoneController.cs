using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpZoneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D colisor)
	{




		if (colisor.gameObject.tag == "Player") {

			GameManager.instance.GameOver ();
			GameManager.instance.StartGame ();



		}
	}
}
