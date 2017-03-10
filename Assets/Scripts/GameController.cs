﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private int score;
	private bool restart;
	private bool gameOver;

	// Use this for initialization
	void Start () {
		gameOver =false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
	StartCoroutine(SpawnWaves());
		
	}
	
	// Update is called once per frame
	void Update () {
		if(restart){
			if(Input.GetKeyDown(KeyCode.R)){
			//	Application.LoadLevel (Application.loadedLevel);
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
		
	}
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while(true)
		{
		for(int i =0;i<hazardCount;i++)
		{
			GameObject hazard = hazards[Random.Range(0,hazards.Length)];
		Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x,spawnValues.x),spawnValues.y,spawnValues.z);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate(hazard,spawnPosition,spawnRotation);
		yield return new WaitForSeconds(spawnWait);
		}
		yield return new  WaitForSeconds(waveWait);
		if(gameOver){
			restartText.text = "press 'R' for restart";
			restart =true;
			break;
		}

		}
	}
	void UpdateScore(){
		scoreText.text = "Score : " + score;
	}
	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore();

	}
	public void GameOver(){
		
		gameOverText.text = "Game Over!!";
	  GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
	  foreach(GameObject go in gos)
	  {
		  Destroy(go);
		  StopCoroutine(SpawnWaves());
	  }
		gameOver = true;

		
	}
}
