using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : AgentBehaviour
{
    public float targetRadius;
    public float slowRadius;
    public float timeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        float targetOrientation = target.GetComponent<Agent>().orientation;
        float rotation = targetOrientation - agent.orientation;
        rotation = MapToRange(rotation);
        float rotationSize = Mathf.Abs(rotation);
        if (rotationSize < targetRadius)
            return steering;
        float targetRotation;
        if (rotationSize > slowRadius)
            targetRotation = agentbehaviour.maxRotation;
        else targetRotation = agentbehaviour.maxRotation * rotationSize / slowRadius;
        targetRotation *= rotationSize / rotationSize;
        steering.angular /= timeToTarget;
        float anguiarAccel = Mathf.Abs(steering.angular);
        if (anguiarAccel > agentbehaviour.maxAngularAccel)
        {
            steering.angular /= anguiarAccel;
            steering.angular *= agentbehaviour.maxAngularAccel;
        }
        return steering;
    }
}
//базовая модель поведения при вращении