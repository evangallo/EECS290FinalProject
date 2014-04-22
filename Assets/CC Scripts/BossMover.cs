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
	public float dodge;
	public float smoothing;
	public float stopDelay;
	public float maneuverTime;
	public float maneuverWait;

	private float currentSpeed;
	private float targetManeuver;

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
		targetManeuver = dodge;
		yield return new WaitForSeconds (maneuverTime/2);
		bool left = true;
		while (true) {
			targetManeuver = 0;
			yield return new WaitForSeconds (maneuverWait);

			if (left) {
				targetManeuver = -dodge;
				yield return new WaitForSeconds (maneuverTime);
				left = false;
			} else {
				targetManeuver = dodge;
				yield return new WaitForSeconds (maneuverTime);
				left = true;
			}
		}
	}

	/*IEnumerator Aim () {
		
	}*/
	
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
	
}
