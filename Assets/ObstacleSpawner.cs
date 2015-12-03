using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject obstacle;

	public static int numInitialObstacles = 10;
	public static float respawnTime = 2.0f;
	public static float spawnDistance = 80.0f;

	// Minimum and maximum coordinates of the space.
	public static float minX = -5.0f;
	public static float maxX = 5.0f;
	public static float midX = (minX + maxX) / 2.0f;
	public static float width = maxX - minX;
	public static float minY = 0.0f;
	public static float maxY = 5.0f;
	public static float midY = (minY + maxY) / 2.0f;
	public static float height = maxY - minY;

	// Obstacle sizes
	public static float scaleWidth = 1.0f;
	private static Vector3 spawnPoint;

	private float timeToRespawn;

	// Use this for initialization
	void Start () {
		timeToRespawn = respawnTime;
		spawnPoint = new Vector3(0.0f, 0.0f, spawnDistance);
		obstacle = (GameObject)Resources.Load ("Obstacle");
	}
	
	// Update is called once per frame
	void Update () {
		timeToRespawn -= Time.deltaTime;
		if (timeToRespawn < 0) {
			SpawnObstacle();
			//CreateObstacle (new Vector3(0.0f, 0.0f, spawnDistance));
			timeToRespawn = respawnTime;
		}
	}

	// Decide next kind of obstacle to spawn.
	private void SpawnObstacle() {
		SpawnPortal ();
	}

	// Creates a column in the given position of X and Y.
	private void CreateObstacle(Vector3 pos, Vector3 scale) {
		GameObject instance = (GameObject) Instantiate(obstacle, spawnPoint + pos, transform.rotation);
		instance.transform.localScale = scale;
	}


	// Level 1 Obstacles:

	static private float portalDistance = 15.0f;

	// Spawns a portal (U shaped obstacle attached to walls) followed by missing bar.
	private void SpawnPortal() {
		Vector3[] scales = new [] {
			new Vector3(width, scaleWidth, scaleWidth),
			new Vector3(width, scaleWidth, scaleWidth),
			new Vector3(scaleWidth, height, scaleWidth),
			new Vector3(scaleWidth, height, scaleWidth)
		};
		Vector3[] positions = new [] {
			new Vector3(midX, maxY),  // Top
			new Vector3(midX, minY),  // Bottom
			new Vector3(minX, midY),  // Left
			new Vector3(maxX, midY)   // Right
		};

		// One of them gets Z further
		int ind = Mathf.Max (0, Mathf.CeilToInt(Random.value*positions.Length)-1);
		positions[ind].z += portalDistance;

		// Instantiate all portals.
		for (int i = 0; i < positions.Length; ++i) {
			CreateObstacle (positions[i], scales[i]);
		}
	}
	
}
