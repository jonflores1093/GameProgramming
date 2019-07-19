using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

    GhostSprite parent;
    public Vector2 Direction;
    public Vector3 viewPoint;

    // Use this for initialization
    void Start () {
        parent = this.GetComponent<GhostSprite>();

	}
    Vector3 headingNeg;
    Vector3 heading;
    RaycastHit2D[] hit;

    // Update is called once per frame
    void Update () {

        //bounce off of walls
        this.parent.transform.position = Util.BounceOffWalls(this.transform.position, GetComponent<Renderer>().bounds.size.x - 1, GetComponent<Renderer>().bounds.size.y - 1, ref this.Direction);


        switch (this.parent.State)
        {
            case GhostState.Dead:
                
                break;
            case GhostState.Evading:
                //Evade
                //Change ghost tecture to blue
                //Too close change direction
                if ((parent.PacMan.transform.position - this.transform.position).magnitude < 1)
                {
                    //change direction
                }
                
                headingNeg = parent.PacMan.transform.position - this.transform.position;
                headingNeg = Vector3.Normalize(headingNeg);
                this.Direction = headingNeg * -1;
                break;
            case GhostState.Chasing:
                
                
                heading = parent.PacMan.transform.position - this.transform.position;
                heading = Vector3.Normalize(heading);
                this.Direction = heading;

                break;
            case GhostState.Roving:
                
                viewPoint = new Vector3(this.transform.position.x + (this.parent.Ghost.Direction.x * 5), this.transform.position.y + (this.parent.Ghost.Direction.y * 5));
                hit = Physics2D.LinecastAll(this.transform.position, this.viewPoint);
                Debug.DrawLine(this.transform.position, this.viewPoint, Color.green);

                for (int i = 0; i < hit.Length; i++)
                    if (hit[i].rigidbody != null)
                    {
                        if (hit[i].rigidbody.tag == "Player")
                        {
                            Debug.Log(string.Format("{0} saw {1} changed to chasing", this, hit[i].rigidbody.tag));
                            //Should this be a method call or should controller change state?
                            this.parent.State = GhostState.Chasing;
                        }
                    }

                break;
        }
    }
}


