using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    public float speed;
    public float rotationSpeed;
    public GameObject player;
    Floor FloorWall;
    private Vector3 offset;
    //move towards angle


    // Use this for initialization
    void Start () {
        FloorWall = Floor.Bottom;
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

        transform.position = transform.postion +
            

    }
    public void Rotate(Floor NewFloor, Vector3 pos)
    {
        if(FloorWall != NewFloor)
        {
            if (
                (NewFloor == Floor.Bottom && FloorWall == Floor.Left) ||
                (NewFloor > FloorWall && (NewFloor != Floor.Left || FloorWall != Floor.Bottom))
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
                gameObject.transform.Translate(0.0f, 0.0f, -20.0f-gameObject.transform.position.z);
            }


            FloorWall = NewFloor;
        }
        
        
        
    }
}
