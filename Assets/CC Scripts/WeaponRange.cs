using UnityEngine;
using System.Collections;

public class WeaponRange : MonoBehaviour {

	public float range;

	private Vector3 startPosition;

	void Start () {
		startPosition = transform.position;
	}
	
	void Update () {
		float distanceTraveled = Vector3.Magnitude (startPosition - transform.position);
		if (distanceTraveled >= range) {
			Destroy (this.gameObject);
		}
	
	}
}
