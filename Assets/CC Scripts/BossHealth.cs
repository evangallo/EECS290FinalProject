using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
    private int HP;
	private Done_GameController gameController;

	void Start ()
	{
        HP = 15;
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

