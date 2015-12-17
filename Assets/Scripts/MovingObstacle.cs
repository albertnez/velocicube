using UnityEngine;
using System.Collections;

public class MovingObstacle : MonoBehaviour {

	static public float minX = -5.0f;
	static public float maxX = 5.0f;
	public float speed = 10.0f;
	static public float maxSpeed = 17.0f;
	static public float minSpeed = 2.0f;
	private int speedSign = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float delta = Mathf.Min(Mathf.Abs(transform.position.x - minX),
		                        Mathf.Abs(transform.position.x - maxX));
		delta = Mathf.Clamp (delta * speed, minSpeed, maxSpeed);
		transform.Translate (new Vector3(speedSign * delta * Time.deltaTime, 0.0f, 0.0f));
		if (transform.position.x >= maxX || transform.position.x <= minX) {
			speedSign *= -1;
			transform.Translate(new Vector3(speedSign * delta * Time.deltaTime, 0.0f, 0.0f));
		}
	}
}
