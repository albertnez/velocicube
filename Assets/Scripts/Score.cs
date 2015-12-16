using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text text = GetComponent<Text>();
		text.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        Game.Step();
        GetComponent<Text>().text = Game.getCurrentScore().ToString();
	}
}
