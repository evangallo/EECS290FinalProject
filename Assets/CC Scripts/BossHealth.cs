using UnityEngine;
using System.Collections;

/**
 * Boss health for Cosmos Commander Final Project.
 * Controls the boss health in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class BossHealth : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
    public int HP;

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
		if (other.tag == "Boundary" || other.tag == "Enemy" || 
		    (other.tag == "Player" && other.gameObject.GetComponent<Done_PlayerController> ().isInvincible()))
		{
			return;
		}
		
		if (other.tag == "Player" && !other.gameObject.GetComponent<Done_PlayerController> ().isInvincible())
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
			gameController.GameOver();
		}
		
		Damage();
		Destroy(other.gameObject);
	}
    void Damage()
    {
        HP--;
        if (HP <= 0)
        {
            Death();
        }

    }
    void Death()
    {

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        gameController.AddScore("boss");
        Destroy(gameObject);
    }
}

