using UnityEngine;
using System.Collections;

/**
 * Starts the space shooter game.
 * 
 * @authors EECS 290 Team 2
 */
public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * On click, button loads next menu scene for 
	 * game mode selection.
	 */
	void OnClick() {
		Application.LoadLevel (1);
	}
}
