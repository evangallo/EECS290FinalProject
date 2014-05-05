using UnityEngine;
using System.Collections;

/**
 * Boss Maneuver Controller for Cosmos Commander Final Project.
 * Controls the maneuvers of the boss in the game.
 * 
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
	private Vector3 target;

	void Start ()
	{
		currentSpeed = rigidbody.velocity.z;
		StartCoroutine(Move());
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


	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (stopDelay <= 0) {
			target = player.transform.position - transform.position;
			//Debug.Log(target);
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

		Vector3 newRotation = Vector3.Slerp (-transform.forward, target, rotationSpeed * Time.deltaTime);
		//Debug.Log(newRotation);
		transform.rotation = Quaternion.LookRotation (-newRotation);
		//rigidbody.rotation = Quaternion.Euler (0, rigidbody.rotation.y, rigidbody.velocity.x * -tilt);
	}
	
}
