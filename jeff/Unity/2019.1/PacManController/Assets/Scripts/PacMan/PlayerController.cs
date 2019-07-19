using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    protected Vector2 controllerDirection;
    public Vector2 ControllerDirection
    {
        get { return this.controllerDirection; }
    }
    protected Vector2 keyDirection;
	public bool IsKeyDown {
		get {
			if(keyDirection.sqrMagnitude == 0) return false;
			return true;}
	}

    public PlayerController()
    {
        controllerDirection = new Vector2();
        keyDirection = new Vector2();
    }
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateKeyboardDirection();
        UpdateGamepadDirection();
        controllerDirection += keyDirection;
        controllerDirection.Normalize();

    }

    protected virtual void UpdateGamepadDirection()
    {
        //TODO add gamepad
    }

    protected virtual void UpdateKeyboardDirection()
    {
        keyDirection.x = keyDirection.y = 0;

        //Keyboard
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
    }
}
