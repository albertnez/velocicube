using UnityEngine;
using System.Collections;
using System;

public class MoveCamera : MonoBehaviour {
    public float speed;
    public float rotationSpeed;
    public Player player;
    Floor FloorWall;


    // Use this for initialization
    void Start () {
        FloorWall = Floor.Bottom;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Flip(Floor NewFloor)
    {
        
    }

    IEnumerator SmoothRotateCoroutine(float difference, float duration)
    {
        yield return null;
        float t = 0;
        float angle = 0;
        Vector3 newPoint;
        while (Math.Abs(angle) < Math.Abs(difference))
        {
            angle += difference* Time.deltaTime / 0.1f;
            newPoint = player.getPosition();
            gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        difference * Time.deltaTime / 0.1f
                        );
            yield return null;
        }
        newPoint = player.getPosition();
        gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        difference-angle
                        );
        yield return null;
    }

    public void SmoothRotate(Floor NewFloor)
    {
        if (FloorWall != NewFloor)
        {
            if (
                (NewFloor == Floor.Bottom && FloorWall == Floor.Left) ||
                (NewFloor > FloorWall && (
                    NewFloor != Floor.Left || FloorWall != Floor.Bottom)
                    )
                )
            {

                Vector3 point = player.getPosition();
                gameObject.transform.RotateAround(
                    point, new Vector3(0.0f, 0.0f, 1.0f), -90.0f);
                StartCoroutine(
                    SmoothRotateCoroutine(90.0f, 1.0f));



            }
            else
            {
                Vector3 point = player.getPosition();
                gameObject.transform.RotateAround(
                    point, new Vector3(0.0f, 0.0f, 1.0f), 90.0f);
                StartCoroutine(
                    SmoothRotateCoroutine(-90.0f, 1.0f));
            }
            if (gameObject.transform.position.z < -20.0f)
            {
                gameObject.transform.Translate(
                    0.0f, 0.0f, -20.0f - gameObject.transform.position.z
                    );
            }


            FloorWall = NewFloor;
        }
    }

}
