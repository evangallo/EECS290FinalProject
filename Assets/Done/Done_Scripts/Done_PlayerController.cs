using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

/**
 * Player controller for Cosmos Commander Final Project.
 * Controls the player in the game.
 * 
 * @authors EECS 290 Team 2
 */
public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject normalShot, rangeShot;
	public float rangeUpTime, rateUpTime;
	public GameObject shield;
	public Transform shotSpawn;
	public float baseFireRate, rateIncrease;
	public int bombCount;
	public float invincibilityTime = 3f; //default is 3 seconds

	private bool invincible;
	private float fireRate;
	private float nextFire;
	private bool rangeUp, rateUp, bombKeyReady;
	private float rangeUpTimeLeft, rateUpTimeLeft;
	private int bombs;
	private Done_GameController gameController;
	private float timer;

	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Done_GameController>();
		bombs = bombCount;
		gameController.SetBombText(bombs);
		fireRate = baseFireRate;
		rangeUp = false;
		rateUp = false;
		bombKeyReady = true;
		rangeUpTimeLeft = 0f;
		rateUpTimeLeft = 0f;
	}
	
	void Update ()
	{
		if (rangeUpTimeLeft > 0) {
			rangeUp = true;
			rangeUpTimeLeft -= Time.deltaTime;
		} else {
			rangeUp = false;
		}
		if (rateUpTimeLeft > 0) {
			rateUp = true;
			rateUpTimeLeft -= Time.deltaTime;
		} else {
			rateUp = false;
		}

		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			if (rateUp) {
				fireRate = baseFireRate + rateIncrease;
			} else {
				fireRate = baseFireRate;
			}
			nextFire = Time.time + 1f / fireRate;

			GameObject shot = null;
			if (rangeUp) {
				shot = rangeShot;
			} else {
				shot = normalShot;
			}
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}
        if (Input.GetButton("Fire2") && bombs > 0 && bombKeyReady)
        {
			bombKeyReady = false;
            Detonate();
			bombs--;
			gameController.SetBombText(bombs);
        }else if(!Input.GetButton("Fire2") && bombs > 0 && !bombKeyReady){
			bombKeyReady = true;
		}

		if (invincible)
			timer -= Time.deltaTime;
		
		if(timer <= 0)
			invincible = false; 
	}

	void FixedUpdate ()
	{
		float moveHorizontal = - Input.GetAxis ("Vertical");
		float moveVertical = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;
		
		rigidbody.position = new Vector3
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

    void Detonate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject victim in enemies)
        {
            victim.BroadcastMessage("Damage", SendMessageOptions.DontRequireReceiver);
        }
        enemies = GameObject.FindGameObjectsWithTag("Hazard");
        foreach (GameObject victim in enemies)
        {
            victim.BroadcastMessage("Damage", SendMessageOptions.DontRequireReceiver);
        }
    }

	 public void rangeUpgrade () {
		rangeUpTimeLeft = rangeUpTime;
	}

	public void rateUpgrade () {
		rateUpTimeLeft = rateUpTime;
	}

	public void generateShield () {
		if (GameObject.FindGameObjectWithTag("Shield") == null) {
			GameObject shieldClone = Instantiate(shield, transform.position, transform.rotation) as GameObject;
			shieldClone.transform.parent = transform;
		}
	}

	
	public bool isInvincible(){
		return invincible;
	}
	
	public void SetPowerUp(string powerUp){
		switch(powerUp){
		case "Invincibility":
			Debug.Log ("Set invincible");
			timer = invincibilityTime;
			invincible = true;
			break;
		case "FireRateIncrease":
			Debug.Log ("Set fire rate increase");
			baseFireRate += .5f;
			break;
		}
	}
}
