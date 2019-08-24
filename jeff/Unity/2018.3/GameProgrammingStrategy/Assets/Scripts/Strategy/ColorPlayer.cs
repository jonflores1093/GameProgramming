using UnityEngine;
using System.Collections;

public class ColorPlayer : MonoBehaviour {

	// Use this for initialization

    SpriteRenderer spriteRenderer;
    public Color color;

    public ColorPlayer()
    {
       
    }

	void Start () {

        Util.GetComponentIfNull<SpriteRenderer>(this, ref spriteRenderer);
        //this.GetComponent<SpriteRenderer>();
        color = Color.white;
        this.spriteRenderer.color = color;
	}
	
	// Update is called once per frame
	void Update () {
        this.spriteRenderer.color = color;
	}

    public void ChangeColor(Color color)
    {
        this.color = color;
    }
}
