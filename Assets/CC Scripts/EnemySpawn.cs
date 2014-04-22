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
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float maxSpawnWait, minSpawnWait;
	public float spawnWaitDecrement;
	public GameObject[] enemyWaves;

	private float currentWait;
	
	void Start ()
	{
		currentWait = maxSpawnWait;


		switch (gameMode) //select game mode coroutine
		{

			case "classic":
			StartCoroutine (SpawnClassicWaves ());
			break;

			case "survival":
			currentWait = maxSpawnWait;
			StartCoroutine (SpawnSurvivalWaves ());
			break;
			//Time attack and challenge use the same routine as classic.
			//A different set of waves can be determined for each scene.
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
	 * Classic mode runs one cycle of a predetermined set of waves, 
	 * spawning the next wave only once the current wave has been cleared.
	 */
	IEnumerator SpawnClassicWaves ()
	{
		yield return new WaitForSeconds (startWait);
		

		for (int i = 0; i < enemyWaves.Length; i++){
			GameObject enemyWave = enemyWaves [i];
			Vector3 spawnPosition = new Vector3 (0f, spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (enemyWave, spawnPosition, spawnRotation);
			while (enemiesArePresent()) {
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}

			
	}

	/**
	 * Spawn the enemy waves for survival game mode.
	 * Survival mode repeatedly spawns a single enemy at decreasing time intervals.
	 */
	IEnumerator SpawnSurvivalWaves ()
	{

		yield return new WaitForSeconds (startWait);
		
		while (true) {
			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 
			                                     spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (enemyWaves[0], spawnPosition, spawnRotation);
			yield return new WaitForSeconds (currentWait);
			if (currentWait > minSpawnWait){
				currentWait -= spawnWaitDecrement;
			} else {
				currentWait = minSpawnWait;
			}
		}
	}

	/**
	 * Check whether enemies are in the viewing area.
	 * For some reason, this causes a really long delay and I don't know why.
	 * At least it works.
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
