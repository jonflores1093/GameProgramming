using UnityEngine;
using System.Collections;

public class GhostSprite : MonoBehaviour {

	public GameObject PacMan;
	public GhostState State;
	private Ghost ghost;
    public Ghost Ghost { get { return this.ghost; } }
	public float Speed;
	
	private Vector3 moveTranslation;
	SpriteRenderer spriteRenderer;
    
    public Sprite EvadeTexture, NormalTexture;

    protected Vector3 viewPoint;
    protected GhostController controller;
	
	// Use this for initialization
	void Start () {
        this.SetupGhost();
	}


    public virtual void SetupGhost()
    {
        this.ghost = new Ghost();
        Util.GetComponentIfNull<SpriteRenderer>(this, ref spriteRenderer);
        this.State = this.ghost.State;
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.controller = this.GetComponent<GhostController>();
        //Set Textures with script rather than UI
        NormalTexture = Resources.Load<Sprite>("Artwork/RedGhost");
        EvadeTexture = Resources.Load<Sprite>("Artwork/GhostHit");

    }

    protected virtual void ChangeGhostTextureToNormal()
    {
        //Change texture if Chasing
        this.spriteRenderer.color = Color.white;
        this.spriteRenderer.sprite = NormalTexture;

    }
    protected virtual void ChangeGhostTectureToBlue()
    {
        //Change texture if evading
        this.spriteRenderer.color = Color.white;
        this.spriteRenderer.sprite = EvadeTexture;
    }

    protected virtual void ChangeGhostTectureToBlack()
    {
        //Change texture if evading
        this.spriteRenderer.color = Color.grey;
        this.spriteRenderer.sprite = EvadeTexture;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") //Make sure player is tagged 
        {
            if (this.State == GhostState.Dead)
            {
                Debug.Log(string.Format("{0} triggerEnter with {1} already dead change to Roving", this, coll.ToString()));
                this.State = GhostState.Roving;
            }
            else
            {
                Debug.Log(string.Format("{0} triggerEnter with {1} change to dead", this, coll.ToString()));
                this.State = GhostState.Dead;
            }
        }
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawLine(this.transform.position, this.viewPoint);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        switch (this.State)
        {
            case GhostState.Dead:
                //change to dead texture
                this.ChangeGhostTectureToBlack();
                break;
            case GhostState.Evading:
                //Evade
                //Change ghost tecture to blue

                //Too close change direction
                //if((PacMan.transform.position-this.transform.position).magnitude < 1)
                //{
                //    //change direction
                //}
                this.ChangeGhostTectureToBlue();
                //Vector3 headingNeg = PacMan.transform.position - this.transform.position;
                //headingNeg = Vector3.Normalize(headingNeg);
                //this.ghost.Direction = headingNeg * -1;
                break;
            case GhostState.Chasing:
                this.ChangeGhostTextureToNormal();
                if (PacMan == null)
                {
                    this.State = GhostState.Roving;
                    break;
                }
                //Vector3 heading = PacMan.transform.position - this.transform.position;
                //heading = Vector3.Normalize(heading);
                //this.ghost.Direction = heading;

                break;
            case GhostState.Roving:
                this.ChangeGhostTextureToNormal();
                //viewPoint = new Vector3(this.transform.position.x + (this.ghost.Direction.x * 5), this.transform.position.y + (this.ghost.Direction.y * 5));
                //RaycastHit2D[] hit = Physics2D.LinecastAll(this.transform.position, this.viewPoint);
                //Debug.DrawLine(this.transform.position, this.viewPoint, Color.green);

                //for (int i = 0; i < hit.Length; i++)
                //    if (hit[i].rigidbody != null)
                //    {
                //        if (hit[i].rigidbody.tag == "Player")
                //        {
                //            Debug.Log(string.Format("{0} saw {1} changed to chasing", this, hit[i].rigidbody.tag));
                //            this.State = GhostState.Chasing;
                //        }
                //    }

                break;
        }

        this.ghost.Direction = this.controller.Direction;

        UpdateGhostLocation();

        if (this.State != this.ghost.State)		//only set value if state is changed
        {
            this.ghost.State = this.State;
        }



    }

    private void UpdateGhostLocation()
    {
        //move the ghost
        this.moveTranslation = new Vector3(this.ghost.Direction.x, this.ghost.Direction.y) * Time.deltaTime * this.Speed;
        this.transform.position = new Vector3(this.transform.position.x + this.moveTranslation.x,
                                                    this.transform.position.y + this.moveTranslation.y);
    }
}
