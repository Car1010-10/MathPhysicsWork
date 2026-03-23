using UnityEngine;

using Rect = System.Drawing.Rectangle; 

public class lab05Grid : DrawableGrid
{
    Rect box;
    Rect boxOnGrid;

    Vector3 DrawTestPoint;

    Line circleRadiusLine;
    float circleRadius = 10;
    Line elipseRadiusLine;
    Vector2 elipseAxis;

    DrawableObject ellipseObject;

    float offset;

    public override void SetupScenes()
    {
        int sceneIndex;
        DrawableObject newObject;

        //create drawing tools
        sceneIndex = AddScene("Drawing Tools Test");

        //create circle object
        newObject = DrawingTools.CreateCircleObject(Vector3.zero, 10, 36, Color.yellow);
        newObject.Position.x = -75;
        newObject.Position.y = 20;
        AddObjectToScene(sceneIndex, newObject);

        //create eclipse object
        newObject = DrawingTools.CreateEllipseObject(Vector3.zero, new Vector2 (10, 20), 36, Color.yellow);
        newObject.Position.x = 75;
        newObject.Position.y = 15;
        ellipseObject = newObject;
        AddObjectToScene(sceneIndex, newObject);

        box = new Rect(100, 100, 100, 100);
        boxOnGrid = new Rect(50, 30, 5, 5);
        elipseAxis = new Vector2(50, 75);
        offset = 0;

        circleRadiusLine = new Line(Vector3.zero, Vector3.zero, Color.cyan);
        elipseRadiusLine = new Line(Vector3.zero, Vector3.zero, Color.magenta);

        DrawTestPoint = origin;
        //DrawTestPoint.x *= .5f;

        circleRadiusLine.start = ScreenToGrid(DrawTestPoint);
        elipseRadiusLine.start = ScreenToGrid(DrawTestPoint);
    }

    public override void Tick()
    {
        offset += Time.deltaTime;

        DrawingTools.DrawRectangle(box, Color.red);
        DrawingTools.DrawRectangle(boxOnGrid, Color.green, this);

        circleRadiusLine.end = DrawingTools.CircleRadiusPoint(ScreenToGrid(DrawTestPoint), offset * 90, circleRadius);
        DrawLine(circleRadiusLine);
        //needs to account for grid size here
        DrawingTools.DrawCircle(DrawTestPoint, circleRadius * gridSize, 36, Color.white);

        elipseRadiusLine.end = DrawingTools.EllipseRadiusPoint(ScreenToGrid(DrawTestPoint), offset * 45, elipseAxis);
        DrawLine(elipseRadiusLine);
        //needs to account for grid size here
        DrawingTools.DrawEllipse(DrawTestPoint, elipseAxis * gridSize, 12, Color.grey);

        ellipseObject.Roation = offset * 15 * Mathf.Deg2Rad;
    }
}
