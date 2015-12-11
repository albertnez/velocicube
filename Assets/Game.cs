using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	static public float obstacleSpeed = 60.0f;
	static public float wallDepthScale = 20.0f;
    static public float tiling = 20.0f;
	static private float gridGameSpeed = obstacleSpeed / (10.0f * wallDepthScale) * tiling;
    static private float gridMenuSpeed = 0.5f;
    static private int currentScore = 0;
    static private int bestScore = 0;
    static public float gridSpeed = gridGameSpeed;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    // Called when the player dies.
    static public void GameOver() {
        SetMenu();
        Application.LoadLevel("Menu");
    }

    // Sets the values for the menu.
    static public void SetMenu() {
        gridSpeed = gridMenuSpeed;
    }

    // Sets the values for the game.
    static public void SetGame() {
        gridSpeed = gridGameSpeed;
    }

    // Called each time we start from first level.
    static public void Restart() {
        currentScore = 0;
    }

    static public void CollectCoin() {
        currentScore += 10;
    }
}
