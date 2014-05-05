using UnityEngine;
using System.Collections;

/**
 * Power up manager for Cosmos Commander Final Project.
 * Controls the power ups obtained in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class PowerUp : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other){
		
		if (other.tag == "Player"){
			other.gameObject.GetComponent<Done_PlayerController> ().SetPowerUp (this.tag);
			Destroy (gameObject);
		}
	}
}
