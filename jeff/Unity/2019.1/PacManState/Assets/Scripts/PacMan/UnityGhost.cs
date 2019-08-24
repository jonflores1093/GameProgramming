using UnityEngine;
using System.Collections;

namespace Assets.Scripts.PacMan
{
    public class UnityGhost : Ghost, ILoggable
    {
        protected GameObject _gameObject;
        public bool ShowDebug { get; protected set; }

        public UnityGhost(GameObject g) : base()
        {
            _gameObject = g;
            this.ShowDebug = true;
        }

        public override void Log(string s)
        {
            if(ShowDebug) Debug.Log(s);
        }
    }

    public class UnitGhostGameConsole : UnityGhost
    {
        IGameConsole console;

        public UnitGhostGameConsole(GameObject g, IGameConsole console) : base(g)
        {
            this.console = console;
        }

        public override void Log(string s)
        {
            console.GameConsoleWrite(s);
        }
    }
}
