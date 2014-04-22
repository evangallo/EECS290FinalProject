using UnityEngine;
using System.Collections;

/**
 * Explosion controller for Cosmos Commander Final Project.
 * Destroys bullets on contact with enemy for the game.
 * 
 * @authors EECS 290 Team 2
 */
public class ExplosionControl : MonoBehaviour
{
	public GameObject explosion;
	public GameObject enemyExplosion;
	public int scoreValue;
	private Done_GameController gameController;
	
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	/**
	 * On trigger enter, explode. 
	 */
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary")
		{
			return;
		}

		if (other.tag == "Enemy")
		{
			if (explosion != null)
				Instantiate(explosion, transform.position, transform.rotation);
			if (enemyExplosion != null)
				Instantiate(enemyExplosion, other.transform.position, other.transform.rotation);
			gameController.AddScore(scoreValue);
			//gameController.GameOver();
		}

		//Destroy (other.gameObject);
		//Destroy (gameObject);
	}
}