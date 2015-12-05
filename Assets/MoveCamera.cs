using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    public float speed;
    public float rotationSpeed;
    public Player player;
    Floor FloorWall;
    private Vector3 offset;
    //move towards angle


    // Use this for initialization
    void Start () {
        FloorWall = Floor.Bottom;
        offset = new Vector3(-5.0f, 3.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
            

        }*/
        float newx = player.getPlanePosition();
        float oldx;
        if(FloorWall == Floor.Bottom || FloorWall == Floor.Top)
        {
            oldx = gameObject.transform.localPosition.x;
        }
        else
        {
            oldx = gameObject.transform.localPosition.y;
        }
        float change = newx - oldx;
        if(FloorWall == Floor.Top || FloorWall == Floor.Left)
        {
            gameObject.transform.Translate(-change, 0.0f, 0.0f);
        }
        else
        {
            gameObject.transform.Translate(change, 0.0f, 0.0f);
        }
        
        if(FloorWall == Floor.Top)
        {
            oldx = oldx;
        }

    }

    public void Flip(Floor NewFloor)
    {
        gameObject.transform.Rotate(-20.0f, 0.0f, 0.0f);
        gameObject.transform.Rotate(0.0f, 0.0f, 180.0f);
        gameObject.transform.Rotate(20.0f, 0.0f, 0.0f);
        FloorWall = NewFloor;
    }

    public void Rotate(Floor NewFloor)
    {
        if(FloorWall != NewFloor)
        {
            if (
                (NewFloor == Floor.Bottom && FloorWall == Floor.Left) ||
                (NewFloor > FloorWall && (
                    NewFloor != Floor.Left || FloorWall != Floor.Bottom)
                    )
                )
            {
                gameObject.transform.Translate(-2.5f, -2.5f, 0.0f);
                gameObject.transform.Rotate(-20.0f, 0.0f, 0.0f);
                gameObject.transform.Rotate(0.0f, 0.0f, 90.0f);
                gameObject.transform.Rotate(20.0f, 0.0f, 0.0f);


            }
            else
            {
                gameObject.transform.Translate(2.5f, -2.5f, 0.0f);
                gameObject.transform.Rotate(-20.0f, 0.0f, 0.0f);
                gameObject.transform.Rotate(0.0f, 0.0f, -90.0f);
                gameObject.transform.Rotate(20.0f, 0.0f, 0.0f);
            }
            if (gameObject.transform.position.z < -20.0f)
            {
                gameObject.transform.Translate(
                    0.0f, 0.0f, -20.0f-gameObject.transform.position.z
                    );
            }


            FloorWall = NewFloor;
        }
        
        
        
    }
}
