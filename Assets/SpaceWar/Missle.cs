using UnityEngine;

public class Missle : MovingObject
{
 

    public float MoveSpeed = 150f;

 

    public override void Initalize()
    {
        base.Initalize();

        AddLineToObject(new Vector3(2, 0, 0), new Vector3(-2, 2, 0), Color.yellow);
        AddLineToObject(new Vector3(-2, 2, 0), new Vector3(-1, 0, 0), Color.yellow);
        AddLineToObject(new Vector3(-1, 0, 0), new Vector3(-2, -2, 0), Color.yellow);
        AddLineToObject(new Vector3(-2, -2, 0), new Vector3(2, 0, 0), Color.yellow);

    }

    public override void Tick()
    {
        base.Tick();

        if (CheckForCollisionWith(SpaceWarGrid.self.ShipAObject))
        {
            Debug.Log("Hit ShipA");
            SpaceWarGrid.self.PlayerBScore++;
            RemoveMissle();
        }

        if (CheckForCollisionWith(SpaceWarGrid.self.ShipBObject))
        {
            Debug.Log("Hit ShipB");
            SpaceWarGrid.self.PlayerAScore++;
            RemoveMissle();
        }
    }

    public void RemoveMissle()
    {
        SpaceWarGrid.self.RemoveObject(this);
        if (CollisionCircle != null) { SpaceWarGrid.self.RemoveObject(CollisionCircle); }

        //Can Do an Explostion :O
    }

    public static void MakeMissle(float angle, Vector3 spawnPosition, DrawableGrid grid, int sceneIndex)
    {
        Missle missle = new Missle();
        //uses ship position and angle
        //spawn object outside the ship
        //13, 14, 15 is a good radius beyond the ship
        //use example in spaceWarGrid as a base

        missle.Position = spawnPosition;
        missle.CreateCollision(2, grid, sceneIndex);
        missle.LaunchMissle(angle);
        grid.AddObjectToScene(sceneIndex, missle);
        SpaceWarGrid.self.MovingObjectlist.Add(missle);
        missle.willDrawCollision = true;
    }

    public void LaunchMissle(float angle)
    {
        SetRotationinDegrees(angle); 
        Velocity = DrawingTools.CircleRadiusPoint(Vector3.zero, angle, 1) * MoveSpeed; 
    }
}
