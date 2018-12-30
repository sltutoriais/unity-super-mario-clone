using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class GameManager : MonoBehaviour {

	public static GameManager instance;//util para qualquer gameObject acessar esta classe sem a necessidade de instancia-la ou declara-la

	public GameObject playerPref;
	public GameObject[] enemyPrefabs;
	public GameObject player = null;
	public ArrayList enemiesList;
	public Transform spawPlayerTrans;
	public Transform[] spawnEnemyTrans;
	public CameraFollow  camera2D;
	public Transform[] points;
	public PathDefinitions path;

	// Use this for initialization
	void Start () {

		if (instance == null) {

			instance = this;
			StartGame ();
		}
		
	}

	public void StartGame()
	{
	    camera2D.GetComponent<AudioSource>().Stop();
		camera2D.GetComponent<AudioSource>().Play();
		enemiesList = new ArrayList ();
		SpawnPlayer ();
		SpawnEnemies();

	}

	public void SpawnPlayer()
	{
		player = GameObject.Instantiate (playerPref, spawPlayerTrans.position,spawPlayerTrans.rotation) as GameObject;
		camera2D.SetTarget (player);

	}
	
	public void SpawnEnemies()
	{
	
	    
		GameObject enemy = GameObject.Instantiate (enemyPrefabs[0],spawnEnemyTrans[0].position,spawnEnemyTrans[0].rotation) as GameObject;
		enemy.GetComponent<FollowPath>().SetPathPoints(points,path);
		enemiesList.Add (enemy);

	}


	public void GameOver()
	{
		
		foreach (GameObject enemy in enemiesList) 
		{
			Destroy (enemy);

		}
		enemiesList.Clear ();
	
		Destroy (player);
	}

	

	// Update is called once per frame
	void Update () {
		
	}
}
