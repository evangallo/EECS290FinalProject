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
			Instantiate (chargeEffect, beamSpawn.position, beamSpawn.rotation);
			yield return new WaitForSeconds (chargeTime);
			Instantiate (beam, beamSpawn.position, beamSpawn.rotation);
			yield return new WaitForSeconds (beamTime);
			yield return new WaitForSeconds (waitTime);
		}
	}
}
