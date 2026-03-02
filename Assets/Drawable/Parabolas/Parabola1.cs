using UnityEngine;

class Parabola1 : DrawableObject
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
    //Parabola1: y = X^2 (x to power of 2)
    public float GetYPointatXof(float xValue)
    {
        float yValue;
        yValue = Mathf.Pow(xValue, 2);
        //yValue = xValue * xValue <<Does the same as above :)
        return yValue;
    }

    public Vector3 GetPointAt(float xValue)
    {
        return new Vector3(xValue, GetYPointatXof(xValue), 0);
    }
}
