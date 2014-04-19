using UnityEngine;
using System.Collections;

/**
 * The Main sub menu stage selection script.
 * 
 * @author Shaun Howard
 */
public class MainSubMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Selects the game mode based on object tags.
	 */
	void OnClick() {
		//Selects which game mode to implement by tag.
		switch(this.tag) {
			case "Survival": //Load survival mode
				Application.LoadLevel(3);
				break;
			case "Time Attack": //Load time attack mode
				Application.LoadLevel (4);
				break;
			case "Challenge": //Load challenge mode
				Application.LoadLevel (5);
				break;
			default: //Load classic mode by default
				Application.LoadLevel (2);
				break;
		}
	}
}
