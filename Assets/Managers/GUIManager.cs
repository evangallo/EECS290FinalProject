using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;

	//texts for various components of runner game
	public GUIText rapidFireText, invText, scoreText, gameOverText, instructionsText, titleText;
	
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
		titleText.enabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}

	//sets rapid fire text, timer
	public static void SetRapidFire(float time){
		instance.rapidFireText.text = "Rapid Fire Time: " + time.ToString("f0");
	}

	//sets distance text, counter
	public static void SetDistance(float score){
		instance.scoreText.text = "Score: " + score.ToString("f0");
	}

	//sets invincibility text, timer
	public static void SetInvincibility(float time){
		instance.invText.text = "Invincibility Time: " + time.ToString ("f0");
	}
}