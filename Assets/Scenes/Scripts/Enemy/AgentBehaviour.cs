﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    public GameObject target;

    protected Agent agent;
    protected AgentBehaviour agentbehaviour;

    public float maxSpeed;
    public float maxAccel;
    public float maxRotation;
    public float maxAngularAccel;

    public virtual void Awake()
    {
        agent = gameObject.GetComponent<Agent>();
    }

    public virtual void Update()
    {
        agent.SetSteering(GetSteering());
    }

    public virtual Steering GetSteering()
    {
        return new Steering();
    }

    public float MapToRange(float rotation)
    {
        rotation %= 360.0f;
        if (Mathf.Abs(rotation) > 180.0f)
        {
            if (rotation < 0.0f)
                rotation += 360.0f;
            else rotation -= 360.0f;
        }
        return rotation;
    }
}
//шаблон для моделей поведения