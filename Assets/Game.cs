using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	static public float obstacleSpeed = 60.0f;
	static public float wallDepthScale = 20.0f;
    static public float tiling = 20.0f;
	static public float gridSpeed = obstacleSpeed / (10.0f * wallDepthScale) * tiling;
    static private int currentScore = 0;
    static private int bestScore = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    // Called each time we start from first level.
    static public void Restart() {
        currentScore = 0;
    }

    static public void CollectCoin() {
        currentScore += 10;
    }
}
