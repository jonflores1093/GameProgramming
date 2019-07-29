using UnityEngine;
using System.Collections;
using Assets.Scripts.PacMan;

public class UnityPacMan : PacMan, ILoggable
{

    protected GameObject _gameObject;
    public bool ShowDebug { get; set; }

    public UnityPacMan(GameObject g) : base()
    {
        _gameObject = g;
    }

    public override void Log(string s)
    {
        if(ShowDebug) Debug.Log(s);
    }
	
}

public class UnityPacManWConsole : UnityPacMan
{
    protected GameConsole console;

    public UnityPacManWConsole(GameObject g, GameConsole console) : base(g)
    {
        this.console = console;
    }

    /// <summary>
    /// Log to game console
    /// </summary>
    /// <param name="s"></param>
    public override void Log(string s)
    {
        if (ShowDebug) this.console.GameConsoleWrite(s);
    }
}
