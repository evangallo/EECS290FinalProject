using UnityEngine;

/**
 * Class for runner of the runner game.
 * Adapted from online tutorials.
 * 
 * @author EECS 290 Team 2
 * @author Shaun Howard
 */
public class Runner : MonoBehaviour {

	//distance traveled by runner thus far
	public static float distanceTraveled;

	//the boost count
	public static int boosts;

	//acceleration of runner based on platform material
	public float acceleration;

	//factors for amount of movement produced via WASD keys
	public float horizMovementFactor, vertMovementFactor;

	//velocities for power ups and jumps
	public Vector3 boostVelocity, jumpVelocity, invincibleVelocity;

	//the distance at which the player can use vertical movement (W and S keys)
	public float levelUpDistance;

	//the position at which the game over text is displayed
	public float gameOverY;

	//the invincibility time count
	public static float invincibilityTime;

	//tells if player is touching platform or not
	private bool touchingPlatform;

	//starting position of runner
	private Vector3 startPosition;

	//tracks input axes for each direction
	private float horizMovement, vertMovement;

	//movement vectors for each of the directions
	private Vector3 horizMovementVector, vertMovementVector;

	//timer for invincibility
	private static float invTimer;

	//whether runner is invincible or not
	private static bool invincible;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition.Set (transform.localPosition.x,
		                   transform.localPosition.y, 
		                   transform.localPosition.z);
		renderer.enabled = false;
		enabled = false;

		//runner is not initially invincible
		invincible = false;
		invTimer = 0f;
		invincibilityTime = 10f;
	}
	
	void Update () {

		//when runner is invincible, set invTimer.
		if (invincible) {
			GUIManager.SetInvincibility (invTimer);
		} else { //or set timer to zero
			GUIManager.SetInvincibility(0f);
		}

		/*//when spacebar is pressed do the following...
		if(Input.GetButtonDown("Jump")){

			//add acceleration to runner on touching a platform
			if(touchingPlatform){
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}

			//when invincible, add invincibility boost to runner
			else if (invincible) {
				rigidbody.AddForce(invincibleVelocity, ForceMode.VelocityChange);
			}

			//add boost to runner when runner has boosts but is not invincible
			else if(boosts > 0 && !invincible){
				rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetRapidFire(boosts);
			}
		
		} */

		//runner's distance from start
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistance(distanceTraveled);

		//when below game over boundary do the following...
		if(transform.localPosition.y < gameOverY){

			//when invincible, respawn above platforms
			if (invincible){
		//keep going
			} else { //otherwise, game is over
				GameEventManager.TriggerGameOver();
			}
		}
	}

	void FixedUpdate () {

		//invTimer varies with change in time of gameplay
		if (invTimer >= 0f) {
			invTimer -= Time.deltaTime;
		} else { //runner is not invincible when time runs out
			invincible = false;
		}

		//A and D keys move runner backward and forward, horizontal movement
		horizMovement = Input.GetAxis ("Horizontal");

		//we factor in our horizontal movement factor and keyboard input
		horizMovementVector = new Vector3 (horizMovement * horizMovementFactor, 0f, 0f);

		//check if our player has leveled up to vertical movement use
		if (distanceTraveled > levelUpDistance) {

			//W and S keys move runner upward and downward, vertical movement
			vertMovement = Input.GetAxis ("Vertical");

			//we factor in our vertical movement factor and keyboard input
			vertMovementVector = new Vector3 (0f, vertMovement * vertMovementFactor, 0f);
		}
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}

	private void GameStart () {
		//boosts = 0;
		//GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		renderer.enabled = true;
		enabled = true;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		enabled = false;
	}
	 /*
	public static void AddBoost(){
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	} */

	/*
	 * Adds invincibility to runner, which is defined by respawning above platforms 
	 * after runner falls and unlimited boost from spacebar press until time is up.
	 */
	public static void AddInvincibility() {

		//set invTimer to our pre-defined invincibility time
		invTimer = invincibilityTime;

		//runner is invincible
		invincible = true;
	}

}