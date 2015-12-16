using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public UnityEngine.UI.Button playButton;

	// Use this for initialization
	void Start () {
        // Set the speed of the tunnel.
        Game.SetMenu();
        // By default, select the Start button.
        playButton.Select();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Menu Functions
    public void Play() {
        Game.SetGame();
        Application.LoadLevel("MainScene");
    }

    public void Credits() {
    }

    public void Exit() {
        Application.Quit();
    }
}
