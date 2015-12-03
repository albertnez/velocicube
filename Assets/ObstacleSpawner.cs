using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject obstacle;

	public int numInitialObstacles = 10;
	public float respawnTime = 2.0f;
	public float spawnDistance = 80.0f;

	private float timeToRespawn;

	// Use this for initialization
	void Start () {
		timeToRespawn = respawnTime;
		obstacle = (GameObject)Resources.Load ("Obstacle");
		//obstacle = Instantiate (Resources.Load ("Obstacle")) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		timeToRespawn -= Time.deltaTime;
		if (timeToRespawn < 0) {
			CreateObstacle (new Vector3(0.0f, 0.0f, spawnDistance));
			timeToRespawn = respawnTime;
		}
	}

	private void CreateObstacle (Vector3 pos = default(Vector3)) {
		GameObject instance;
		instance = (GameObject)Instantiate (obstacle, transform.position + pos, transform.rotation);
	}
}
