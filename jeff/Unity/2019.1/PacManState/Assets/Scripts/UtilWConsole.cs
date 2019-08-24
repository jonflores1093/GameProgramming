using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class UtilWConsole : Util
{
    
    public GameConsole GameConsole;

    void Start()
    {
        UtilStart();
    }

    public override void UtilStart()
    {
        base.UtilStart();
        logger = new UtilConsoleDebugLogger(GameConsole) { ShowDebug = true };
    }

    void Update()
    {
        UtilUpdate(); //simlar to base.Update();
    }
    
}
public class UtilConsoleDebugLogger : ILogger
{
    GameConsole gameConsole;

    public UtilConsoleDebugLogger(GameConsole console)
    {
        gameConsole = console;
    }

    public bool ShowDebug { get; set; }

    public void Log(string message)
    {
        if (ShowDebug)
            gameConsole.GameConsoleWrite(message);
    }
    public void Log(string message, UnityEngine.Object sender)
    {
        if (ShowDebug)
            gameConsole.GameConsoleWrite(message);
    }
}


