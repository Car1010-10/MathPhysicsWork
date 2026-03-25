using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Rect = System.Drawing.Rectangle;
public static class CollisionTools 
{
    public static void DrawTriangle(TriangleData data, Color color, DrawableGrid grid = null)
    {
        Line lineA = new Line(data.PointA, data.PointB, color);
        Line lineB = new Line(data.PointB, data.PointC, color);
        Line lineC = new Line(data.PointC, data.PointA, color);

        if (grid == null)
        {
            // Info is in Screen Space 
            Glint.AddCommand(lineA);
            Glint.AddCommand(lineB);
            Glint.AddCommand(lineC); 
        }
        else
        {
            grid.DrawLine(lineA);
            grid.DrawLine(lineB);
            grid.DrawLine(lineC);
        }
    }

    public static void SetColor(DrawableObject thing,  Color color)
    {
        for (int i = 0; i < thing.LineList.Count; i++)
        {
            Line item = thing.LineList[i];
            item.color = color;
            thing.LineList[i] = item;

            // C# is acting weird... 
            // won't let me use foreach
            // wont' let me do LineList[i].color = color; 
        }
    }

    public static bool IsPointInCircle(Vector3 Point, Vector3 Center, float Radius)
    {
        // stub code 
        return true; 
    }

    public static bool IsPointInRectangle(Vector3 Point, Rect Rectangle)
    {
        // stub code 
        return false;
    }

    //incomplete
    public static bool SameSide(Vector3 Point, TriangleData Triangle)
    {
        Vector3 crossPoint1 = Vector3.Cross(Triangle.PointB - Triangle.PointA, Point - Triangle.PointA);
        Vector3 crossPoint2 = Vector3.Cross(Triangle.PointB - Triangle.PointA, Point - Triangle.PointA);

        if (Vector3.Dot(crossPoint1, crossPoint2) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //incomplete
    public static bool IsPointInTriangle(Vector3 Point, TriangleData Triangle)
    {
        if (SameSide(Point, Triangle))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
