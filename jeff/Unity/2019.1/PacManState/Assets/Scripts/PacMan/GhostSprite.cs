using UnityEngine;
using System.Collections;
using Assets.Scripts.PacMan;

[RequireComponent(typeof(GhostController))]
public class GhostSprite : MonoBehaviour {

	public GameObject PacMan;
	public GhostState State;
	protected Ghost ghost;
    public Ghost Ghost { get { return this.ghost; } }
	public float Speed;
	
	private Vector3 moveTranslation;
	SpriteRenderer spriteRenderer;
    
    public Sprite EvadeTexture, NormalTexture, DeadTexture;

    protected Vector3 viewPoint;
    protected GhostController controller;   //Controller decides which direction ghost moves
	
	// Use this for initialization
	void Start () {
        this.SetupGhost();
	}


    public virtual void SetupGhost()
    {

        this.ghost = new UnityGhost(this.gameObject);
        Util.GetComponentIfNull<SpriteRenderer>(this, ref spriteRenderer);
        this.State = this.ghost.State;
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.controller = this.GetComponent<GhostController>();
        //Set Textures with script rather than UI textures need to be in Resources folder
        NormalTexture = Resources.Load<Sprite>("Artwork/RedGhost");
        EvadeTexture = Resources.Load<Sprite>("Artwork/GhostHit");
        DeadTexture = Resources.Load<Sprite>("Artwork/DeadGhost");
        this.Ghost.Log($"{this} SetupGhost done");
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

    protected virtual void ChangeGhostTectureToDead()
    {
        //Change texture if evading
        //this.spriteRenderer.color = Color.grey; //for shading effect don't use this anymore
        this.spriteRenderer.sprite = DeadTexture;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") //Make sure player is tagged 
        {

            switch(this.State)
            {
                case GhostState.Roving:
                    this.ghost.Log(string.Format("{0} triggerEnter with {1} already dead change to Roving", this, coll.ToString()));
                    this.State = GhostState.Roving;
                    break;
                case GhostState.Dead:
                case GhostState.Evading:
                case GhostState.Chasing:
                    this.ghost.Log(string.Format("{0} triggerEnter with {1} change to dead", this, coll.ToString()));
                    this.State = GhostState.Dead;
                    break;
                
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
                this.ChangeGhostTectureToDead();
                break;
            case GhostState.Evading:
                //Evade
                this.ChangeGhostTectureToBlue();
                break;
            case GhostState.Chasing:
                this.ChangeGhostTextureToNormal();
                if (PacMan == null)
                {
                    this.State = GhostState.Roving;
                    break;
                }
                
                break;
            case GhostState.Roving:
                this.ChangeGhostTextureToNormal();
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
