using UnityEngine;
using System.Collections;

public class ObstacleRotate : MonoBehaviour {

	public float rotateSpeed = 150.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0.0f, 0.0f, 1.0f), rotateSpeed * Time.deltaTime);
	}
}
