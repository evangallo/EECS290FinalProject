using UnityEngine;
using System.Collections;

public class PickupDropper : MonoBehaviour {

	public GameObject[] pickups;
	public float dropChance;

	public void drop () {
		if (Random.Range(0,1) < dropChance) {
			Instantiate(pickups[Mathf.FloorToInt(Random.Range(0,pickups.Length))],
			            transform.position, transform.rotation);
		}
	}
}
