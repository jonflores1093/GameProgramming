using UnityEngine;
using System.Collections;

public class KeyObserver : MonoBehaviour, IObserver {



    ISubject sub;
    
    // Use this for initialization
	void Start () {

        sub = (ISubject)Camera.main.GetComponent<KeySubject>();
        sub.Attach(this);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ObserverUpdate(object sender, object message)
    {
        throw new System.NotImplementedException();
    }
}
