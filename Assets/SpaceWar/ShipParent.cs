using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipParent : MovingObject
{
    public DrawableObject ship;
    public DrawableObject thrust;
    public float ShipMaxVelocity = 250f;
    public float ShipThrust = 75f;
    public float ShipRotation = 180f; 
    public float SpawnPointDistance = 15f; 

    public void SetupA(DrawableGrid grid, int sceneIndex)
    {
        ship = new ShipA();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipAThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        //MaxVelocity = ShipMaxVelocity; 
    }

    public void SetupB(DrawableGrid grid, int sceneIndex)
    {
        ship = new ShipB();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipBThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

       //MaxVelocity = ShipMaxVelocity;
    }

    public override void Tick()
    {
        base.Tick();
        UpdateSubObjects();
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

    }
}
