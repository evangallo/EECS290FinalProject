using UnityEngine;
using System.Collections;

/**
 * Object destroyed by boundary for Cosmos Commander Final Project.
 * Destroys objects by boundary for the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit (Collider other) 
	{
		Destroy(other.gameObject);
	}
}