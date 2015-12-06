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
    private bool changingState;
    // Use this for initialization
    void Start()
    {
        changingState = false;
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
        Vector3 pos = gameObject.transform.position;
        if (pos.x < -4.5f)
        {
            gameObject.transform.Translate(-4.5f - pos.x, 0.0f, 0.0f,Space.World);
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (FloorWall)
            {
                case Floor.Bottom:
                    changingState = true;
                    FloorWall = Floor.Top;
                    gameObject.transform.Translate(0.0f, 4.0f, 0.0f);
                    
                    cameraScript.Flip(FloorWall);
                    changingState = false;
                    break;
                case Floor.Left:
                    changingState = true;
                    FloorWall = Floor.Right;
                    gameObject.transform.Translate(0.0f, 9.0f, 0.0f);
                    
                    cameraScript.Flip(FloorWall);
                    changingState = false;
                    break;
                case Floor.Right:
                    changingState = true;
                    FloorWall = Floor.Left;
                    gameObject.transform.Translate(0.0f, 9.0f, 0.0f);
                    
                    cameraScript.Flip(FloorWall);
                    changingState = false;
                    break;
                case Floor.Top:
                    changingState = true;
                    FloorWall = Floor.Bottom;
                    gameObject.transform.Translate(0.0f, 4.0f, 0.0f);
                    
                    cameraScript.Flip(FloorWall);
                    changingState = false;
                    break;
                default:
                    break;
            }
            gameObject.transform.Rotate(0.0f, 0.0f, 180.0f);

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
        else if(!changingState)
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
                cameraScript.Rotate(FloorWall);
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
                cameraScript.Rotate(FloorWall);

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
                cameraScript.Rotate(FloorWall);
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
                cameraScript.Rotate(FloorWall);
            }
        } 
        
    }
}
