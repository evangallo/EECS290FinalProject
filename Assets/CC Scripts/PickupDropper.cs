using UnityEngine;
using System.Collections;

/**
 * Pickup dropper for Cosmos Commander Final Project.
 * Controls the pickup drops in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class PickupDropper : MonoBehaviour {

	public GameObject[] pickups;
	public float dropChance;

	public void drop () {
		if (Random.value < dropChance) {
			Instantiate(pickups[Mathf.FloorToInt(Random.Range(0,pickups.Length))],
			            transform.position, transform.rotation);
		}
	}
}
