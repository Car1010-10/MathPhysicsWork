using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grid2D : MonoBehaviour
{
    public Vector3 screenSize;
    public Vector3 origin;

    public float gridSize = 10.0f; 
    public float minGridSize = 2.0f;
    public float originSize = 0.6f;

    float pointOffset;

    public int divisionCount = 5;
    public int minDivisionCount = 2;

    public float ScaleGrid2Screen = 5.0f;
    public float ScaleScreen2Grid = 2.5f;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;
    public bool isDrawingGrid = true;
    public bool isDrawingObject = true;

    public List<DrawingObject> drawingObjects = new List<DrawingObject>(); 

    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);

        
        //drawingObjects.Add("Arrow");
        //DrawingObject obj = new DrawingObject("Arrows");
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
        Mouse mouse = Mouse.current; 
        Keyboard kb = Keyboard.current;

        if ((kb == null) || (mouse == null))
        {
            Debug.LogWarning("No Keyboard or Mouse");
            return; 
        }
        // If you get here mouse and kb are valid objects. 
        bool controlKey = kb.ctrlKey.isPressed; 
        Vector2 scroll = mouse.scroll.ReadValue();

        if (( scroll.y > 0) && !controlKey) 
        {
            gridSize++; 
        }
        if (( scroll.y < 0 ) && !controlKey)
        {
            gridSize--; 
            if (  gridSize <= minGridSize )
            {
                gridSize = minGridSize;
            }
        }

        if ((scroll.y > 0) && controlKey)
        {
            divisionCount++;
        }
        if ((scroll.y < 0) && controlKey)
        {
            divisionCount--;
            if ( divisionCount <= minDivisionCount)
            {
                divisionCount = minDivisionCount;
            }
        }
        
        if (mouse.leftButton.isPressed)
        {
            origin = mouse.position.ReadValue(); 
        }

        if (kb.digit1Key.wasPressedThisFrame)
        {
            isDrawingOrigin = !isDrawingOrigin; 
        }

        if (kb.digit2Key.wasPressedThisFrame)
        {
            isDrawingAxis = !isDrawingAxis; 
        }

        if (kb.digit3Key.wasPressedThisFrame)
        {
            isDrawingDivisions = !isDrawingDivisions;
        }

        if (kb.digit4Key.wasPressedThisFrame)
        {
            isDrawingGrid = !isDrawingGrid;
        }

        if (kb.digit5Key.wasPressedThisFrame)
        {
            isDrawingObject = !isDrawingObject;
        }
    }

    /// <summary>
    /// Draws the grid
    /// </summary>
    void DrawGrid()
    {
        if (!isDrawingGrid)
        {
            return;
        }

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
                drawColor = divisionColor;
            }
            // is Axis Line
            if (isDrawingAxis && (lineIndex == 0))
            {
                drawColor = axisColor;
            }

            drawOffset = new Vector3(gridSize, gridSize, 0) * lineIndex;
            posPoint = origin + drawOffset;
            negPoint = origin - drawOffset;

            DrawGridLines(posPoint, drawColor);
            DrawGridLines(negPoint, drawColor);

            // check to end drawing
            // Debug stop right away. 

            lineIndex++;
            if (IsOffScreen(posPoint) && IsOffScreen(negPoint))
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
        Vector3 top     = new Vector3(point.x,                     0,      0); //y
        Vector3 bottom  = new Vector3(point.x,          screenSize.y,      0); //y
        Vector3 left    = new Vector3(screenSize.x,         point.y,      0); //x
        Vector3 right   = new Vector3(0,                    point.y,      0); //x
         
        DrawLine(top, bottom, drawColor); 
        DrawLine(right, left, drawColor);   
    }
    
    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOrigin()
    {  
        if (!isDrawingOrigin)
        {
            return; 
        }

        pointOffset = gridSize * originSize;

        Vector3 top = origin;
        top.y += pointOffset; 
        Vector3 bottom = origin;
        bottom.y -= pointOffset;
        Vector3 left= origin;
        left.x -= pointOffset;
        Vector3 right = origin ;
        right.x += pointOffset;

        DrawLine(top, right, axisColor);
        DrawLine(right, bottom, axisColor);
        DrawLine(bottom, left, axisColor);
        DrawLine(left, top, axisColor);
    }

    public void DrawObject(DrawingObject lineObj, bool DrawOnGrid = true)
    {
        if (!isDrawingObject)
        {
            return;
        }


    }

    public bool IsOffScreen(Vector3 point)
    {
        /// Can you tell me how to get to Seaseme Street
        bool vertical = ((point.y < 0) || (point.y > screenSize.y));
        bool horirzonal = ((point.x < 0) || (point.x > screenSize.x));

        return (vertical && horirzonal);
    }

    public static float V3ToAngle(Vector3 startPoint, Vector3 endPoint)
    {
        //Use Atan2 to convert
        //don't forget to convert from radians

        //Mathf.Atan2(startPoint.x, endPoint.y); (180f/Mathf.PI);
        return Mathf.Atan2(startPoint.x, endPoint.y) * (180f / Mathf.PI); //not finished
    }

    public static float LineToAngle(Line line)
    {
        //Calls V3toAngle using the information from the line object


        Glint.AddCommand(line);
        return 10f; //not finished
    }

    public static Vector3 RotatePoint(Vector3 Center, float angle, Vector3 pointIN)
    {
        //For a given center point and angle, determines the new rotated of a given point (pointIN)
        /* 
        point = pointIN-Center; // Center is not at 0,0, so translate from Center to 0,0 Origin 
        xnew = point.X * cos(angle) - point.Y * sin(angle);
        ynew = point.X * sin(angle) + point.Y * cos(angle);
        */
        
        Vector3 point = pointIN - Center;
        float xnew = point.x * Mathf.Cos(angle) - point.y * Mathf.Sin(angle);
        float ynew = point.x * Mathf.Sin(angle) + point.y * Mathf.Cos(angle);

        return new Vector3(xnew, ynew, 0);//not finished
    }

    /// <summary>
    /// Takes the potential grid space and outputs it into screen space
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <returns>Vector3 translated to Screen Space</returns>
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        screenSize = gridSpace;
        return gridSpace;
    }

    /// <summary>
    /// Takes in screen space and outputs it as grid space
    /// </summary>
    /// <param name="screenSpace"></param>
    /// <returns>Vector3 translated to Grid Space</returns>
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        gridSize = screenSpace.x;
        gridSize = screenSpace.y;
        return screenSpace;
    }

    /// <summary>
    /// Draws the given line object. If you are creating new line object, use the overload that takes parameters instead. 
    /// </summary>
    /// <param name="line"></param>
    public void DrawLine(Line line, bool drawOnGrid = true)
    {
        Glint.AddCommand(line);
    }

    /// <summary>
    /// Draws a line, This overload takes line parameters
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    public void DrawLine(Vector3 start, Vector3 end, Color color, bool drawOnGrid = true)
    {
        Glint.AddCommand(new Line(start, end, color));
    }

    //Draws the Origin Point (or Symbol)
}