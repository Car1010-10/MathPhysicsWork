using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Grid2D : MonoBehaviour
{
    public Vector3 screenSize;
    public Vector3 origin;

    float gridSize = 10.0f; 
    float minGridSize = 2.0f;
    public float originSize = 0.6f;

    float pointOffset;

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
        Vector3 drawOffset = Vector3.zero;
        Vector3 posPoint = Vector3.zero; 
        Vector3 negPoint = Vector3.zero;
        Color drawColor = lineColor;

        int lineIndex = 0;

        bool isStillDrawing = true;
        while (isStillDrawing)
        {
            drawColor = lineColor;
            // is Division Line 
            if (isDrawingDivisions && ((lineIndex % divisionCount) == 0))
            {
                lineColor = divisionColor;
            }
            // is Axis Line
            if (isDrawingAxis && (lineIndex == 0))
            {
                lineColor = axisColor;
            }

            drawOffset = new Vector3(gridSize, gridSize, 0) * lineIndex;
            posPoint = origin + drawOffset;
            negPoint = origin - drawOffset;

            DrawGridLines(posPoint, axisColor);
            DrawGridLines(negPoint, axisColor);

            // check to end drawing
            // Debug stop right away. 

            lineIndex++;
            if (lineIndex >= 35)
            {
                isStillDrawing = false;
            }
        }
       

        DrawOrigin();
    }

    /// <summary>
    /// Draw horizonal and vertical line at point given with color given. 
    /// </summary>
    /// <param name="point"></param>
    /// <param name="drawColor"></param>
    void DrawGridLines(Vector3 point, Color drawColor)
    {

        Vector3 top     = new Vector3(0,            1,      0);
        Vector3 bottom  = new Vector3(0,            -1,      0);
        Vector3 left    = new Vector3(-1,            0,      0);
        Vector3 right   = new Vector3(1,            0,      0);

        DrawLine(top, bottom, drawColor); 
        DrawLine(left, right, drawColor);   
    }

    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOrigin()
    {  
        pointOffset = gridSize * originSize;

        Vector3 top = origin;
        top.y += pointOffset; 
        Vector3 bottom = origin;
        bottom.y -= pointOffset;
        Vector3 left= origin;
        left.x -= pointOffset;
        Vector3 right = origin ;
        right.x += pointOffset;

        DrawLine(top, right, Color.red);
        DrawLine(right, bottom, Color.red);
        DrawLine(bottom, left, Color.red);
        DrawLine(left, top, Color.red);
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