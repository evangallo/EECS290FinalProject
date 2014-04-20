using UnityEngine;
using System.Collections;

/**
 * Evasive Maneuver Controller for Cosmos Commander Final Project.
 * Controls the maneuvers of the enemies in the game.
 * 
 * Right now, dodgeing is random, we need them to be able to dodge asteroids and 
 * not run into each other
 * @authors EECS 290 Team 2
 */
public class EvasiveManeuver : MonoBehaviour
{
	public Done_Boundary boundary;
	public float tilt;
	public float dodge;
	public float smoothing;
	public float stopDelay;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

	private float currentSpeed;
	private float targetManeuver;

	void Start ()
	{
		currentSpeed = rigidbody.velocity.z;
		StartCoroutine(Evade());
	}

	/**
	 * This is where the AI should be implemented, I think. Evasive behavior is 
	 * determined by setting targetManeuver
	 */
	IEnumerator Evade () {
		Vector3 incomingAsteroid = detectAsteroid();
		if (incomingAsteroid.magnitude != 0f){
			if (detectObstacleLeft() && detectObstacleRight()) {
					//Don't dodge
			} else if (incomingAsteroid.x < rigidbody.position.x){
			  	if (detectObstacleRight()){
		  				//Dodge left
				} else {
		  				//Dodge right
				}
			} else {
			  	if (detectObstacleLeft()){
		  				//Dodge right
				} else {
		  				//Dodge left
			  	}
			}
		}

		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true)
		{
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
	
	void FixedUpdate ()
	{
		float newManeuver = Mathf.MoveTowards (rigidbody.velocity.x, targetManeuver, smoothing * Time.deltaTime);
		if (stopDelay > 0) {
			rigidbody.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
			stopDelay -= Time.deltaTime;
		} else {
			rigidbody.velocity = new Vector3 (newManeuver, 0.0f, 0f);
		}
		rigidbody.position = new Vector3
		(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		
		rigidbody.rotation = Quaternion.Euler (0, 0, rigidbody.velocity.x * -tilt);
	}

	/**
	 * Detects if there is an asteroid behind the enemy and returns the asteroid's 
	 * position as a Vector3 or zero vector if there is no asteroid because apparently
	 * Vector3s are non nullable
	 */
	Vector3 detectAsteroid() {
		return new Vector3 (0f, 0f, 0f);
	}

	/**
	 * Detect if there is an asteroid or another enemy to this enemy's left
	 */
	bool detectObstacleLeft() {
		return false;
	}

	/**
	 * Detect if there is an asteroid or another enemy to this enemy's right
	 */
	bool detectObstacleRight() {
		return false;
	}
}
