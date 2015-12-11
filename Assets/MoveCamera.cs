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
    public void setFloor(Floor NewFloor)
    {
        FloorWall = NewFloor;
    }

    IEnumerator SmoothRotateCoroutine(float difference, float duration)
    {
        float t = 0;
        float angle = 0;
        Vector3 newPoint;
        while (Math.Abs(angle) < Math.Abs(difference))
        {
            angle += difference * Time.deltaTime / 0.1f;
            newPoint = player.getPosition();
            gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        difference * Time.deltaTime / 0.1f
                        );
            yield return null;
        }
        gameObject.transform.localPosition = new Vector3(0.0f, 2.5f, -5.0f);
        gameObject.transform.localEulerAngles = new Vector3(20.0f, 0.0f, 0.0f);
        /*newPoint = player.getPosition();
        gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        difference - angle
                        );*/
        yield return null;
    }


    public void SmoothFlip(
        Vector3 target, float distance, float rate)
    {
        StartCoroutine(SmoothFlipCoroutine(target, distance, rate));
        
    }

    IEnumerator SmoothFlipCoroutine(Vector3 target, float distance, float rate)
    {
        float angle = 0;
        Vector3 newPoint;
        
        float currentDistance = 0;
        float angleRate = 180.0f * rate / distance;
        while (Math.Abs(currentDistance) < Math.Abs(distance))
        {

            currentDistance += rate;
            angle += angleRate;
            //gameObject.transform.Translate(0.0f, rate, 0.0f);
            /*transform.position = Vector3.MoveTowards(
                transform.position, target, rate
                );*/
            //newPoint = gameObject.transform.position;
            newPoint = player.getPosition();
            newPoint.z = -20.0f;
            gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        angleRate
                        );
            yield return null;

            /*SmoothDamp(float current, 
                float target, rate, 0.2f);*/
}
        //gameObject.transform.Translate(0.0f, distance - currentDistance, 0.0f);
        newPoint = gameObject.transform.position;
        gameObject.transform.localPosition = new Vector3(0.0f, 2.5f, -5.0f);
        gameObject.transform.localEulerAngles = new Vector3(20.0f, 0.0f, 0.0f);
        /*gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        180.0f - angle
                        );*/
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


    public void unchild()
    {
        gameObject.transform.parent = null;
    }
    public void child(Transform t)
    {
        gameObject.transform.parent = t;
    }
}
