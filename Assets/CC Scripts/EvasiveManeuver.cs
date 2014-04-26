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
	public float detectionRange;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

	private float currentSpeed;
	private float targetManeuver;
	private Transform threat;

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


		while (true)
		{
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
			targetManeuver = Random.Range (1, dodge) * Mathf.Sign (Random.Range(-1, 1));
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
		}
	}

	void Update () {
		threat = detectNearestThreat ();
	}
	
	void FixedUpdate ()
	{
		float newManeuver = 0f;
		if (threat == null) {
			//use maneuver from Evade coroutine
			newManeuver = Mathf.MoveTowards (rigidbody.velocity.x, targetManeuver, smoothing * Time.deltaTime);
		} else {
			//avoid incoming threat
			float evasiveManeuver = dodge * Mathf.Sign(transform.position.x - threat.transform.position.x);
			newManeuver = Mathf.MoveTowards (rigidbody.velocity.x, evasiveManeuver, smoothing * Time.deltaTime);
		}

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
	 * Detects nearest object with player or hazard tag and returns its transform
	 * This method is kinda inefficient.
	 */
	Transform detectNearestThreat () {
		GameObject[] playerObjects = GameObject.FindGameObjectsWithTag ("Player");
		GameObject[] hazards = GameObject.FindGameObjectsWithTag ("Hazard");
		float distance = Mathf.Infinity;
		GameObject nearestThreat = null;

		foreach (GameObject playerObject in playerObjects) {
			float difference = Vector3.Magnitude(transform.position - playerObject.transform.position);
			if (difference < distance) {
				distance = difference;
				nearestThreat = playerObject;
			}
		}

		foreach (GameObject hazard in hazards) {
			float difference = Vector3.Magnitude(transform.position - hazard.transform.position);
			if (difference < distance) {
				distance = difference;
				nearestThreat = hazard;
			}
		}

		//Debug.Log (nearestThreat.tag);

		if (distance < detectionRange) {
			return nearestThreat.transform;
		} else {
			return null;
		}
	}
}
