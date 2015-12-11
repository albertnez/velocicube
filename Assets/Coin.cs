using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public float rotateSpeed = 100.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f), rotateSpeed * Time.deltaTime);
	}
}
