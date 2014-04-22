using UnityEngine;
using System.Collections;

/**
 * Object destroyer on contact for Cosmos Commander Final Project.
 * Destroys objects on contact for the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
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

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
        Damage();

        Destroy(other.gameObject);
	}
    void Damage()
    {

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        gameController.AddScore(scoreValue);
        Destroy(gameObject);
    }
}