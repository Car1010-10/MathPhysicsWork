using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grid2D : MonoBehaviour
{
    public Vector3 screenSize;
    public Vector3 origin;

    float gridSize = 10f; 
    float minGridSize = 2f;
    public float originSize = .6f;

    float point;
    float topLine;
    float bottomLine;
    float leftLine;
    float rightLine;

    int divisionCount = 5;
    int minDivisionCount = 2;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;


    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);

    }

    void Update()
    {
        GetInput();
        DrawGrid();
    }

    /// <summary>
    /// Grabs Input 
    /// </summary>
    void GetInput()
    {

    }

    /// <summary>
    /// Draws the grid
    /// </summary>
    void DrawGrid()
    {

    }

    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOrigin()
    {
        point = gridSize * originSize;

        //topLine = origin.y + Vector3(0, point, Color.white);
        //bottomLine = origin.y + DrawLine(0, point, Color.white);
        //rightLine = origin.y + DrawLine(0, point, Color.white);
        //leftLine = origin.y + DrawLine(0, point, Color.white);
    }

    /// <summary>
    /// Takes the potential grid space and outputs it into screen space
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <returns>Vector3 translated to Screen Space</returns>
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Takes in screen space and outputs it as grid space
    /// </summary>
    /// <param name="screenSpace"></param>
    /// <returns>Vector3 translated to Grid Space</returns>
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Draws the given line object. If you are creating new line object, use the overload that takes parameters instead. 
    /// </summary>
    /// <param name="line"></param>
    public void DrawLine(Line line)
    {
        Glint.AddCommand(line);
    }

    /// <summary>
    /// Draws a line, This overload takes line parameters
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    public void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Glint.AddCommand(new Line(start, end, color));
    }

    //Draws the Origin Point (or Symbol)
}