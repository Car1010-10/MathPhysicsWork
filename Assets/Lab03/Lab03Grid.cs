using UnityEngine;

public class Lab03Grid : DrawableGrid
{
    public override void SetupScenes()
    {
        int sceneIndex; 
        DrawableArrow newArrow; 


        AddScene("Empty Scene, Use Tab To Switch Scenes");


        sceneIndex = AddScene("Arrow, As is");
        newArrow = new DrawableArrow();
        AddObjectToScene(sceneIndex, newArrow);


        sceneIndex = AddScene("Arrow, Moved Forward");
        newArrow = new DrawableArrow();
        newArrow.Position = new Vector2(30, 0);
        AddObjectToScene(sceneIndex, newArrow);


        sceneIndex = AddScene("Arrow, 50% size");
        newArrow = new DrawableArrow();
        newArrow.Scale = (Vector3.one * .5f); 
        AddObjectToScene(sceneIndex, newArrow);


        sceneIndex = AddScene("Arrow, Moved Forward at 25% size");
        newArrow = new DrawableArrow();
        newArrow.Position = new Vector2(30, 0);
        newArrow.Scale = (Vector3.one * .25f);
        AddObjectToScene(sceneIndex, newArrow);
    }
}
