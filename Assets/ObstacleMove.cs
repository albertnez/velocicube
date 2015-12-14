using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

    static public float destroyOffset = -100.0f;

    public float depth = 0.0f;
    public int direction = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(0.0f, 0.0f, -direction * Game.obstacleSpeed * Time.deltaTime);
		// If obstacle if out of bounds, delete.
		if (gameObject.transform.position.z < destroyOffset - depth) {
			Destroy (gameObject);
		}
	}
}
