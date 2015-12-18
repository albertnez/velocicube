using UnityEngine;
using System.Collections;

public class Tunnel : MonoBehaviour {

    private Renderer[] walls;
	// Use this for initialization
	void Start () {
        walls = new Renderer[] {
            transform.Find("Top").GetComponent<Renderer>(),
            transform.Find("Bottom").GetComponent<Renderer>(),
            transform.Find("Left").GetComponent<Renderer>(),
            transform.Find("Right").GetComponent<Renderer>(),
        };
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < walls.Length; ++i) {
            walls[i].material.mainTextureOffset 
                += new Vector2(0.0f, - Game.gridSpeed * Time.deltaTime);
        }
	}
}
