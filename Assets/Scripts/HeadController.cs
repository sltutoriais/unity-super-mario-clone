using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {

	public  GameObject owner;

	//JUMP SOUND
	public AudioClip kickSound;

	public bool isDeath;
	public float animTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D colisor)
	{


		if (colisor.gameObject.tag == "Footer" && GameManager.instance.player.GetComponent<PlayerController>().jumped) {

			StartCoroutine ("DeathAnim");




		}

	}

	IEnumerator DeathAnim()
	{
		if (isDeath)
		{
			yield break;
		}

		isDeath = true;
		PlayKickSound ();
		GetComponentInParent<CircleCollider2D> ().isTrigger = true;
		yield return new WaitForSeconds(animTime); 
		Destroy (owner);
	}
	public void PlayKickSound()
	{
		GetComponent<AudioSource>().PlayOneShot(kickSound);
	}


}
