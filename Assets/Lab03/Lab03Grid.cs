using UnityEngine;

public class Lab03Grid : DrawableGrid
{
    public override void SetupScenes()
    {
        int sceneIndex; 
        DrawableArrow newArrow;
        DrawableObject newGraph;

        AddScene("Empty Scene, Use Tab To Switch Scenes");

        //******* PARABOLAS *******
        sceneIndex = AddScene("Parabola 1: Y=X^2");
        newGraph = new Parabola1();
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Parabola 2: Y=X^2+2x+1");
        newGraph = new Parabola2();
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Parabola 3: Y=-2X^2+10X+12");
        newGraph = new Parabola3();
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Parabola 4: X=-Y^3");
        newGraph = new Parabola4();
        AddObjectToScene(sceneIndex, newGraph);


        //******* DIAMONDS *******
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


        //******* ARROWS *******
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
