using UnityEngine;
using System.Collections;

public class SimpleMoveKeepOnScreen : MonoBehaviour {

    // Use this for initialization
    public Vector2 Direction = new Vector2(1, 0);
    public float Speed = 10;
    public float Angle;
    private Vector3 moveTranslation;

    //camera
    private Vector3 cameraBottomLeft;
    private Vector3 cameraTopRight;
    private Rect cameraRect;            //Rectangle for current camera view

    bool showDebug = true;              //turns on and off console logging

    void Start()
    {
        cameraBottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        cameraTopRight = Camera.main.ScreenToWorldPoint(new Vector3(
            Camera.main.pixelWidth, Camera.main.pixelHeight));

        //cameraRect is only set on start should be moved if the camera moves
        cameraRect = new Rect(
            cameraBottomLeft.x,
            cameraBottomLeft.y,
            cameraTopRight.x - cameraBottomLeft.x,
            cameraTopRight.y - cameraBottomLeft.y);
    }

    // Update is called once per frame
    void Update()
    {
        this.BounceOffWalls(this.transform.position , 0.5f, 0.5f, ref this.Direction); //Bad hard coded width and length how can we fix this?
        this.UpdateMovement();
    }

    protected virtual void UpdateMovement()
    {
        //Adjust moveTranslate by deltatime
        this.moveTranslation = new Vector3(this.Direction.x, this.Direction.y) * Time.deltaTime * this.Speed;
        //change position
        this.transform.position += new Vector3(this.moveTranslation.x, this.moveTranslation.y);
    }

    public Vector3 BounceOffWalls(Vector3 position, float width, float height, ref Vector2 direction)
    {
        //if(cameraRect.xMin == cameraRect.x) throw new UnityException("No instance of Util in Scene");
        if (!cameraRect.Contains(position))
        {
            //keep  on screen
            if (position.x <= cameraRect.xMin)
            {
                if (showDebug) Debug.Log(string.Format("left xMin {0} pos {1} w {2} h {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.x *= -1;
            }
            if (position.x >= cameraRect.xMax)
            {
                if (showDebug) Debug.Log(string.Format("right xMin {0} pos {1} w {2} h {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.x *= -1;
            }
            if (position.y > cameraRect.yMax)
            {
                if (showDebug) Debug.Log(string.Format("top xMin {0} pos {1} w {2} h {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.y *= -1;
            }
            if (position.y < cameraRect.yMin)
            {

                if (showDebug) Debug.Log(string.Format("bottom xMin {0} pos {1} w {2} h {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.y *= -1;
            }
            position.x = Mathf.Clamp(position.x, cameraRect.xMin, cameraRect.xMax);
            position.y = Mathf.Clamp(position.y, cameraRect.yMin, cameraRect.yMax);
            if (showDebug) Debug.Log(string.Format("corected position {0} direction {1}", position, direction));
        }
        return position;
    }

}
