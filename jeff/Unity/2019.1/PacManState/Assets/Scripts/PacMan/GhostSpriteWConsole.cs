using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpriteWConsole : GhostSprite
{
    public GameConsole console;

    public override void SetupGhost()
    {
        base.SetupGhost();
        //if console isn't null make new ghost with console
        if(console != null)
            this.ghost = new GhostWithUnityGameConsole(console);
        this.ghost.Log("GhostSpriteWConsole setup done.");
    }

   

}

public class GhostWithUnityGameConsole : Ghost
{
    protected GameConsole console;

    public GhostWithUnityGameConsole(GameConsole console)
    {
        this.console = console;
    }

    public override void Log(string s)
    {
        console.GameConsoleWrite(s);
    }
}
