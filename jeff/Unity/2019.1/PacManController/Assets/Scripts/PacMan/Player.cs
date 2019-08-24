using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Depends on PlayerContoller
    private PlayerController controller;

    public Vector2 Direction = new Vector2(1, 0);
    public float Speed = 10;
    public float Angle;

    public UnityPacMan PacMan { get; private set; }

    private Vector3 moveTranslation;
    
    // Use this for initialization
	void Start () {
        //Get PlayerController from game object
		controller = GetComponent<PlayerController>();
		//Log error if controller is null will throw null refernece exception eventually
		if (controller == null) {
			Debug.LogWarning( "GetComponent of type " + typeof( PlayerController ) + " failed on " + this.name, this );
		}

		//or
		//Util.GetComponentIfNull<PlayerController> (this, ref controller);

        PacMan = new UnityPacMan(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (this.controller.IsKeyDown) 
        {
			this.Direction = this.controller.ControllerDirection;
			Angle = Mathf.Atan2 (this.Direction.y, this.Direction.x) * Mathf.Rad2Deg;
			this.transform.eulerAngles = new Vector3 (0, 0, Angle);
            if (this.PacMan.State != PacManState.SuperPacMan)
            {
                this.PacMan.State = PacManState.Chomping;
            }
		}
		else
		{
			this.Direction = new Vector2(0,0);
            if (this.PacMan.State != PacManState.SuperPacMan)
            {
                this.PacMan.State = PacManState.Still;
            }
		}
		
		this.moveTranslation = new Vector3(this.Direction.x, this.Direction.y) * Time.deltaTime * this.Speed;
		this.transform.position += new Vector3(this.moveTranslation.x, this.moveTranslation.y);
        
	}

    public virtual void PowerUp()
    {
        this.PacMan.State = PacManState.SuperPacMan;
        this.StartCoroutine("PowerUpTimer"); //coroutines are memory wasters fix this with simple timer
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(3);
        this.PacMan.State = PacManState.Still;
    }
}
