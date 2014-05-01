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
	public string gameMode;
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
	public GUIText timerText;
	public GUIText bombAmmoText;

	private float timer;
	private bool isTiming;
	private bool gameOver;
	private int score;
	private Done_GameController gameController;
	
	void Start ()
	{
		isTiming = false;
		gameOver = false;
		restartText.text = "";
		gameOverText.text = "";
		changeModeText.text = "";
		timerText.text = "";
		score = 0;
		UpdateScore ();
		if (hazards.Length > 0) //Do not spawn hazards if there are none.
			StartCoroutine (SpawnWaves ());

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	void Update ()
	{
		
		if(isTiming)  //if timing, add to timer
		{
			timer += Time.deltaTime;
			UpdateTimer ();
		}

		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();

		if (gameOver)
		{
			EndTimer (); //stops timing
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

	public void beginTimer()
	{
		timer = 0;
		isTiming = true;
	}
	
	/*void update(){
		if(isTiming)
		{
			//+= is the same thing as adding to the current variable
			//timer = timer + time.delatime is the same thing as time +=... its just faster to use +=
			
			timer += time.deltatime;
			
		}
		
		if (timer > NumberOfSecondsToWait)
		{
			//do something, like destroy;
		}
		
	} */
	
	public void EndTimer(){
		isTiming = false;
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
	
	public void AddScore (string objectType)
	{
		switch (gameMode) //select game mode scoring scheme
		{
		case "classic":
			if (objectType == "enemyShip")
				score += 20;
			if (objectType == "asteroid")
				score += 10;
			if (objectType == "boss") 
				score += 50;
			break;
			
		case "survival":
			if (objectType == "enemyShip")
				score += 20;
			if (objectType == "asteroid")
				score += 10;
			if (objectType == "boss") 
				score += 50;
			break;
			
		case "time attack":
			if (objectType == "enemyShip")
				score += (int)(20f / timer) + 20;
			if (objectType == "asteroid")
				score += (int)(10f / timer) + 10;
			if (objectType == "boss") 
				score += (int)(50f / timer) + 50;
			break;
			
		case "challenge":
			if (objectType == "enemyShip")
				score += 10;
			if (objectType == "asteroid")
				score += 5;
			if (objectType == "boss") 
				score += 50;
			break;
		}

		UpdateScore ();
	}

	public int GetScore ()
	{
		return score;
	}

	void UpdateTimer()
	{
		timerText.text = "Time: " + (int)timer;
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

	public void SetBombText(int newBombCount){
		bombAmmoText.text = "Bombs: " + newBombCount;
	}

    public void Victory()
    {
        gameOverText.text = "Player Wins!";
        gameOver = true;
    }
}