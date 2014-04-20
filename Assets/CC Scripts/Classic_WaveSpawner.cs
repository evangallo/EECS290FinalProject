using UnityEngine;
using System.Collections;

/**
 * Wave Spawner for Cosmos Commander Final Project.
 * Spawns waves of enemies for each game mode of the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Classic_WaveSpawner : MonoBehaviour
{
	public Vector3 spawnValues;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GameObject[] enemyWaves;

	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);

		for (int i = 0; i < enemyWaves.Length; i++) {
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

	bool enemiesArePresent() {
		int enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (enemyCount < 1) {
			return false;
		}
		return true;
	}
}