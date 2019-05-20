using UnityEngine;
using System.Collections;
using System;

public class SimpleGravity : MonoBehaviour {


    public Vector2 GravityDirection = new Vector2(0,-1);
    public float GravityAcceloration = 0.1f;
    private Vector3 moveTranslation;
    public float GravitySpeed = 0.0f;
    public float GravityMaxSpeed = 10.0f;

    private Vector2 inferedMovementDirection;
    private Vector3 currentPosition, previousPosition;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if (GravitySpeed < GravityMaxSpeed)
        {
            GravitySpeed = Mathf.Clamp(GravitySpeed + GravityAcceloration, -1 * GravityMaxSpeed, GravityMaxSpeed);
        }

        this.moveTranslation = new Vector3(this.GravityDirection.x, this.GravityDirection.y) * GravitySpeed * Time.deltaTime ;
        this.previousPosition = this.transform.position;
        this.currentPosition += new Vector3(this.moveTranslation.x, this.moveTranslation.y);
        
        UpdateGravityBounce();
        this.transform.position = this.currentPosition;
    }

    private void UpdateGravityBounce()
    {

        inferedMovementDirection = currentPosition - previousPosition;
        inferedMovementDirection.Normalize();
        
        //Hack hard coded floor 
        if (this.transform.position.y < -4.0)
        {
            if (inferedMovementDirection.y < 0)
            {
                //going down reverse the speed vector
                this.GravitySpeed *= -1;
            }
        }
    }
}
