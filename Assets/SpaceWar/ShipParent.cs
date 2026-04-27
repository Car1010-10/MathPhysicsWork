using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipParent : MovingObject
{
    public DrawableObject ship;
    public DrawableObject thrust;
    Line LaserObject;
    public bool IsShipA = true;

    public float ShipMaxVelocity = 250f;
    public float ShipThrust = 75f;
    public float ShipRotation = 180f; 
    public float SpawnPointDistance = 15f;

    public bool IsDrawingLaser = false;
    public float LaserStart = 5f;
    public float LaserEnd = 150f;
    public float LaserShowTime = .5f;
    public float LaserShowCounter = 0;

    public void SetupA(DrawableGrid grid, int sceneIndex)
    {
        IsShipA = true;

        ship = new ShipA();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipAThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        LaserObject = new Line();
        LaserObject.color = Color.yellow;

        //MaxVelocity = ShipMaxVelocity; 
    }

    public void SetupB(DrawableGrid grid, int sceneIndex)
    {
        IsShipA = false;

        ship = new ShipB();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipBThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        LaserObject = new Line();
        LaserObject.color = Color.yellow;

        //MaxVelocity = ShipMaxVelocity;
    }

    public override void Tick()
    {
        base.Tick();
        UpdateSubObjects();
        UpdateLaser();
    }

    public void UpdateLaser()
    {
        if (!IsDrawingLaser) { return; }

        LaserShowCounter -= Time.deltaTime;

        if (LaserShowCounter < 0 )
        {
            IsDrawingLaser = false;
            return;
        }

        LaserObject.start = this.Position + DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), LaserStart);
        LaserObject.end = this.Position + DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), LaserEnd);

        SpaceWarGrid.self.DrawLine(LaserObject);
        LaserCollisionDetection();
    }

    public void LaserCollisionDetection()
    {
        foreach(MovingObject mo in SpaceWarGrid.self.MovingObjectlist)
        {
            if (CollisionTools.DoesLineIntersectCircle(LaserObject.start, LaserObject.end, mo.Position, mo.CollisionRadius))
            {
                Debug.Log("Found Hit with " + mo.ToString());
                if (mo is ShipParent)
                {
                    if (((ShipParent)mo).IsShipA != this.IsShipA)
                    {
                        SpaceWarGrid.self.RecordKill(IsShipA);
                    }
                }

                if (mo is Missle)
                {
                    //cast it
                    Missle missle = (Missle)mo;
                    missle.RemoveMissle();
                }
            }
        }
    }

    public void UpdateSubObjects()
    {
        ship.Position = this.Position;
        thrust.Position = this.Position;

        ship.Roation = this.Roation;
        thrust.Roation = this.Roation;

        ship.Scale = this.Scale;
        thrust.Scale = this.Scale;
    }
     

    public void AddThrust()
    {
        thrust.PerformDraw = true;

        Velocity += DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), 1) * ShipThrust * Time.deltaTime;
    }

    public void NoThrust()
    {
        thrust.PerformDraw = false; 
    }

    public void RotateShip(float value)
    {
        Roation += (value * ShipRotation * Time.deltaTime * Mathf.Deg2Rad);
    }

    public void FireMissle(DrawableGrid grid, int sceneIndex)
    {
        Vector3 spawnpoint = this.Position;
        spawnpoint += DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), SpawnPointDistance); 

        Missle.MakeMissle(GetRotationinDegrees(), spawnpoint, grid, sceneIndex);
    }

    public void FireLaser(DrawableGrid grid, int sceneIndex)
    {
        IsDrawingLaser = true;
        LaserShowCounter = LaserShowTime;
    }
}
