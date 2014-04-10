using UnityEngine;

/**
 * The invincibility of runner.
 * Uses principles laid out in booster class.
 * 
 * @author EECS 290 Team 2
 * @author Shaun Howard
 */

public class Invincibility : MonoBehaviour {

	//offset and rotational velocity of transform
	public Vector3 offset, rotationVelocity;

	//offset and chance of spawn
	public float recycleOffset, spawnChance;
	
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}
	
	void Update () {

		//perk is inactive if runner passed it
		if(transform.localPosition.x + recycleOffset < Runner.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}

		//transform rotates
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	/*
	 * When runner enters object, perk is added to runner.
	 */
	void OnTriggerEnter () {
		Runner.AddInvincibility();
		gameObject.SetActive(false);
	}

	/*
	 * Spawn invincibility if placement is optimal for spawn.
	 * Based on spawn chance in platform manager.
	 * <param name="position">the position to spawn the perk</param>
	 * <returns>whether perk can spawn</returns>
	 */
	public bool SpawnIfAvailable (Vector3 position) {

		//when spawn chance in a given random range, don't spawn perk
		if (gameObject.activeSelf || spawnChance <= Random.Range (0f, 100f)) {
			return false;
		}

		//position of perk is offset given runner location
		transform.localPosition = position + offset;

		//perk is activated
		gameObject.SetActive(true);

		return true;
	}

	/*
	 * Perk is not active if game is over.
	 */
	private void GameOver () {
		gameObject.SetActive(false);
	}
}