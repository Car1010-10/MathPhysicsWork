using UnityEngine;

public class Parabola3 : DrawableObject
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
    //Parabola3: y = -2X^2 + 10x + 12
    public float GetYPointatXof(float xValue)
    {
        float yValue;
        yValue = -2 * Mathf.Pow(xValue, 2) + 10 * xValue + 12;
        return yValue;
    }

    public Vector3 GetPointAt(float xValue)
    {
        return new Vector3(xValue, GetYPointatXof(xValue), 0);
    }
}
