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
public class BossMover : MonoBehaviour
{
	public Done_Boundary boundary;
	public float tilt;
	public float speed;
	public float rotationSpeed;
	public float smoothing;
	public float stopDelay;
	public float maneuverTime;
	public float maneuverWait;

	private float currentSpeed;
	private float targetManeuver;
	private float targetRotation;

	void Start ()
	{
		currentSpeed = rigidbody.velocity.z;
		StartCoroutine(Move());
		//StartCoroutine (Aim());
	}

	/**
	 * This is where the AI should be implemented, I think. Evasive behavior is 
	 * determined by setting targetManeuver
	 */
	IEnumerator Move () {
		yield return new WaitForSeconds (stopDelay);
		targetManeuver = speed;
		yield return new WaitForSeconds (maneuverTime/2);
		bool left = true;
		while (true) {
			targetManeuver = 0;
			yield return new WaitForSeconds (maneuverWait);

			if (left) {
				targetManeuver = -speed;
				yield return new WaitForSeconds (maneuverTime);
				left = false;
			} else {
				targetManeuver = speed;
				yield return new WaitForSeconds (maneuverTime);
				left = true;
			}
		}
	}

	/**
	 * Aims the boss at the player
	 */
	IEnumerator Aim () {
		yield return new WaitForSeconds (stopDelay);
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		while (true) {
			targetRotation = Vector3.Angle(player.transform.position - transform.position, 
			                               transform.forward);
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

		float newRotation = Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation, 
		                                            rotationSpeed * Time.deltaTime);
		//Mathf.LerpAngle(rigidbody.rotation.y, targetRotation, Time.deltaTime)
		rigidbody.rotation = Quaternion.Euler (0, rigidbody.rotation.y, 
		                                       rigidbody.velocity.x * -tilt);
	}
	
}
