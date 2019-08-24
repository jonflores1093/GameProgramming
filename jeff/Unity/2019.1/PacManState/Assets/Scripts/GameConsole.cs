using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public interface IGameConsole
{
    string FontName { get; set; }
    string ConsoleDebugText { get; set; }

    string GetGameConsoleText();
    void GameConsoleWrite(string s);
}

public class GameConsole : MonoBehaviour, IGameConsole
{

    protected string fontName;
    public string FontName { get { return fontName; } set { fontName = value; } }

    protected string consoleDbugText;
    public string ConsoleDebugText { get { return consoleDbugText; } set { consoleDbugText = value; } }

    [SerializeField]
    protected int maxLines= 15;
    public int MaxLines { get { return maxLines; } set { maxLines = value; } }

    protected List<string> gameConsoleText;
    protected GameConsoleState gameConsoleState;

    public KeyCode ToggleConsoleKey = KeyCode.Tilde;

    public Text ConsoleText, DebugText;

    public void Awake()
    {
        SetupConsole();
    }

    protected void SetupConsole()
    {
        ToggleConsoleKey = KeyCode.BackQuote;
        gameConsoleText = new List<string>();
        gameConsoleState = GameConsoleState.Open;
        //ToggleConsole();
        
        GameConsoleWrite("Awake Called");
        consoleDbugText = "Default Debug Text";

        //Test for Canvas and GameConsoleText and Debug Text

        //set overflow for text boxes 
        ConsoleText.horizontalOverflow = HorizontalWrapMode.Overflow;
        DebugText.horizontalOverflow = HorizontalWrapMode.Overflow;
        ConsoleText.verticalOverflow = VerticalWrapMode.Overflow;
        DebugText.verticalOverflow = VerticalWrapMode.Overflow;
    }

    public void Update()
    {
        UpdateConsole();

    }

    public virtual void UpdateConsole()
    {
        //Console.enabled = false;
        if (gameConsoleState == GameConsoleState.Open)
        {
            ConsoleText.enabled = true;
            ConsoleText.text = GetGameConsoleText();
            DebugText.text = ConsoleDebugText;
        }
        else
        {
            DebugText.text = string.Empty;
            ConsoleText.text = string.Empty;

        }
        if (Input.GetKeyUp(ToggleConsoleKey))
        {
            this.ToggleConsole();
        }
    }

    public void ToggleConsole()
    {
        if (this.gameConsoleState == GameConsoleState.Closed)
        {
            this.gameConsoleState = GameConsoleState.Open;
            ConsoleText.enabled = true;
            DebugText.enabled = true;

        }
        else
        {
            this.gameConsoleState = GameConsoleState.Closed;
            ConsoleText.enabled = false;
            DebugText.enabled = false;
        }
    }

    public void Draw()
    {

    }

    string Text;
    string[] current;
    int offsetLines;
    int offest;
    int indexStart;

    public string GetGameConsoleText()
    {
        Text = "";

        current = new string[System.Math.Min(gameConsoleText.Count, MaxLines)];
        offsetLines = (gameConsoleText.Count / maxLines) * maxLines;

        offest = gameConsoleText.Count - offsetLines;

        indexStart = offsetLines - (maxLines - offest);
        if (indexStart < 0)
            indexStart = 0;

        gameConsoleText.CopyTo(
            indexStart, current, 0, System.Math.Min(gameConsoleText.Count, MaxLines));

        foreach (string s in current)
        {
            Text += s;
            Text += "\n";
        }
        return Text;
    }

    public void GameConsoleWrite(string s)
    {
        gameConsoleText.Add(s);
    }

    //Console State
    public enum GameConsoleState { Closed, Open };
}

