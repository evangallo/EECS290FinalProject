using UnityEngine;
using System.Collections;

/**
 * Weapon controller for Cosmos Commander Final Project.
 * Controls the weapons of the ships in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		audio.Play();
	}
}
