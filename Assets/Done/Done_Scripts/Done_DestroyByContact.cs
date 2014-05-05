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
	public string objectType = "enemyShip"; //default to enemy ship
	private Done_GameController gameController;
	private PickupDropper pickupDropper;

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

		pickupDropper = gameObject.GetComponent <PickupDropper> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy" || 
		    (other.tag == "Player" && other.gameObject.GetComponent<Done_PlayerController> ().isInvincible())
		    || other.tag == "Pickup" || other.tag == "Invincibility" || other.tag == "FireRateIncrease")
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

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
		gameController.AddScore (objectType);

		if (pickupDropper != null) {
			pickupDropper.drop();
		}
        Destroy(gameObject);
    }

}