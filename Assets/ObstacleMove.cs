using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

	public float speed = 15.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(0.0f, 0.0f, - speed * Time.deltaTime);
	}
}
