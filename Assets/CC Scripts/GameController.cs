using UnityEngine;
using System.Collections;

/**
 * Game controller for Cosmos Commander Final Project.
 * Controls the game play and game modes of the game.
 * 
 * @authors EECS 290 Team 2
 */
public class GameController : MonoBehaviour
{
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText changeModeText;
	
	private bool gameOver;
	private bool restart;
	private int score;
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		changeModeText.text = "";
		score = 0;
		UpdateScore ();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}

			if (Input.GetKeyDown (KeyCode.M)) //loads mode select menu
			{
				Application.LoadLevel (1);
			}
		}
		if (gameOver)
		{
			restartText.text = "Press 'R' for Restart";
			restart = true;
			changeModeText.text = "Press 'M' for Mode Selection";
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}