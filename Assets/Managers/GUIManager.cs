using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;

	//texts for various components of runner game
	public GUIText boostsText, invText, distanceText, gameOverText, instructionsText, runnerText;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}

	//sets boosts text, counter
	public static void SetBoosts(int boosts){
		instance.boostsText.text = "Boosts: " + boosts.ToString();
	}

	//sets distance text, counter
	public static void SetDistance(float distance){
		instance.distanceText.text = "Distance: " + distance.ToString("f0");
	}

	//sets invincibility text, timer
	public static void SetInvincibility(float time){
		instance.invText.text = "Invincibility Time: " + time.ToString ("f0");
	}
}