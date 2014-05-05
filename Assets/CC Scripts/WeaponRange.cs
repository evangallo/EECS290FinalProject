using UnityEngine;
using System.Collections;


/**
 * Weapon range manager for Cosmos Commander Final Project.
 * Controls the range of the player's weapon in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class WeaponRange : MonoBehaviour {

	public float range;

	private Vector3 startPosition;

	void Start () {
		startPosition = transform.position;
	}
	
	void Update () {
		float distanceTraveled = Vector3.Magnitude (startPosition - transform.position);
		if (distanceTraveled >= range) {
			Destroy (this.gameObject);
		}
	
	}
}
