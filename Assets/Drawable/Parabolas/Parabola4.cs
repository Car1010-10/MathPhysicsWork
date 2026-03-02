using UnityEngine;

public class Parabola4 : DrawableObject
{
    public override void Initalize()
    {
        //for loop from -100 to 100
        for (int i = -100; i < 100; i++)
        {
            AddLineToObject(GetPointAt(i), GetPointAt(i + 1), Color.green);
        }

    }

    //where the equation gets implemented
    //Parabola4: x = -y^3
    public float GetXPointatYof(float yValue)
    {
        float xValue;
        xValue = Mathf.Pow(-yValue, 3);
        return xValue;
    }

    public Vector3 GetPointAt(float yValue)
    {
        return new Vector3(GetXPointatYof(yValue), yValue, 0);
    }
}
