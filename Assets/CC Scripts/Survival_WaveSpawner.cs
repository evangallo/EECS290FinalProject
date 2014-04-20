using UnityEngine;
using System.Collections;

/**
 * Wave Spawner for Cosmos Commander Final Project.
 * Spawns waves of enemies for each game mode of the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Survival_WaveSpawner : MonoBehaviour
{
	public Vector3 spawnValues;
	public float maxSpawnWait, minSpawnWait;
	public float spawnWaitDecrement;
	public float startWait;
	public GameObject enemy;

	private float currentWait;

	void Start ()
	{
		currentWait = maxSpawnWait;
		StartCoroutine (SpawnWaves ());
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);

		while (true) {
			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (enemy, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (currentWait);
			if (currentWait > minSpawnWait) {
				currentWait -= spawnWaitDecrement;
			} else {
				currentWait = minSpawnWait;
			}
		}
	}

}