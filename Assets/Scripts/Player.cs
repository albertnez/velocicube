﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public enum Floor
{
    Bottom = 0,
    Right,
    Top,
    Left,
};


public class Player : MonoBehaviour
{
    public GameObject coinExplosion;
    public Slider scoreSlider;
    Floor FloorWall;
    public float speed;
    public MoveCamera cameraScript;
    private bool changingState;
    private bool flipping180;
    // Use this for initialization
    void Start()
    {
        changingState = false;
        flipping180 = false;
        FloorWall = Floor.Bottom;

    }

    public Vector3 getPosition()
    {
        return transform.position;
    }

    public float getPlanePosition()
    {
        if (FloorWall == Floor.Bottom || FloorWall == Floor.Top)
        {
            return transform.position.x;
        }
        else
        {
            return transform.position.y;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        if (pos.x < -4.5f)
        {
            gameObject.transform.Translate(-4.5f - pos.x, 0.0f, 0.0f, Space.World);
        }
        if (pos.x > 4.5f)
        {
            gameObject.transform.Translate(4.5f - pos.x, 0.0f, 0.0f, Space.World);
        }
        if (pos.y < 0.5f)
        {
            gameObject.transform.Translate(0.0f, 0.5f - pos.y, 0.0f, Space.World);
        }
        if (pos.y > 4.5f)
        {
            gameObject.transform.Translate(0.0f, 4.5f - pos.y, 0.0f, Space.World);
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);

        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);

        }
        if (Input.GetAxisRaw("Vertical") == 1 && !flipping180)
        {
            flipping180 = true;
            Vector3 target;
            switch (FloorWall)
            {
                case Floor.Bottom:
                    FloorWall = Floor.Top;
                    target = gameObject.transform.position;
                    target.y = 4.5f;
                    StartCoroutine(SmoothFlipCoroutine(target, 4.0f, 0.3f));
                    break;
                case Floor.Left:
                    FloorWall = Floor.Right;
                    target = gameObject.transform.position;
                    target.x = 4.5f;
                    StartCoroutine(SmoothFlipCoroutine(target, 9.0f, 0.3f));
                    break;
                case Floor.Right:
                    FloorWall = Floor.Left;
                    target = gameObject.transform.position;
                    target.x = -4.5f;
                    StartCoroutine(SmoothFlipCoroutine(target, 9.0f, 0.3f));
                    break;
                case Floor.Top:
                    FloorWall = Floor.Bottom;
                    target = gameObject.transform.position;
                    target.y = 0.5f;
                    StartCoroutine(SmoothFlipCoroutine(target, 4.0f, 0.3f));
                    break;
                default:
                    break;
            }

        }

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
            transform.position = Vector3.MoveTowards(
                transform.position, target, rate
                );
            newPoint = gameObject.transform.position;
            gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        angleRate
                        );
            cameraScript.setFloor(FloorWall);
            yield return null;
        }
        gameObject.transform.Translate(0.0f, distance - currentDistance, 0.0f);
        newPoint = gameObject.transform.position;
        gameObject.transform.RotateAround(
                        newPoint,
                        new Vector3(0.0f, 0.0f, 1.0f),
                        180.0f - angle
                        );
        flipping180 = false;
        yield return null;
    }

    // When colliding with something
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Game.GameOver();
        }
        else if (other.CompareTag("Coin"))
        {
            Game.CollectCoin();
            scoreSlider.value += 1;
            if (scoreSlider.value >= scoreSlider.maxValue) {
                Game.FullCoins();
                scoreSlider.value = 0;
            }
            Destroy(other.gameObject);
            Instantiate(coinExplosion, transform.position, transform.rotation);
        }
        else if (!changingState)
        {
            if (other.CompareTag("RightWall") && FloorWall != Floor.Right)
            {
                if (FloorWall == Floor.Bottom)
                {
                    gameObject.transform.Rotate(0.0f, 0.0f, 90.0f);
                    //StartCoroutine(SmoothRotate(90.0f, 1.0f));
                }
                else
                {
                    gameObject.transform.Rotate(0.0f, 0.0f, -90.0f);
                }
                FloorWall = Floor.Right;
                cameraScript.SmoothRotate(FloorWall);
            }
            else if (other.CompareTag("LeftWall") && FloorWall != Floor.Left)
            {
                if (FloorWall == Floor.Top)
                {
                    gameObject.transform.Rotate(0.0f, 0.0f, 90.0f);
                }
                else
                {
                    gameObject.transform.Rotate(0.0f, 0.0f, -90.0f);
                }
                FloorWall = Floor.Left;
                cameraScript.SmoothRotate(FloorWall);

            }
            else if (other.CompareTag("TopWall") && FloorWall != Floor.Top)
            {
                if (FloorWall == Floor.Right)
                {
                    gameObject.transform.Rotate(0.0f, 0.0f, 90.0f);
                }
                else
                {
                    gameObject.transform.Rotate(0.0f, 0.0f, -90.0f);
                }
                FloorWall = Floor.Top;
                cameraScript.SmoothRotate(FloorWall);
            }
            else if (other.CompareTag("BottomWall") && FloorWall != Floor.Bottom)
            {
                if (FloorWall == Floor.Left)
                {
                    gameObject.transform.Rotate(0.0f, 0.0f, 90.0f);
                }
                else
                {
                    gameObject.transform.Rotate(0.0f, 0.0f, -90.0f);
                }
                FloorWall = Floor.Bottom;
                cameraScript.SmoothRotate(FloorWall);
            }
        }

    }
}
