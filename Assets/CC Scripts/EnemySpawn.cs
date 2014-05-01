using UnityEngine;
using System.Collections;

/**
 * Enemy Spawner for Cosmos Commander Final Project.
 * Spawns waves of enemies for each mode of the game.
 * 
 * @authors EECS 290 Team 2
 */
public class EnemySpawn : MonoBehaviour
{
	public string gameMode; // classic, survival, time attack or challenge
	public Vector3 spawnValues;
	public float spawnCheckWait;
	public float startWait;
	public float waveWait;
	public float maxSpawnWait, minSpawnWait;
	public float spawnWaitDecrement;
	//public int hazardCount;
	public GameObject[] enemyWaves;
	public GameObject[] placeHolders; //place holders are instantiated then destroyed to make 
	//sure enemies do not spawn on top of asteroids. This array must contain object groups 
	//corresponding to each wave of enemies.
	private float currentWait;
	private Done_GameController gameController;
	//private bool fullCount;
	
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");

		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}

		switch (gameMode) //select game mode coroutine
		{
			case "classic":
			//fullCount = false;
			//hazardCount = hazardCount / 2;
			StartCoroutine (SpawnClassicWaves ());
			break;

			case "survival":
			currentWait = maxSpawnWait;
			StartCoroutine (SpawnSurvivalWaves ());
			break;

			case "time attack":
			StartCoroutine (SpawnClassicWaves ());
			break;

			case "challenge":
			StartCoroutine (SpawnClassicWaves ());
			break;
		}
	}

	/**
	 * Spawn the enemy waves for classic game mode.
	 */
	IEnumerator SpawnClassicWaves ()
	{
		if (enemyWaves.Length != placeHolders.Length) {
			Debug.Log("placeholders and enemy waves do not match");
		}

		yield return new WaitForSeconds (startWait);

		for (int i = 0; i < enemyWaves.Length; i++)
		{
			if (gameMode == "time attack")
				gameController.beginTimer();

			Vector3 spawnPosition = new Vector3 (0f, spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			GameObject placeHolder = Instantiate (placeHolders[i], spawnPosition, spawnRotation) as GameObject;
			Destroy(placeHolder);
			Instantiate (enemyWaves[i], spawnPosition, spawnRotation);

			while (enemiesArePresent())
				yield return new WaitForSeconds (spawnCheckWait);

			gameController.EndTimer ();

			yield return new WaitForSeconds (waveWait);
		}
		gameController.Victory();

	}

	/**
	 * Spawn the enemy waves for survival game mode.
	 */
	IEnumerator SpawnSurvivalWaves ()
	{
		if (enemyWaves.Length != placeHolders.Length) {
			Debug.Log("placeholders and enemy waves do not match");
		}

		yield return new WaitForSeconds (startWait);

		while (true) 
		{
			for (int i = 0; i < enemyWaves.Length; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject placeHolder = Instantiate (placeHolders[i], spawnPosition, spawnRotation) as GameObject;
				Destroy(placeHolder);
				Instantiate (enemyWaves[i], spawnPosition, spawnRotation);
				yield return new WaitForSeconds (currentWait);
			}
	
			if (currentWait > minSpawnWait)
			{
				currentWait -= spawnWaitDecrement;
			} else {
				currentWait = minSpawnWait;
			}
			//yield return new WaitForSeconds (waveWait);
		}
	}

	/**
	 * Check whether enemies are in the viewing area.
	 */
	bool enemiesArePresent() {
		
		int enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		
		if (enemyCount < 1) 
		{
			return false;
		}

		return true;
	}
}
