using UnityEngine;
using System.Collections;

/**
 * Ends the space shooter game.
 * 
 * @authors EECS 290 Team 2
 */
public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * On click, button quits game.
	 */
	void OnClick() {
		Application.Quit ();
	}
}
