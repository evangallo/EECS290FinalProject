using UnityEngine;
using System.Collections;

/**
 * Random Rotator for Cosmos Commander Final Project.
 * Controls the rotation of the asteroids in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_RandomRotator : MonoBehaviour 
{
	public float tumble;
	
	void Start ()
	{
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}