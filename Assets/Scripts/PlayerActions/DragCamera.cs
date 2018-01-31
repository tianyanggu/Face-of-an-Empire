using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour
{
	public float dragSpeed = 10;
	private Vector3 dragOrigin;

    public float zoomSpeed = 5;
    public float keyAdjustmentSpeed = 2;

    //TODO set left,right,etc to change by map size
    public static float outerLeft = 0f;
	public static float outerRight = 200f;
	public static float outerTop = 150f;
	public static float outerDown = -50f;

    void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			dragOrigin = Input.mousePosition;
			return;
		}

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            MouseDragCameraMovement(pos);
        }

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        if (xAxis != 0f || zAxis != 0f)
        {
            AdjustPosition(xAxis, zAxis);
        }

        //zoom in and out
        float yAxis = Input.GetAxis("Mouse ScrollWheel");
        if (yAxis != 0f)
        {
            AdjustZoom(yAxis); //MINPT smooth out zoom
        }

        //if (Input.GetKey("q"))
        //    this.transform.localEulerAngles = new Vector3(45, transform.rotation.y + 90, 0);
        //if (Input.GetKey("e"))
        //    this.transform.localEulerAngles = new Vector3(45, transform.rotation.y - 90, 0);
        //if (Input.GetKeyDown("space"))
        //    this.transform.localEulerAngles = new Vector3(45, 0, 0);
    }

    void AdjustZoom(float yAxis)
    {
        MoveCamera(new Vector3(0, yAxis * zoomSpeed, 0)); //
    }

    void AdjustPosition(float xAxis, float zAxis)
    {
        MoveCamera(new Vector3(xAxis * keyAdjustmentSpeed, 0, zAxis * keyAdjustmentSpeed));
    }

    void MouseDragCameraMovement(Vector3 pos)
    {
        //made pos.y since screen is x and y. i.e. no z. Thus needed to make y movement of mouse into z axis movement in game.
        MoveCamera(new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed));
    }

    void MoveCamera(Vector3 move)
    {
        transform.Translate(move, Space.World);

        //limits the boundaries
        if (this.transform.position.x < outerLeft)
        {
            this.transform.position = new Vector3(outerLeft, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x > outerRight)
        {
            this.transform.position = new Vector3(outerRight, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.z > outerTop)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, outerTop);
        }
        if (this.transform.position.z < outerDown)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, outerDown);
        }
    }
}
