using UnityEngine;
using System.Collections;

public class UnityPacMan : PacMan {

    private GameObject _gameObject;

    public UnityPacMan(GameObject g) : base()
    {
        _gameObject = g;
    }

    public override void Log(string s)
    {
        Debug.Log(s);
    }

    protected override void NotifyAnimator(int value)
    {
        var animator = _gameObject.GetComponent<Animator>();

        animator.SetInteger("State", value);
        Debug.Log("animator State set to " + value);

        base.NotifyAnimator(value);
    }

}
