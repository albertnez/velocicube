using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject obstacle;
	public GameObject[] typeObstacles;

	public static float respawnTime = 1.0f;
	public static float spawnDistance = 160.0f;

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

    public enum ObstacleId {
        HorizontalColumn = 0,
        CentralBlock,
        SideBlock,
        ThreeColumns,
        DodgeColumns,
        MovingColumn,
        Pi,
        Portal,
        RotatingColumn,
        ZShape,
        Table,
        Jump,
        TopDownObstacle,
        Loop,
        ReverseLoop,
    };

	// Use this for initialization
	void Start () {
		// Load all type of objects
		typeObstacles = new GameObject[] {
			Resources.Load("Obstacles/HorizontalColumn") as GameObject,
			Resources.Load("Obstacles/CentralBlock") as GameObject,
			Resources.Load("Obstacles/SideBlock") as GameObject,
			Resources.Load("Obstacles/ThreeColumns") as GameObject,
			Resources.Load("Obstacles/DodgeColumns") as GameObject,
			Resources.Load("Obstacles/MovingColumn") as GameObject,
			Resources.Load("Obstacles/Pi") as GameObject,
			Resources.Load("Obstacles/Portal") as GameObject,
			Resources.Load("Obstacles/RotatingColumn") as GameObject,
			Resources.Load("Obstacles/ZShape") as GameObject,
			Resources.Load("Obstacles/Table") as GameObject,
			Resources.Load("Obstacles/Jump") as GameObject,
			Resources.Load("Obstacles/TopDownObstacle") as GameObject,
			Resources.Load("Obstacles/Loop") as GameObject,
			Resources.Load("Obstacles/ReverseLoop") as GameObject,
		};
		timeToRespawn = respawnTime;
		spawnPoint = new Vector3(0.0f, 0.0f, spawnDistance);
	}
	
	// Update is called once per frame
	void Update () {
		timeToRespawn -= Time.deltaTime;
		if (timeToRespawn < 0) {
            int ind = Random.Range(0, typeObstacles.Length);
			float depth = SpawnObstacle(ind);
			timeToRespawn = respawnTime + depth / Game.obstacleSpeed;
		}
	}


    // Spawns an object and returns its depth.
	private float SpawnObstacle(int ind) {
		GameObject which = typeObstacles[ind];
		GameObject instance = (GameObject)Instantiate(
				which, spawnPoint + transform.position, transform.rotation);
        float depth = instance.GetComponent<ObstacleMove>().depth;
		// With probability 0.5, flip.
		if (Random.value < 0.5f && 
            ind != (int)ObstacleId.Loop && ind != (int)ObstacleId.ReverseLoop) {
			instance.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
			instance.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180.0f);
			// When rotating along Y axis, we must change direction.
            ObstacleMove om = instance.GetComponent<ObstacleMove>();
            om.direction *= -1;
            instance.transform.Translate(0.0f, 0.0f, om.direction * om.depth);
		}
        return depth;
	}
}
