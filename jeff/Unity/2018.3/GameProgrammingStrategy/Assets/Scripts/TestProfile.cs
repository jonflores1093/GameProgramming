using UnityEngine;
using System.Collections;

public class TestProfile : MonoBehaviour {

	private float outputTest = 0;

	void Start()
	{
		Util.Profile.StartProfile("Start");
	}
 
	void Update ()
	{
        Util.Profile.StartProfile("Update");
		for (int i = 0; i < 100; ++i)
			outputTest += Mathf.Sin(i * Time.time);
        Util.Profile.EndProfile("Update");
	}
 
	void OnApplicationQuit()
	{
        Util.Profile.EndProfile("Start");
		Debug.Log("outputTest is " + outputTest.ToString("F3"));
        Util.Profile.PrintResults();
	}
}
