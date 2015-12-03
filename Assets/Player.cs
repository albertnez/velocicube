using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	// When colliding with something
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Obstacle")) {
			// TODO game over.
			Destroy (other.gameObject);
		}
    }
}
