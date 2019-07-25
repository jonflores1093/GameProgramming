using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWConsole : Player
{
    public GameConsole console;

    protected override void SetupPlayer()
    {
        base.SetupPlayer();
        this.PacMan = new UnityPacManWConsole(this.gameObject, console);
        ((UnityPacManWConsole)this.PacMan).ShowDebug = true; //Turn on debug notice the cast cuz PacMan object is boxed
        this.PacMan.Log("PlayerWConsole setup done.");
    }
}
