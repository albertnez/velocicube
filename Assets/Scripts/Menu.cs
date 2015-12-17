using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public UnityEngine.UI.Button playButton;
    public UnityEngine.UI.Text bestScore;
    public UnityEngine.UI.Text lastScore;

	// Use this for initialization
    void Start () {
        // Set the speed of the tunnel.
        Game.SetMenu();
        // By default, select the Start button.
        playButton.Select();
        int best = Game.getBestScore();
        int last = Game.getLastScore();
        bestScore.text = (best == last && best > 0 ? "New " : "") + "Best Score: " + best.ToString();
        lastScore.text = "Last Score: " + last.ToString();
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
