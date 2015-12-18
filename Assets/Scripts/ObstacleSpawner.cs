using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {
    // Class representing a Range [from, to].
    public class Range {
        public Range() {
        }

        public Range(int from, int to) {
            this.from = from;
            this.to = to;
        }

        public int from { get; set; }
        public int to { get; set; }
    };

    public LevelManager levelManager;
	public GameObject obstacle;
	public GameObject[] typeObstacles;

    public enum ObstacleId {
        HorizontalColumn = 0,
        CentralBlock,
        SideBlock,
        MovingColumn,
        ZShape,
        Portal,
        ThreeColumns,
        DodgeColumns,
        Pi,
        TopDownObstacle,
        RotatingColumn,
        Table,
        Jump,
        Loop,
        ReverseLoop,
    };

    private float[] respawnTime = {
        1.0f,  // Level 0.
        3.0f,
        1.5f,
        1.0f,
        0.8f,
        0.7f,
        0.5f,
        0.5f,
    };

    private static Range[] obstaclesInLevel = {
        // First is level 0.
        new Range((int)ObstacleId.HorizontalColumn, (int)ObstacleId.HorizontalColumn),
        new Range((int)ObstacleId.HorizontalColumn, (int)ObstacleId.SideBlock),
        new Range((int)ObstacleId.Portal, (int)ObstacleId.Pi),
        new Range((int)ObstacleId.Loop, (int)ObstacleId.ReverseLoop),
        new Range((int)ObstacleId.HorizontalColumn, (int)ObstacleId.Table),
        new Range((int)ObstacleId.HorizontalColumn, (int)ObstacleId.ReverseLoop),
        new Range((int)ObstacleId.HorizontalColumn, (int)ObstacleId.ReverseLoop),
    };

	private static float spawnDistance = 160.0f;

	// Obstacle sizes
	public static float scaleWidth = 1.0f;
	private static Vector3 spawnPoint;

	private float timeToRespawn;

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
		timeToRespawn = respawnTime[levelManager.GetCurrentLevel()];
		spawnPoint = new Vector3(0.0f, 0.0f, spawnDistance);
	}
	
	// Update is called once per frame
	void Update () {
		timeToRespawn -= Time.deltaTime;
		if (timeToRespawn < 0) {
            Range range = obstaclesInLevel[levelManager.GetCurrentLevel()];
            // Select from the available this level.
            int ind = Random.Range(range.from, range.to+1);
			float depth = SpawnObstacle(ind);
			timeToRespawn = respawnTime[levelManager.GetCurrentLevel()] + 
                            depth / Game.obstacleSpeed;
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
