using UnityEngine;
using System.Collections;

/**
 * Mover for Cosmos Commander Final Project.
 * Controls the forward movement of the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_Mover : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		rigidbody.velocity = transform.forward * speed;
	}
}
