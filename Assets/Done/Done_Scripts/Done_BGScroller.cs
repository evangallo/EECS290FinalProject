using UnityEngine;
using System.Collections;

/**
 * Background scroller for Cosmos Commander Final Project.
 * Scrolls the background for the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;

	private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;
	}

	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}