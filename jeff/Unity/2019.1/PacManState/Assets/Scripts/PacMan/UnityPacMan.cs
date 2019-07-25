using UnityEngine;
using System.Collections;
using Assets.Scripts.PacMan;

public class UnityPacMan : PacMan, ILoggable
{

    private GameObject _gameObject;
    public bool ShowDebug { get; protected set; }

    public UnityPacMan(GameObject g) : base()
    {
        _gameObject = g;
    }

    public override void Log(string s)
    {
        if(ShowDebug) Debug.Log(s);
    }
	
}
