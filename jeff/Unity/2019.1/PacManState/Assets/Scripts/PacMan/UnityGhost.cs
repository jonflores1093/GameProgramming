using UnityEngine;
using System.Collections;

namespace Assets.Scripts.PacMan
{
    public class UnityGhost : Ghost, ILoggable
    {
        private GameObject _gameObject;
        public bool _debug { get; protected set; }

        public UnityGhost(GameObject g) : base()
        {
            _gameObject = g;
            this._debug = true;
        }

        public override void Log(string s)
        {
            if(_debug)Debug.Log(s);
        }
    }
}
