using UnityEngine;
using System.Collections;

/**
 * Manages main menu buttons in space shooter game.
 * 
 * @authors EECS 290 Team 2
 */
public class MainMenu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}

	/**
	 * Check if directions menu should load.
	 */
	void Update() {

		//Press Tab to load directions menu
		if (Input.GetKeyDown (KeyCode.Tab)) {
			DontDestroyOnLoad (GameObject.FindGameObjectWithTag ("Music"));
			Application.LoadLevel (6);
		}
	}
	
	/**
	 * Determine which level to load based on button clicked
	 * or input key pressed.
	 */
	void OnClick () {
		switch (this.tag)
		{
		case "Start": //Load game mode submenu
			DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Music"));
			Application.LoadLevel (1);
			break;
		case "Quit": //Quit game
			Application.Quit ();
			break;
		case "Classic": //Load classic mode
			Application.LoadLevel (2);
			break;
		case "Survival": //Load survival mode
			Application.LoadLevel(3);
			break;
		case "Time Attack": //Load time attack mode
			Application.LoadLevel (4);
			break;
		case "Challenge": //Load challenge mode
			Application.LoadLevel (5);
			break;
		case "MainMenu": //Load main menu 
			Application.LoadLevel (0);
			break;
		}
	}
}
