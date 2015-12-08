using UnityEngine;
using System.Collections;

public class Tunnel : MonoBehaviour {

    private Material[] walls;
	// Use this for initialization
	void Start () {
        walls = new Material[] {
            transform.Find("Top").GetComponent<Renderer>().material,
            transform.Find("Bottom").GetComponent<Renderer>().material,
            transform.Find("Left").GetComponent<Renderer>().material,
            transform.Find("Right").GetComponent<Renderer>().material,
        };
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < walls.Length; ++i) {
            walls[i].mainTextureOffset += new Vector2(0.0f, - Game.gridSpeed * Time.deltaTime);
        }
	}
}
