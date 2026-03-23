using UnityEngine;

using Rect = System.Drawing.Rectangle;
public class DrawingTools
{
    /// <summary>
    /// Draw a Rectangle on the Screen
    /// </summary>
    /// <param name="box">Rectangle to draw</param>
    /// <param name="color">Color to draw, use Color.#### </param>
    /// <param name="grid"> if grid = null, info in Rect is in screen coordinates, else info is in grid space</param>
    public static void DrawRectangle(Rect box, Color color, DrawableGrid grid = null)
    {
        Line Bottom = new Line(new Vector2(box.X, box.Y), new Vector2((box.X + box.Width), box.Y), color);
        Line Right = new Line(new Vector2((box.X + box.Width), box.Y), new Vector2((box.X + box.Width), (box.Y + box.Height)), color);
        Line Top = new Line(new Vector2((box.X + box.Width), (box.Y + box.Height)), new Vector2(box.X, (box.Y + box.Height)), color);
        Line Left = new Line(new Vector2(box.X, (box.Y + box.Height)), new Vector2(box.X, box.Y), color);

        if (grid == null)
        {
            //info is in Screen Space
            Glint.AddCommand(Bottom);
            Glint.AddCommand(Right);
            Glint.AddCommand(Top);
            Glint.AddCommand(Left);
        }
        else
        {
            grid.DrawLine(Bottom);
            grid.DrawLine(Right);
            grid.DrawLine(Top);
            grid.DrawLine(Left);
        }
       
    }
    
    /// <summary>
    /// Find a point on a circle with given information
    /// </summary>
    /// <param name="origin">Center of the Cirle</param>
    /// <param name="angle">Angle in degrees, 0 degrees at (1,0,0)</param>
    /// <param name="radius">length of the radius</param>
    /// <returns>point in Vector3</returns>
    public static Vector3 CircleRadiusPoint(Vector3 origin, float angle, float radius)
    {
        Vector3 result = Vector3.zero;
        result.x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        result.y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        result += origin;

        return result;
    }

    /// <summary>
    /// Find a point on an ellipse  
    /// </summary>
    /// <param name="origin">Center of the Cirle</param>
    /// <param name="angle">Angle in degrees, 0 degrees at (1,0,0)</param>
    /// <param name="axis">length and axis of the elipse, this is half of the width or height</param>
    /// <returns>point in Vector3</returns>
    public static Vector3 EllipseRadiusPoint(Vector3 origin, float angle, Vector3 axis)
    {
        Vector3 result = Vector3.zero;
        result.x = Mathf.Cos(angle * Mathf.Deg2Rad) * axis.x;
        result.y = Mathf.Sin(angle * Mathf.Deg2Rad) * axis.y;
        result += origin;

        return result;
    }

    /// <summary>
    /// Draw a Circle in Screen Space 
    /// </summary>
    /// <param name="position">Position to draw in Screen Space</param>
    /// <param name="radius">Circle radius</param>
    /// <param name="sides">How many Sides of the Object. If Sides Less than 3, defaults to 12</param>
    /// <param name="color">Color to draw, use Color.####</param>
    public static void DrawCircle(Vector3 position, float radius, int sides, Color color)
    {
        int numberofSides = sides;
        if (numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            lineStart = CircleRadiusPoint(position, (degreeStep * i), radius);
            //might get some floating point squirly stuff cuz its not perfect
            lineEnd = CircleRadiusPoint(position, (degreeStep * (i+1)), radius);
            newline = new Line(lineStart, lineEnd, color);

            Glint.AddCommand(newline);
        }

    }

    /// <summary>
    /// Draw an Elipse in Screen Space 
    /// </summary>
    /// <param name="position">Position to draw in Screen Space</param>
    /// <param name="axis">Half Width\Height of the Ellipse</param>
    /// <param name="sides">How many Sides of the Object. If Sides Less than 3, defaults to 12</param>
    /// <param name="color">Color to draw, use Color.####</param>
    public static void DrawEllipse(Vector3 position, Vector2 axis, int sides, Color color)
    {
        int numberofSides = sides;
        if (numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            lineStart = EllipseRadiusPoint(position, (degreeStep * i), axis);
            //might get some floating point squirly stuff cuz its not perfect
            lineEnd = EllipseRadiusPoint(position, (degreeStep * (i + 1)), axis);
            newline = new Line(lineStart, lineEnd, color);

            Glint.AddCommand(newline);
        }

    }

    public static DrawableObject CreateCircleObject(Vector3 position, float radius, int sides, Color color)
    {
        DrawableObject newCircle = new DrawableObject();

        int numberofSides = sides;
        if (numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            lineStart = CircleRadiusPoint(position, (degreeStep * i), radius);
            //might get some floating point squirly stuff cuz its not perfect
            lineEnd = CircleRadiusPoint(position, (degreeStep * (i + 1)), radius);
            newline = new Line(lineStart, lineEnd, color);

            newCircle.AddLineToObject(newline);
        }

        return newCircle;
    }

    public static DrawableObject CreateEllipseObject(Vector3 position, Vector2 axis, int sides, Color color)
    {
        DrawableObject newEllipse = new DrawableObject();

        int numberofSides = sides;
        if (numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            lineStart = EllipseRadiusPoint(position, (degreeStep * i), axis);
            //might get some floating point squirly stuff cuz its not perfect
            lineEnd = EllipseRadiusPoint(position, (degreeStep * (i + 1)), axis);
            newline = new Line(lineStart, lineEnd, color);

            newEllipse.AddLineToObject(newline);
        }

        return newEllipse;
    }

}
