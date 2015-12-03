using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	static public float obstacleSpeed = 50.0f;
	static public float gridSpeed; 
	static public Material gridMaterial;
	static public float wallDepthScale = 10.0f;

	// Use this for initialization
	void Start () {
		gridMaterial = Resources.Load("Grid", typeof(Material))as Material;
		float tiling = gridMaterial.mainTextureScale.y;
		Debug.Log("Tiling: " + tiling.ToString());
		gridSpeed = obstacleSpeed / (10.0f*wallDepthScale) * tiling;
	}
	
	// Update is called once per frame
	void Update () {
		gridMaterial.mainTextureOffset += new Vector2(0.0f, - gridSpeed * Time.deltaTime);
	}
}
