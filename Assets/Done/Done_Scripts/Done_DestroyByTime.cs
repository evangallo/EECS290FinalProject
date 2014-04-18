using UnityEngine;
using System.Collections;

/**
 * Objecy destroyer for Cosmos Commander Final Project.
 * Destroys objects by given time in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_DestroyByTime : MonoBehaviour
{
	public float lifetime;

	void Start ()
	{
		Destroy (gameObject, lifetime);
	}
}
