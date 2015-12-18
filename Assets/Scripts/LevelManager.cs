using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static int maxLevel = 6;
    private static Color[] obstacleColors = {
        new Color(0.321f, 0.294f, 0.725f, 0.75f),  // Level 0. Not used.
        new Color(0.321f, 0.294f, 0.725f, 0.75f),  // Dark blue.
        new Color(1.0f, 0.1f, 0.875f, 0.75f),  // Pink.
        new Color(0.396f, 0.780f, 0.886f, 0.85f),  // Light Blue.
        new Color(0.15f, 0.015f, 0.62f, 0.75f),  // Dark blue.
        new Color(0.26f, 0.45f, 0.21f, 0.75f),  // Dark green.
        new Color(1.0f, 0.862f, 0.101f, 0.75f),  // Yellow.
    };

    public GameObject wallTop;
    public GameObject wallBottom;
    public GameObject wallLeft;
    public GameObject wallRight;
    public GameObject player;
    public Material obstacleMaterial;
    private Material[] vertMaterials;
    private Material[] horMaterials;
    private Material[] playerMaterials;
    private int currentLevel = 1;

	// Use this for initialization
	void Start () {
        vertMaterials = new Material[maxLevel+1];
        horMaterials = new Material[maxLevel+1];
        playerMaterials = new Material[maxLevel+1];
        for (int i = 1; i <= maxLevel; ++i) {
            vertMaterials[i] = Resources.Load("Materials/WallVert" + i.ToString()) as Material;
            horMaterials[i] = Resources.Load("Materials/WallHor" + i.ToString()) as Material;
            playerMaterials[i] = Resources.Load("Materials/Player" + i.ToString()) as Material;
        }
        SetMaterials();
	}

    private void SetMaterials() {
        wallTop.GetComponent<Renderer>().material = horMaterials[currentLevel];
        wallBottom.GetComponent<Renderer>().material = horMaterials[currentLevel];
        wallLeft.GetComponent<Renderer>().material = vertMaterials[currentLevel];
        wallRight.GetComponent<Renderer>().material = vertMaterials[currentLevel];
        player.GetComponent<Renderer>().material = playerMaterials[currentLevel];
        obstacleMaterial.color = obstacleColors[currentLevel];
    }
	
    public bool HasNextLevel() {
        return currentLevel < maxLevel;
    }

    public void NextLevel() {
        if (currentLevel >= maxLevel) {
            return;
        }
        ++currentLevel;
        SetMaterials();
    }

	// Update is called once per frame
	void Update () {
	}
}
