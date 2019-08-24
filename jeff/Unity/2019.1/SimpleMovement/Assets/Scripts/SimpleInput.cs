using UnityEngine;
using System.Collections;

public class SimpleInput : MonoBehaviour {


    //Not this scipt moves whatever it's attached to

    protected Vector3 inputDirection;
    private Vector3 moveTranslation;
    public float Speed = 10;
    public float Angle;
    public Vector3 Direction;
    public Vector3 keyDirection;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()  
    {
        //input
        this.UpdateInputFromKeyboard();
        this.Direction = this.inputDirection;
        //Move Pacman
        this.UpdateMovement();
    }

    protected virtual void UpdateMovement()
    {
        //Adjust moveTranslate by deltatime
        this.moveTranslation = new Vector3(this.Direction.x, this.Direction.y) * Time.deltaTime * this.Speed;
        //change position
        this.transform.position += new Vector3(this.moveTranslation.x, this.moveTranslation.y);
    }

    private void UpdateInputFromKeyboard()
    {
        keyDirection.x = keyDirection.y = 0;  //reset key direction on every frame this will stop movement if no ket is pressed

        //Keyboard Input
        //Note this input uses keys not axis you shouldn't mix keys and axis unless you are carefull
        if (Input.GetKey("right"))
        {
            keyDirection.x += 1;
        }
        if (Input.GetKey("left"))
        {
            keyDirection.x += -1;
        }

        if (Input.GetKey("up"))
        {
            keyDirection.y += 1;
        }
        if (Input.GetKey("down"))
        {
            keyDirection.y += -1;
        }

        inputDirection = keyDirection;
        inputDirection.Normalize();
    }
}
