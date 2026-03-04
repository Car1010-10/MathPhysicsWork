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

        //*******FACING BOX*******\\

    }


}
