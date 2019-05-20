using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeySubject : MonoBehaviour, ISubject {

    List<IObserver> KeyClients;

    public Vector2 direction = new Vector2();
    public Vector2 keyDirection;
    public Vector2 oldkeyDirection;
    public bool IsKeyDown
    {
        get
        {
            if (keyDirection.sqrMagnitude == 0) return false;
            return true;
        }
    }

    public KeySubject()
    {
        keyDirection = new Vector2();
    }
    
    // Use this for initialization
	void Start () {
        this.KeyClients = new List<IObserver>();
	}
	
	// Update is called once per frame
	void Update () {
        //direction.x = direction.y = 0;
        keyDirection.x = keyDirection.y = 0;

        //Keyboard
        if (Input.GetKey("right"))
        {
            keyDirection.x += 1;
        }
        if (Input.GetKey("left"))
        {
            keyDirection.x += -1;
        }

        if (Input.GetKey("up"))
        {
            keyDirection.y += 1;
        }
        if (Input.GetKey("down"))
        {
            keyDirection.y += -1;
        }

        direction += keyDirection;
        direction.Normalize();
        if (oldkeyDirection != keyDirection)
        {
            this.Notify();

        }
        oldkeyDirection = keyDirection;
	}

    public void Attach(IObserver o)
    {
        this.KeyClients.Add(o);
    }

    public void Detach(IObserver o)
    {
        this.KeyClients.Remove(o);
    }

    public void Notify()
    {
        foreach (var client in KeyClients)
        {
            client.ObserverUpdate(this, keyDirection); 
        }
    }
}
