using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering
{
    public float angular;
    public Vector3 linear;

    public Steering ()
    {
        angular = 0.0f;
        linear = new Vector3();
    }
}
//класс для хранения значений перемещения и поворота агента