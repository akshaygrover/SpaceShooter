using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Boundary") || other.CompareTag("Enemy")){
			return;
		}
		if(explosion != null){
		Instantiate(explosion,transform.position,transform.rotation);
		} 
		if(other.tag == "Player"){

		Instantiate(playerExplosion,other.transform.position,other.transform.rotation);
		gameController.GameOver();
		}
		Debug.Log(other.name);
		
		gameController.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if(gameController == null){
			Debug.Log("cannot find gamecontroller script");
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}
