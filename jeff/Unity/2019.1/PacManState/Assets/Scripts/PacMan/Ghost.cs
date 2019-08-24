using UnityEngine;
using System.Collections;

public enum GhostState { Chasing, Evading, Roving, Dead }

public class Ghost {

	public Vector2 Direction = new Vector2();
	
	protected GhostState state;
	public GhostState State
	{
		get { return state;}
		set { 
				if(this.state != value)		//only set value if state is changed
				{
					this.state = value;
					
					//if state is set to Roving pick random direction
					switch(State)
					{
					case GhostState.Roving :
						RandomDirection();
						break;
					case GhostState.Chasing :
						break;
					case GhostState.Evading :
						break;
					case GhostState.Dead :	
						this.Direction.x = this.Direction.y = 0;
						break;
					}
				}
			}
	}
	
	public Ghost()
	{
		this.State = GhostState.Roving;
	}

    public virtual void Log(string s)
    {
        //nothing
    }


    public void RandomDirection()
	{
		//TODO use Random from mono instead of unity
		this.Direction.x = Random.Range(-1,1);
		this.Direction.y = Random.Range(-1,1);
		//Check for Direction if there is none set it randomly
		if(this.Direction.sqrMagnitude == 0)
		{
			this.RandomDirection();
		}
	}
	
}


