using UnityEngine;

public class Lab04Grid : DrawableGrid
{
    public override void SetupScenes()
    {
        int sceneIndex;
        DrawableObject newGraph;

        //*******DIAMONDS*******\\
        sceneIndex = AddScene("Diamond, As is");
        newGraph = new DrawableDiamond();
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Diamond at scale of 20");
        newGraph = new DrawableDiamond();
        newGraph.Scale = (Vector3.one * 20);
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Diamond at scale of 20,10");
        newGraph = new DrawableDiamond();
        newGraph.Scale = new Vector3(20, 10, 1);
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Diamond at scale of 20,10, Rotation at 45 deg");
        newGraph = new DrawableDiamond();
        newGraph.Scale = new Vector3(20, 10, 1);
        newGraph.Roation = 45 * Mathf.Deg2Rad;
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Rotating Diamond!!!");
        newGraph = new RotatingDiamond();
        newGraph.Scale = new Vector3(20, 10, 1);
        AddObjectToScene(sceneIndex, newGraph);

        //*******FACING BOX*******\\
        sceneIndex = AddScene("Facing Box, At scale of 10,10");
        newGraph = new FacingBox();
        newGraph.Scale = new Vector3(10, 10, 1);
        AddObjectToScene(sceneIndex, newGraph);
    }

    public override void Tick()
    {
        
    }
}
