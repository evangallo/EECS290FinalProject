using UnityEngine;
using System.Collections;

/**
 * Boss Weapon controller for Cosmos Commander Final Project.
 * Controls the weapons of the boss in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class BossWeapon : MonoBehaviour
{
	public GameObject chargeEffect;
	public GameObject beam;
	public Transform beamSpawn;
	public int shots;
	// The lifetimes of lightning beam and tractor beam need to be adjusted from their respective scripts.
	public float chargeTime, beamTime, waitTime;
	public float delay;

	void Start ()
	{
		StartCoroutine(Fire());
	}

	IEnumerator Fire ()
	{
		yield return new WaitForSeconds (delay);
		while (true) {
			audio.Play ();
			GameObject chargeClone = Instantiate (chargeEffect, beamSpawn.position, beamSpawn.rotation) as GameObject;
			chargeClone.transform.parent = transform;
			yield return new WaitForSeconds (chargeTime);
			for (int i = 0; i < shots; i++) {
				GameObject beamClone = Instantiate (beam, beamSpawn.position, beamSpawn.rotation) as GameObject;
				beamClone.transform.parent = transform;
				yield return new WaitForSeconds (beamTime/shots);
			}
			yield return new WaitForSeconds (waitTime);
		}
	}
}
