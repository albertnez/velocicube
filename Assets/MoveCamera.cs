using UnityEngine;
using System.Collections;
using System;

public class MoveCamera : MonoBehaviour {
    public Player player;
    Floor FloorWall;
    public float maxTilt = 30.0f;
    private float minTilt;
    public float tiltRate = 5.0f;

    private float flipOffset = 0.0f;
    public float v = 5.0f;
    // Use this for initialization
    void Start () {
        FloorWall = Floor.Bottom;
        minTilt = 360.0f - maxTilt;
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
        StartCoroutine(SmoothFlipTranslateCoroutine(target, distance, rate));
    }

    IEnumerator SmoothFlipTranslateCoroutine(Vector3 target, float distance, float rate)
    {
        v = 5.0f;
        flipOffset = 0.0f;
        while (flipOffset >= 0)
        {
            float vf = v - 10.0f * Time.deltaTime;
            float delta = (v + vf) * Time.deltaTime / 2;

            v = vf;
            switch (FloorWall)
            {
                case Floor.Bottom:
                    gameObject.transform.Translate(0.0f, -delta, 0.0f, Space.World);
                    break;
                case Floor.Left:
                    gameObject.transform.Translate(-delta, 0.0f, 0.0f, Space.World);
                    break;
                case Floor.Top:
                    gameObject.transform.Translate(0.0f, delta, 0.0f, Space.World);
                    break;
                case Floor.Right:
                    gameObject.transform.Translate(delta, 0.0f, 0.0f, Space.World);
                    break;
                default:
                    break;
            }
            flipOffset = flipOffset + delta;
            yield return null;
        }
        yield return null;
        
    }


    IEnumerator SmoothFlipCoroutine(Vector3 target, float distance, float rate)
    {
        float angle = 0;
        Vector3 newPoint;
        Vector3 trueTarget = target;
        trueTarget.z = -20.0f;
        float currentDistance = 0;
        float angleRate = 180.0f * rate;
        float translateRate = rate / 50.0f;
        while (angle < 180.0f)
        {

            currentDistance += rate;
            angle += angleRate;
            newPoint = player.getPosition();
            newPoint.z = -20.0f;
            switch (FloorWall)
            {
                case Floor.Bottom:
                    newPoint.y -= flipOffset;
                    break;
                case Floor.Left:
                    newPoint.x -= flipOffset;
                    break;
                case Floor.Top:
                    newPoint.y += flipOffset;
                    break;
                case Floor.Right:
                    newPoint.x += flipOffset;
                    break;
                default:
                    break;
            }

            //Quaternion newRot = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));// get the equivalent quaternion
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRot, speed * Time.deltaTime);
            gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        angleRate
                        );
            //angleRate = angleRate / 10.0f;
            angleRate = (float)Math.Sqrt(angleRate*5.0f);
            rate = rate - 10.0f * Time.deltaTime;
            yield return null;

            /*SmoothDamp(float current, 
                float target, rate, 0.2f);*/
        }
        newPoint = gameObject.transform.position;
        gameObject.transform.localEulerAngles = new Vector3(20.0f, 0.0f, 0.0f);
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


    public void tilt(float d, Vector3 center)
    {
        float tilt = gameObject.transform.localEulerAngles.z;
        if (tilt < maxTilt)
            gameObject.transform.RotateAround(
                    center,
                    new Vector3(0.0f, 0.0f, 1.0f),
                    Math.Min(tiltRate * d, maxTilt - tilt)
                );
        if(tilt > minTilt)
        {
            gameObject.transform.RotateAround(
                    center,
                    new Vector3(0.0f, 0.0f, 1.0f),
                    Math.Min(tiltRate * d, maxTilt - tilt)
                );
        }
    }
    public void untilt()
    {

    }
}
