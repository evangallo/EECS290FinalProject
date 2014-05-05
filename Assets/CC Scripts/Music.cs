using UnityEngine;
using System.Collections;

/**
 * Music controller for Cosmos Commander Final Project.
 * Controls the music in the menu.
 * 
 * @authors EECS 290 Team 2
 */
public class Music : MonoBehaviour {
	private static Music instance = null;
	public static Music Instance {
		get{ return instance;}
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
