using UnityEngine;
using System.Collections;


public enum PacManState { Spawning, Still, Chomping, SuperPacMan }

public class PacMan  {

    private bool DEBUG;
    
    //Private state for pacman
    protected PacManState _state;
    public PacManState State { 
        get { return _state; } 
        set {
                //If state is changes
                if (_state != value)
                {
                    //Log state change
                    this.Log (string.Format("{0} was: {1} now {2}", this.ToString(), _state, value));
                    _state = value;
                }
            } 
    }

    public PacMan()
    {
        //starting state for pacman
        this.State = PacManState.Still;
        this.DEBUG = false;
    }

    public virtual void Log(string s)
    {
        //nothing
        if(DEBUG)
        {
            Debug.Log(s);
        }
    }
	
}
