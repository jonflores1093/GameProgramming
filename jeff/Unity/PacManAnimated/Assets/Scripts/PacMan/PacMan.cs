using UnityEngine;
using System.Collections;
using System;

public enum PacManState {  Still, Chomping, Dying, Spawning, SuperPacMan }

public class PacMan  {

    protected PacManState _state;
    public PacManState State { 
        get { return _state; } 
        set {
                if (_state != value)
                {
                    this.Log (string.Format("{0} was: {1} now {2}", this.ToString(), _state, value));
                this.NotifyAnimator((int)value);
                    _state = value;
                }
            } 
    }

    protected virtual void NotifyAnimator(int value)
    {
        //nothing 
    }

    public PacMan()
    {
        this.State = PacManState.Still;
    }

    public virtual void Log(string s)
    {
        //nothing
    }
	
}
