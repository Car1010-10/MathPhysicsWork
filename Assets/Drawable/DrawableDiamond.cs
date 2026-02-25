using System;
using System.Collections.Generic;
using UnityEngine;

class DrawableDiamond : DrawableObject
{
    public override void Initalize()
    {
        AddLineToObject(new Vector2(1, 0), new Vector2(0, 1), Color.magenta);
        AddLineToObject(new Vector2(0, 1), new Vector2(-1, 0), Color.magenta);
        AddLineToObject(new Vector2(-1, 0), new Vector2(0, -1), Color.magenta);
        AddLineToObject(new Vector2(0, -1), new Vector2(1, 0), Color.magenta);

    }
}
