using UnityEngine;
using System.Collections;

public enum Floor
{
    Bottom = 0,
    Right,
    Top,
    Left,
};


public class Player : MonoBehaviour
{
    Floor FloorWall;
    public float speed;
    public MoveCamera cameraScript;
    // Use this for initialization
    void Start()
    {
        FloorWall = Floor.Bottom;
    }

    public float getPlanePosition()
    {
        if(FloorWall == Floor.Bottom || FloorWall == Floor.Top)
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
        Vector3 pos = gameObject.transform.localPosition;
        if (pos.x < -4.5f)
        {
            gameObject.transform.Translate(-4.5f - pos.x, 0.0f, 0.0f);
        }
        if (pos.x > 4.5f)
        {
            gameObject.transform.Translate(4.5f - pos.x, 0.0f, 0.0f);
        }
        if (pos.y < 0.5f)
        {
            gameObject.transform.Translate(0.0f, 0.5f - pos.y, 0.0f);
        }
        if (pos.y > 4.5f)
        {
            gameObject.transform.Translate(0.0f, 4.5f - pos.y, 0.0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            switch (FloorWall)
            {
                case Floor.Bottom:
                    gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
                    break;
                case Floor.Left:
                    gameObject.transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);
                    break;
                case Floor.Right:
                    gameObject.transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
                    break;
                case Floor.Top:
                    gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
                    break;
                default:
                    break;
            }
            //cameraScript.playerPos = gameObject.transform.position;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            switch (FloorWall)
            {
                case Floor.Bottom:
                    gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
                    break;
                case Floor.Left:
                    gameObject.transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
                    break;
                case Floor.Right:
                    gameObject.transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);
                    break;
                case Floor.Top:
                    gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
                    break;
                default:
                    break;
            }
        }

    }

    // When colliding with something
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // TODO game over.
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("RightWall") && FloorWall != Floor.Right)
        {

            FloorWall = Floor.Right;
            Vector3 pos = gameObject.transform.position;
            Vector3 posWall = other.transform.position;
            //gameObject.transform.Translate(pos.x - posWall.x, 0.0f, 0.0f);
            cameraScript.Rotate(FloorWall, pos);
        }
        else if (other.CompareTag("LeftWall") && FloorWall != Floor.Left)
        {
            FloorWall = Floor.Left;
            Vector3 pos = gameObject.transform.position;
            Vector3 posWall = other.transform.position;
            //gameObject.transform.Translate(pos.x - posWall.x, 0.0f, 0.0f);
            cameraScript.Rotate(FloorWall, pos);

        }
        else if (other.CompareTag("TopWall") && FloorWall != Floor.Top)
        {
            FloorWall = Floor.Top;
            Vector3 pos = gameObject.transform.position;
            Vector3 posWall = other.transform.position;
            //gameObject.transform.Translate(0.0f, pos.y - posWall.y, 0.0f);
            cameraScript.Rotate(FloorWall, pos);
        }
        else if (other.CompareTag("BottomWall") && FloorWall != Floor.Bottom)
        {
            FloorWall = Floor.Bottom;
            Vector3 pos = gameObject.transform.position;
            Vector3 posWall = other.transform.position;
            //gameObject.transform.Translate(0.0f, pos.y - posWall.y, 0.0f);
            cameraScript.Rotate(FloorWall,pos);
        }
    }
}
