using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceWarGrid : DrawableGrid
{
    public static SpaceWarGrid self; 

    public bool isPlayingGame = false;
    public bool isApplyingGravaity = true;

    public float ForceOfGravity = 2.5f; //units per second

    int sceneIndex = 0;

    public Missle missleObject;
    public ShipParent ShipAObject;
    public ShipParent ShipBObject;

    public DrawableObject DebugMagicCircle;
    public DrawableObject PlanetCircle;
    public float MagicCircleRadius = 150; 

    public List<MovingObject> MovingObjectlist = new List<MovingObject>();

    // Game Score, This is Player Deaths
    public int PlayerAScore = 0;
    public int PlayerBScore = 0;

    // Player Inputs
    bool P1_Thrust = false; 
    bool P2_Thrust = false;
    bool P1_CWRotation = false;
    bool P2_CWRotation = false;
    bool P1_CCWRotation = false;
    bool P2_CCWRotation = false;
    bool P1_FireMissle = false;    
    bool P2_FireMissle = false;
    bool P1_FireLaser = false;
    bool P2_FireLaser = false;

    bool F3Key = false;

    public void Awake()
    {
        self = this;
    }

    public override void SetupScenes()
    {
        sceneIndex = AddScene("Lab 07: SpaceWar");
        
        missleObject = new Missle();
        missleObject.Position = new Vector3(0, 100, 0);
        missleObject.CreateCollision(2, this, sceneIndex);
        missleObject.LaunchMissle(25);
        AddObjectToScene(sceneIndex, missleObject);
        MovingObjectlist.Add(missleObject);

        ShipAObject = new ShipParent();
        ShipAObject.SetupA(this, sceneIndex); 
        ShipAObject.Position = new Vector3(50, -50, 0);
        ShipAObject.SetRotationinDegrees(180);
        ShipAObject.CreateCollision(10, this, sceneIndex);
        AddObjectToScene(sceneIndex, ShipAObject);
        MovingObjectlist.Add(ShipAObject);

        ShipBObject = new ShipParent();
        ShipBObject.SetupB(this, sceneIndex);
        ShipBObject.Position = new Vector3(-50, 50, 0);
        ShipBObject.CreateCollision(10, this, sceneIndex);
        AddObjectToScene(sceneIndex, ShipBObject);
        MovingObjectlist.Add(ShipBObject);

        DebugMagicCircle = DrawingTools.CreateCircleObject(Vector3.zero, MagicCircleRadius, 360, Color.gray);
        AddObjectToScene(sceneIndex, DebugMagicCircle);

        PlanetCircle = DrawingTools.CreateCircleObject(Vector3.zero, 20, 36, Color.green);
        AddObjectToScene(sceneIndex, PlanetCircle);
    }

    public void RemoveObject(DrawableObject removeObject) 
    {
        RemoveList.Add(removeObject, sceneIndex);

        //breaks execution since we are alrady in a foreach loop
        //SceneList[sceneIndex].Remove(removeObject);
    }

    public override void ProcessInput(Keyboard kb, Mouse mouse)
    {
        P1_Thrust = kb.wKey.isPressed;
        P1_CWRotation = kb.dKey.isPressed;
        P1_CCWRotation = kb.aKey.isPressed;
        P1_FireMissle = kb.qKey.wasPressedThisFrame;
        P1_FireLaser = kb.eKey.wasPressedThisFrame;

        P2_Thrust = kb.iKey.isPressed;
        P2_CWRotation = kb.lKey.isPressed;
        P2_CCWRotation = kb.jKey.isPressed;
        P2_FireMissle = kb.uKey.wasPressedThisFrame;
        P2_FireLaser = kb.oKey.wasPressedThisFrame;

        F3Key = kb.f3Key.wasPressedThisFrame;
    }

    public override void Tick()
    {
        HandleInput();
        ApplyGravity();
    }

    public override void CleanUpScenes()
    {
        if (RemoveList.Count == 0) { return; } //don't do anything if there's nothing in the list

        foreach (var item in RemoveList)
        {
            SceneList[item.Value].Remove(item.Key);

            //DrawableObject thing = item.Key;
            //MovingObject mo = (MovingObject)item.Key;

            if (item.Key is MovingObject)
            {
                MovingObjectlist.Remove((MovingObject)item.Key);
            }
        }

        RemoveList.Clear();
    }

    public void ApplyGravity()
    {
        if (!isApplyingGravaity) { return; }
        Vector3 directionVector = Vector3.zero;

        foreach (MovingObject item in MovingObjectlist)
        {
            directionVector = -item.Position.normalized;
            item.Velocity += directionVector * ForceOfGravity * Time.deltaTime;
        }
    }

    public void RecordKill(bool who)
    {
        if (who)
        {
            //ship a
            PlayerAScore++;
        }
        else
        {
            //ship b 
            PlayerBScore++;
        }
    }

    public void HandleInput()
    {
        if (P1_Thrust) { ShipAObject.AddThrust(); } else { ShipAObject.NoThrust(); }
        if (P1_CWRotation) { ShipAObject.RotateShip(1); }
        if (P1_CCWRotation) { ShipAObject.RotateShip(-1); }
        if (P1_FireMissle) { ShipAObject.FireMissle(this, sceneIndex); }
        if (P1_FireLaser) { ShipAObject.FireLaser(this, sceneIndex); ; }

        if (P2_Thrust) { ShipBObject.AddThrust(); } else { ShipBObject.NoThrust(); }
        if (P2_CWRotation) { ShipBObject.RotateShip(1); }
        if (P2_CCWRotation) { ShipBObject.RotateShip(-1); }
        if (P2_FireMissle) { ShipBObject.FireMissle(this, sceneIndex); }
        if (P2_FireLaser) { ShipBObject.FireLaser(this, sceneIndex); }

        if (F3Key) { isApplyingGravaity = !isApplyingGravaity; }
    }

    public void TestStuff()
    {
        ShipAObject.Roation += (30 * Time.deltaTime * Mathf.Deg2Rad);
        ShipBObject.Roation += (-30 * Time.deltaTime * Mathf.Deg2Rad);
    }

    public void StartGame()
    {
        isPlayingGame = true;
        isTickingScenes = true; 
    }

    public void GameOver()
    {
        isPlayingGame = false;
        isTickingScenes = false;
    }
}
