using UnityEngine;
using System.Collections;

/**
 * Game controller for Cosmos Commander Final Project.
 * Controls the game play and game modes of the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText changeModeText;
	
	private bool gameOver;
	private int score;
	
	void Start ()
	{
		gameOver = false;
		restartText.text = "";
		gameOverText.text = "";
		changeModeText.text = "";
		score = 0;
		UpdateScore ();
		if (hazards.Length > 0) //Do not spawn hazards if there are none.
			StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();

		if (gameOver)
		{
			restartText.text = "Press 'R' for Restart";
			changeModeText.text = "Press 'M' for Mode Selection";

			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}

			if (Input.GetKeyDown (KeyCode.M)) //loads mode select menu
			{
				Application.LoadLevel (1);
			}
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);

			}

			yield return new WaitForSeconds (waveWait);
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	public int GetScore ()
	{
		return score;
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}