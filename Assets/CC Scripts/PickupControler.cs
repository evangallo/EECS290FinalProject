using UnityEngine;
using System.Collections;

public class PickupControler : MonoBehaviour {

	public string pickupType;
	public float velocity;

	private Done_PlayerController playerController;
	
	void Start ()
	{
		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		
		if (playerControllerObject != null)
		{
			playerController = playerControllerObject.GetComponent <Done_PlayerController>();
		}
		
		if (playerController == null)
		{
			Debug.Log ("Cannot find 'PlayerController' script");
		}
	}

	void Update() {
		transform.Translate (0f, 0f, velocity * Time.deltaTime);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary")
		{
			Destroy(gameObject);
		}

		Debug.Log ("Collision detected");
		
		if (other.tag == "Player" || other.tag == "Shield")
		{
			switch (pickupType)
			{
			case "Rate Boost":
				playerController.rateUpgrade();
				break;
				
			case "Range Boost":
				playerController.rangeUpgrade();
				break;
				
			case "Shield":
				playerController.generateShield();
				break;
			}

			Destroy(gameObject);
		}
	}
}
