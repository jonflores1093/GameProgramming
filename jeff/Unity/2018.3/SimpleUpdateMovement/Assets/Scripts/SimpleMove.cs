using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {

    // Use this for initialization
    public Vector2 Direction = new Vector2(1, 0);
    public float Speed = 10;
    public float Angle;
    private Vector3 moveTranslation;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.moveTranslation = new Vector3(this.Direction.x, this.Direction.y) * Time.deltaTime * this.Speed;
        this.transform.position += new Vector3(this.moveTranslation.x, this.moveTranslation.y);
    }
}
