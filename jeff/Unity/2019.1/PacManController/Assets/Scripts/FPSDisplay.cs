using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    int w = Screen.width, h = Screen.height;

    Rect rect;
    float msec;
    float fps;

    string text;
    GUIStyle style;

    void Start()
    {
        rect = new Rect(0, 0, w, h* 2 / 100);
        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    //OnGUI may be called mutilple times per draw 
    void OnGUI()
    {
        msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
